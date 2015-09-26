using LiveSplit.AutoSplitting;
using LiveSplit.Skyrim.AutoSplitData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.Skyrim
{
	class GameMemory : IDisposable
	{
		public event EventHandler OnStartSaveLoad;
		public event EventHandler OnLoadStarted;
		public event EventHandler OnLoadFinished;
		public event EventHandler<SplitEventArgs> OnSplit;
		public event EventHandler OnBearCart;

		public AutoSplitManager AutoSplitManager { get; set; }

		private Task _thread;
		private CancellationTokenSource _cancelSource;
		private SynchronizationContext _uiThread;
		private List<int> _ignorePIDs;
		private SkyrimData _data;

		private enum ExpectedDllSizes
		{
			SkyrimSteam = 27336704,
			SkyrimCracked = 26771456,
		}

		public GameMemory()
		{
			_ignorePIDs = new List<int>();

			AutoSplitManager = new AutoSplitManager();
			AutoSplitManager.SplitTriggered += (s, e) =>
			{
				string debug = e.AutoSplit.Name == "Alduin Defeated" ?
					$"{AutoSplitManager.AutoSplitList.Count(split => split.Triggered) + 1}/{AutoSplitManager.AutoSplitList.Count()}"
					: string.Empty;
				Debug.WriteLine($"[AutoSplitManager] Triggered {e.AutoSplit.Name} {debug} - {_data.FrameCounter}");
				FireEvent(OnSplit, this, e);
			};
		}

		public void StartMonitoring()
		{
			if (_thread != null && _thread.Status == TaskStatus.Running)
			{
				throw new InvalidOperationException();
			}
			if (!(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
			{
				throw new InvalidOperationException("SynchronizationContext.Current is not a UI thread.");
			}

			_uiThread = SynchronizationContext.Current;
			_cancelSource = new CancellationTokenSource();
			_thread = Task.Factory.StartNew(MemoryReadThread, CancellationToken.None, TaskCreationOptions.LongRunning);
		}

		public void Stop()
		{
			if (_cancelSource == null || _thread == null || _thread.Status != TaskStatus.Running)
			{
				return;
			}

			_cancelSource.Cancel();
			_thread.Wait();
			_cancelSource.Dispose();
		}

		public void Reset()
		{
			_data?.Reset();
			AutoSplitManager.Reset();
		}

		void MemoryReadThread(object obj)
		{
			Trace.WriteLine("[NoLoads] MemoryReadThread");

			while (!_cancelSource.IsCancellationRequested)
			{
				try
				{
					Trace.WriteLine("[NoLoads] Waiting for TESV.exe...");

					Process game;
					while ((game = GetGameProcess()) == null)
					{
						Thread.Sleep(250);
						if (_cancelSource.IsCancellationRequested)
						{
							return;
						}
					}

					Trace.WriteLine("[NoLoads] Got TESV.exe!");

					_data = new SkyrimData();
					uint? framesCountLoadScreenEnd = null;

					while (!game.HasExited)
					{
						_data.UpdateAll(game);

						if (_data.IsLoading.Changed)
						{
							if (_data.IsLoading.Current)
							{
								Trace.WriteLine($"[NoLoads] Load Start - {_data.FrameCounter}");

								// pause game timer
								FireEvent(OnLoadStarted);
							}
							else
							{
								Trace.WriteLine($"[NoLoads] Load End - {_data.FrameCounter}");

								if (!_data.LoadScreenFadeoutStarted && !_data.IsLoadingScreen.Old)
								{
									if (_data.QuickLoadFadeoutStarted)
									{
										Debug.WriteLine($"[NoLoads] Quickload - {_data.FrameCounter}");
										AutoSplitManager.Update(_data, SkyrimEvent.QuickLoad);
									}
									else
									{
										Debug.WriteLine($"[NoLoads] Quicksave - {_data.FrameCounter}");
										AutoSplitManager.Update(_data, SkyrimEvent.QuickSave);
									}
								}

								// unpause game timer
								FireEvent(OnLoadFinished);
							}
						}

						if (_data.IsLoadingScreen.Changed)
						{
							if (_data.IsLoadingScreen.Current)
							{
								Trace.WriteLine($"[NoLoads] LoadScreen Start at {_data.WorldID.Current.ToString("X8")} X: {_data.CellX.Current} Y: {_data.CellY.Current} - {_data.FrameCounter}");

								_data.LoadingScreenStarted = true;
								_data.LoadScreenStartLocation = _data.Location.Current;
								_data.LoadScreenFadeoutStarted = _data.IsInFadeOut.Current;
								_data.IsLoadingSaveFromMenu = _data.IsInEscapeMenu.Current;
								framesCountLoadScreenEnd = null;

								// if it isn't a loadscreen from loading a save
								if (!_data.IsLoadingSaveFromMenu)
								{
									_data.IsWaitingLocationIDUpdate = true;
									AutoSplitManager.Update(_data, SkyrimEvent.LoadScreenStart);
								}
								else
									_data.IsWaitingLocationIDUpdate = false;
							}
							else
							{
								Trace.WriteLine($"[NoLoads] LoadScreen End at {_data.WorldID.Current.ToString("X8")} X: {_data.CellX.Current} Y: {_data.CellY.Current} - {_data.FrameCounter}");
								_data.LoadingScreenStarted = false;
								_data.IsLoadingSaveFromMenu = false;
								framesCountLoadScreenEnd = _data.FrameCounter;
								AutoSplitManager.Update(_data, SkyrimEvent.LoadScreenLoadEnd);
							}
						}

						if (_data.IsInFadeOut.Changed)
						{
							if (_data.IsInFadeOut.Current)
							{
								Debug.WriteLine($"[NoLoads] Fadeout started - {_data.FrameCounter}");

								if (_data.IsLoadingScreen.Current)
									_data.LoadScreenFadeoutStarted = true;
								else if (_data.IsQuickSaving.Current)
									_data.QuickLoadFadeoutStarted = true;
							}
							else
							{
								Debug.WriteLine($"[NoLoads] Fadeout ended - {_data.FrameCounter}");
								// if loadscreen fadeout finishes in helgen
								if (_data.IsInFadeOut.Old && _data.LoadScreenFadeoutStarted &&
									_data.Location.Current == new Location((int)Worlds.Tamriel, 3, -20))
								{
									// start and reset
									Trace.WriteLine($"[NoLoads] Reset and Start - {_data.FrameCounter}");
									FireEvent(OnStartSaveLoad);
								}

								_data.LoadScreenFadeoutStarted = false;
								_data.QuickLoadFadeoutStarted = false;
							}
						}

						//sometimes the locationID changes a few frames after the end of the loadscreen, wait 2 frames max to trigger the event
						if (_data.IsWaitingLocationIDUpdate && (_data.WorldID.Changed || (framesCountLoadScreenEnd != null && _data.FrameCounter - framesCountLoadScreenEnd >= 2)))
						{
							_data.IsWaitingLocationIDUpdate = false;

							if (_data.WorldID.Current == (int)Worlds.SkyHavenTemple)
								_data.IsSkyHavenTempleVisited.Current = true;

							// if loadscreen starts while leaving the Sleeping Giant Inn and ends in front of its door
							if (_data.LoadScreenStartLocation.ID == (int)Worlds.RiverwoodSleepingGiantInn
								&& _data.Location.Current == new Location((int)Worlds.Tamriel, 5, -11))
							{
								_data.LeaveSleepingGiantInnCounter.Current++;
							}

							AutoSplitManager.Update(_data, SkyrimEvent.LoadScreenEnd);
						}

						if (_data.WorldID.Current == (int)Worlds.HelgenKeep01 && _data.BearCartHealth.Current < 0 && _data.BearCartHealth.Old >= 0 && _data.FrameCounter > 1)
						{
							Debug.WriteLine($"[NoLoads] BEAR CART! HP: {_data.BearCartHealth.Current} - {_data.FrameCounter}");
							FireEvent(OnBearCart);
						}

						if (!_data.IsAlduin1Defeated.Current && _data.Alduin1Health.Current < 0)
						{
							Debug.WriteLine($"[NoLoads] Alduin 1 has been defeated - {_data.FrameCounter}");
							_data.IsAlduin1Defeated.Current = true;
						}

						// the only mainquest you can complete here is the council so when a quest completes, walrus' council split
						if (_data.MainQuestsCompleted.Current == _data.MainQuestsCompleted.Old + 1 && _data.WorldID.Current == (int)Worlds.HighHrothgar)
						{
							_data.IsCouncilDone.Current = true;
						}

						// reset lastQuest 100 frames (1.5 seconds) after a completion to avoid splitting on a wrong questline.
						if (_data.FrameCounter >= _data.LastQuestframeCounter + 100 && _data.LastQuestCompleted != Questlines.None)
						{
							_data.LastQuestCompleted = Questlines.None;
						}

						if (_data.CollegeOfWinterholdQuestsCompleted.Current > _data.CollegeOfWinterholdQuestsCompleted.Old)
						{
							Debug.WriteLine($"[NoLoads] A College of Winterhold quest has been completed - {_data.FrameCounter}");
							_data.LastQuestCompleted = Questlines.CollegeOfWinterhold;
							_data.LastQuestframeCounter = _data.FrameCounter;
						}
						else if (_data.CompanionsQuestsCompleted.Current > _data.CompanionsQuestsCompleted.Old)
						{
							Debug.WriteLine($"[NoLoads] A Companions quest has been completed - {_data.FrameCounter}");
							_data.LastQuestCompleted = Questlines.Companions;
							_data.LastQuestframeCounter = _data.FrameCounter;
						}
						else if (_data.DarkBrotherhoodQuestsCompleted.Current > _data.DarkBrotherhoodQuestsCompleted.Old)
						{
							Debug.WriteLine($"[NoLoads] A Dark Brotherhood quest has been completed - {_data.FrameCounter}");
							_data.LastQuestCompleted = Questlines.DarkBrotherhood;
							_data.LastQuestframeCounter = _data.FrameCounter;
						}
						else if (_data.ThievesGuildQuestsCompleted.Current > _data.ThievesGuildQuestsCompleted.Old)
						{
							Debug.WriteLine($"[NoLoads] A Thieves' Guild quest has been completed - {_data.FrameCounter}");
							_data.LastQuestCompleted = Questlines.ThievesGuild;
							_data.LastQuestframeCounter = _data.FrameCounter;
						}

						AutoSplitManager.Update(_data, GameEvent.Always);

						Debug.WriteLineIf(_data.MiscObjectivesCompleted.Changed, $"[NoLoads] MiscObjectivesCompleted changed from {_data.MiscObjectivesCompleted.Old} to {_data.MiscObjectivesCompleted.Current} - {_data.FrameCounter}");
						Debug.WriteLineIf(_data.WorldID.Changed, $"[NoLoads] Location changed to {_data.WorldID.Current.ToString("X8")} - {_data.FrameCounter}");
						//Debug.WriteLineIf(data.WorldX.Changed || data.WorldY.Changed, $"[NoLoads] Coords changed to X: {data.WorldX.Current} Y: {data.WorldY.Current} - {data.frameCounter}");
						Debug.WriteLineIf(_data.IsInEscapeMenu.Changed, $"[NoLoads] isInEscapeMenu changed to {_data.IsInEscapeMenu.Current} - {_data.FrameCounter}");

						_data.FrameCounter++;

						Thread.Sleep(15);

						if (_cancelSource.IsCancellationRequested)
						{
							return;
						}
					}

					//pause time when game crashes or closes
					FireEvent(OnLoadStarted);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex.ToString());
					Thread.Sleep(1000);
				}
			}
		}

		void FireEvent(Delegate handler, params object[] args)
		{
			_uiThread.Post(d => handler?.DynamicInvoke(args), null);
		}
		void FireEvent(EventHandler handler) => FireEvent(handler, this, EventArgs.Empty);

		Process GetGameProcess()
		{
			Process game = Process.GetProcesses().FirstOrDefault(p => p.ProcessName.ToLower() == "tesv"
				&& !p.HasExited && !_ignorePIDs.Contains(p.Id));
			if (game == null)
			{
				return null;
			}

			if (game.MainModule.ModuleMemorySize != (int)ExpectedDllSizes.SkyrimSteam && game.MainModule.ModuleMemorySize != (int)ExpectedDllSizes.SkyrimCracked)
			{
				_ignorePIDs.Add(game.Id);
				_uiThread.Send(d => MessageBox.Show("Unexpected game version. Skyrim 1.9.32.0.8 is required.", "LiveSplit.Skyrim",
					MessageBoxButtons.OK, MessageBoxIcon.Error), null);
				return null;
			}

			return game;
		}

		public void Dispose() => Stop();
	}
}
