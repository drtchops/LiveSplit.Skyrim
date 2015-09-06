using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.Skyrim
{
    class GameMemory
    {
        public event EventHandler OnStartSaveLoad;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        public delegate void SplitCompletedEventHandler(object sender, SplitArea type, string[] template, uint frame);
        public event SplitCompletedEventHandler OnSplitCompleted;
        public event EventHandler OnBearCart;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;

        private enum ExpectedDllSizes
        {
            SkyrimSteam = 27336704,
            SkyrimCracked = 26771456,
        }

        public bool[] splitStates { get; set; }
        private SkyrimData data;

        public void resetSplitStates()
        {
            for (int i = 0; i <= (int)SplitArea.AlduinDefeated; i++)
            {
                splitStates[i] = false;
            }

            if (data != null)
            {
                data.isSkyHavenTempleVisited = false;
                data.isAlduin1Defeated = false;
                data.leaveSleepingGiantInnCounter = 0;
                data.isCouncilDone = false;
            }
        }

        public GameMemory()
        {
            splitStates = new bool[(int)SplitArea.AlduinDefeated + 1];

            resetSplitStates();

            _ignorePIDs = new List<int>();
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
            _thread = Task.Factory.StartNew(MemoryReadThread);
        }

        public void Stop()
        {
            if (_cancelSource == null || _thread == null || _thread.Status != TaskStatus.Running)
            {
                return;
            }

            _cancelSource.Cancel();
            _thread.Wait();
        }

        void MemoryReadThread()
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

                    uint frameCounter = 0;
                    data = new SkyrimData();

                    while (!game.HasExited)
                    {
                        data.UpdateAll(game);

                        if (data.IsLoading.Changed)
                        {
                            if (data.IsLoading.Current)
                            {
                                Trace.WriteLine($"[NoLoads] Load Start - {frameCounter}");

                                // pause game timer
                                FireEvent(OnLoadStarted);
                            }
                            else
                            {
                                Trace.WriteLine($"[NoLoads] Load End - {frameCounter}");

                                if (!data.loadScreenFadeoutStarted)
                                {
                                    if (data.Location.Current == new Location[]{ new Location(Locations.Tamriel, 13, -10), new Location(Locations.Tamriel, 13, -9) } && data.WordsOfPowerLearned.Current == 3)
                                    {
                                        Split(SplitArea.ClearSky, new string[]{ SplitTemplates.MRWALRUS }, frameCounter);
                                    }
                                }

                                // unpause game timer
                                FireEvent(OnLoadFinished);
                            }
                        }

                        if (data.IsLoadingScreen.Changed)
                        {
                            if (data.IsLoadingScreen.Current)
                            {
                                Trace.WriteLine($"[NoLoads] LoadScreen Start at {data.LocationID.Current.ToString("X8")} X: {data.WorldY.Current} Y: {data.WorldY.Current} - {frameCounter}");

                                data.loadingScreenStarted = true;
                                data.loadScreenStartLocation = data.Location.Current;

                                if (data.IsInFadeOut.Current)
                                {
                                    data.loadScreenFadeoutStarted = true;
                                }

                                if (data.IsInEscapeMenu.Current)
                                {
                                    data.isLoadingSaveFromMenu = true;
                                }

                                // if it isn't a loadscreen from loading a save
                                if (!data.isLoadingSaveFromMenu)
                                {
                                    data.isWaitingLocationUpdate = true;
                                    data.isWaitingLocationIDUpdate = true;

                                    // if loadscreen starts while leaving helgen
                                    if (data.Location.Current == new Location(Locations.HelgenKeep01, -2, -5))
                                    {
                                        Split(SplitArea.Helgen, null, frameCounter);
                                    }
                                    // if loadscreen starts in around the carriage of Whiterun Stables
                                    else if (data.loadScreenStartLocation == new Location[]{ new Location(Locations.Tamriel, 4, -3), new Location(Locations.Tamriel, 4, -4) })
                                    {
                                        string[] templates = new string[]
                                        {
                                            SplitTemplates.DRTCHOPS,
                                            SplitTemplates.GR3YSCALE
                                        };
                                        Split(SplitArea.Whiterun, templates, frameCounter);
                                    }
                                    // if loadscreen starts at Markarth stables
                                    else if (data.loadScreenStartLocation == new Location(Locations.Tamriel, -42, 1))
                                    {
                                        Split(SplitArea.Karthspire, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                                    }
                                    // if loadscreen starts in Karthspire and Sky Haven Temple has been entered at least once
                                    else if (data.loadScreenStartLocation.ID == (int)Locations.KarthspireRedoubtWorld && data.isSkyHavenTempleVisited)
                                    {
                                        string[] templates = new string[]
                                        {
                                            SplitTemplates.MRWALRUS,
                                            SplitTemplates.DRTCHOPS,
                                            SplitTemplates.DALLETH
                                        };
                                        Split(SplitArea.TheWall, templates, frameCounter);
                                    }
                                }
                                else
                                {
                                    data.isWaitingLocationUpdate = false;
                                    data.isWaitingLocationIDUpdate = false;
                                }
                            }
                            else
                            {
                                Trace.WriteLine($"[NoLoads] LoadScreen End at {data.LocationID.Current.ToString("X8")} X: {data.WorldX.Current} Y: {data.WorldY.Current} - {frameCounter}");

                                if (data.loadingScreenStarted)
                                {
                                    data.loadingScreenStarted = false;
                                    data.isLoadingSaveFromMenu = false;
                                }
                            }
                        }

                        if (data.IsInFadeOut.Changed)
                        {
                            if (data.IsInFadeOut.Current)
                            {
                                Debug.WriteLine($"[NoLoads] Fadeout started - {frameCounter}");
                                if (data.IsLoadingScreen.Current)
                                {
                                    data.loadScreenFadeoutStarted = true;
                                }
                            }
                            else
                            {
                                Debug.WriteLine($"[NoLoads] Fadeout ended - {frameCounter}");
                                // if loadscreen fadeout finishes in helgen
                                if (data.IsInFadeOut.Old && data.loadScreenFadeoutStarted &&
                                    data.Location.Current == new Location(Locations.Tamriel, 3, -20))
                                {
                                    // start and reset
                                    Trace.WriteLine($"[NoLoads] Reset and Start - {frameCounter}");
                                    FireEvent(OnStartSaveLoad);
                                }
                                data.loadScreenFadeoutStarted = false;
                            }
                        }

                        if (data.Location.Changed && data.isWaitingLocationUpdate)
                        {
                            data.isWaitingLocationUpdate = false;

                            // if loadscreen starts while in front of the door of Thalmor Embassy and doesn't end inside the Embassy
                            if (data.loadScreenStartLocation == new Location(Locations.Tamriel, -20, 28) && data.LocationID.Current != (int)Locations.ThalmorEmbassy02)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.ThalmorEmbassy, templates, frameCounter);
                            }
                            // if loadscreen starts while in front of the Sleeping Giant Inn and doesn't end inside it or in riften
                            else if (data.loadScreenStartLocation == new Location(Locations.Tamriel, 5, -11)
                                && data.LocationID.Current != (int)Locations.RiverwoodSleepingGiantInn && data.LocationID.Current != (int)Locations.RiftenWorld)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Riverwood, templates, frameCounter);
                            }
                            // if loadscreen starts outside Septimus' Outpost and doesn't end inside it
                            else if (data.loadScreenStartLocation == new Location(Locations.Tamriel, 28, 34) && data.LocationID.Current != (int)Locations.SeptimusSignusOutpost)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Septimus, templates, frameCounter);
                            }
                            // if loadscreen starts outside Mzark Tower and doesn't end inside it
                            else if (data.loadScreenStartLocation == new Location(Locations.Tamriel, 6, 11) && data.LocationID.Current != (int)Locations.TowerOfMzark)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.MzarkTower, templates, frameCounter);
                            }
                            // if loadscreen starts in High Hrothgar's whereabouts and doesn't end inside
                            else if (data.loadScreenStartLocation == new Location[] { new Location(Locations.Tamriel, 13, -9), new Location(Locations.Tamriel, 13, -10) }
                                && data.LocationID.Current != (int)Locations.HighHrothgar)
                            {
                                if (!data.isCouncilDone)
                                {
                                    Split(SplitArea.HighHrothgar, null, frameCounter);
                                }
                                else
                                {
                                    string[] templates = new string[]
                                    {
                                        SplitTemplates.DRTCHOPS,
                                        SplitTemplates.DALLETH
                                    };
                                    Split(SplitArea.Council, templates, frameCounter);
                                }
                            }
                        }

                        if (data.LocationID.Changed && data.isWaitingLocationIDUpdate)
                        {
                            data.isWaitingLocationIDUpdate = false;

                            if (data.LocationID.Current == (int)Locations.SkyHavenTemple)
                            {
                                data.isSkyHavenTempleVisited = true;
                            }

                            // if loadscreen starts in whiterun and doesn't end in dragonsreach
                            if (data.loadScreenStartLocation == new Location(Locations.WhiterunWorld, 6, 0)
                                && data.LocationID.Current != (int)Locations.WhiterunDragonsreach)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Whiterun, templates, frameCounter);
                            }
                            // if loadscreen starts Thalmor Embassy and ends in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.ThalmorEmbassy02
                                && data.Location.Current == new Location(Locations.Tamriel, -20, 28))
                            {
                                Split(SplitArea.ThalmorEmbassy, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts while in front of the ratway door and doesn't end inside it
                            else if (data.loadScreenStartLocation == new Location(Locations.RiftenWorld, 42, -24)
                                && data.LocationID.Current != (int)Locations.RiftenRatway01)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Esbern, templates, frameCounter);
                            }
                            // if loadscreen starts inside the ratway and ends in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.RiftenRatway01
                                && data.Location.Current == new Location(Locations.RiftenWorld, 42, -24))
                            {
                                Split(SplitArea.Esbern, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts while leaving the Sleeping Giant Inn and ends in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.RiverwoodSleepingGiantInn
                                && data.Location.Current == new Location(Locations.Tamriel, 5, -11))
                            {
                                data.leaveSleepingGiantInnCounter++;
                                if (data.leaveSleepingGiantInnCounter == 2)
                                {
                                    Split(SplitArea.Riverwood, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                                }
                            }
                            // if loadingscren starts in Karthspire and ends at Markarth Stables
                            else if (data.loadScreenStartLocation.ID == (int)Locations.KarthspireRedoubtWorld
                                && data.Location.Current == new Location(Locations.Tamriel, -42, 1))
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Karthspire, templates, frameCounter);
                            }
                            // if loadscreen starts inside Septimus' Outpost and ends in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.SeptimusSignusOutpost
                                && data.Location.Current == new Location(Locations.Tamriel, 28, 34))
                            {
                                Split(SplitArea.Septimus, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadingscren starts in Sky Haven Temple and ends in Karthspire
                            else if (data.loadScreenStartLocation.ID == (int)Locations.SkyHavenTemple
                                && data.LocationID.Current == (int)Locations.KarthspireRedoubtWorld)
                            {
                                Split(SplitArea.TheWall, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts inside Mzark Tower and ends outside of it
                            else if (data.loadScreenStartLocation.ID == (int)Locations.TowerOfMzark
                                && data.Location.Current == new Location(Locations.Tamriel, 6, 11))
                            {
                                Split(SplitArea.MzarkTower, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if Alduin1 has been defeated once and loadscreen starts in Paarthurnax' mountain whereabouts and ends in front of dragonsreach (fast travel)
                            else if (data.loadScreenStartLocation == Location.ThroatOfTheWorld
                                && data.Location.Current == new Location(Locations.WhiterunWorld, 6, -1) && data.isAlduin1Defeated)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.GR3YSCALE,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Alduin1, templates, frameCounter);
                            }
                            // if loadscreen starts in high hrothgar and ends in front of one of its doors
                            else if (data.loadScreenStartLocation.ID == (int)Locations.HighHrothgar
                                && data.Location.Current == new Location[]{ new Location(Locations.Tamriel, 13, -9), new Location(Locations.Tamriel, 13, -10) })
                            {
                                if (!data.isCouncilDone)
                                {
                                    string[] templates = new string[]
                                    {
                                        SplitTemplates.DRTCHOPS,
                                        SplitTemplates.GR3YSCALE
                                    };
                                    Split(SplitArea.ClearSky, templates, frameCounter);
                                }
                                else if (data.isCouncilDone)
                                {
                                    Split(SplitArea.Council, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                                }
                            }
                            // if loadscreen starts in Solitude in front of the door of Castle Dour and doesn't end inside it
                            else if (data.loadScreenStartLocation == new Location(Locations.SolitudeWorld, -16, 26)
                                && data.LocationID.Current != (int)Locations.SolitudeCastleDour)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Solitude, templates, frameCounter);
                            }
                            // if loadscreen starts in Solitude Castle Dour and ends outside in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.SolitudeCastleDour
                                && data.Location.Current == new Location(Locations.SolitudeWorld, -16, 26))
                            {
                                Split(SplitArea.Solitude, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts in Windhelm and doesn't end inside
                            else if (data.loadScreenStartLocation == new Location(Locations.WindhelmWorld, 32, 10)
                                && data.LocationID.Current != (int)Locations.WindhelmPalaceoftheKings)
                            {
                                string[] templates = new string[]
                                {
                                    SplitTemplates.MRWALRUS,
                                    SplitTemplates.DRTCHOPS,
                                    SplitTemplates.DALLETH
                                };
                                Split(SplitArea.Windhelm, templates, frameCounter);
                            }
                            // if loadscreen starts in Windhelm's Palace of the Kings and ends outside
                            else if (data.loadScreenStartLocation.ID == (int)Locations.WindhelmPalaceoftheKings
                                && data.Location.Current == new Location(Locations.WindhelmWorld, 32, 10))
                            {
                                Split(SplitArea.Windhelm, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen ends in Skuldafn.
                            else if (data.LocationID.Current == (int)Locations.SkuldafnWorld)
                            {
                                Split(SplitArea.Odahviing, null, frameCounter);
                            }
                            // if loadscreen ends in Sovngarde
                            else if (data.LocationID.Current == (int)Locations.Sovngarde)
                            {
                                Split(SplitArea.EnterSovngarde, null, frameCounter);
                            }
                        }

                        if (data.WordsOfPowerLearned.Changed && data.WordsOfPowerLearned.Current == 3 && data.Location.Current == new Location(Locations.Tamriel, 13, -10))
                        {
                            Split(SplitArea.ClearSky, new string[]{ SplitTemplates.DALLETH }, frameCounter);
                        }

                        if (data.LocationsDiscovered.Current == data.LocationsDiscovered.Old + 1 && data.Location.Current == Location.ThroatOfTheWorld)
                        {
                                Split(SplitArea.HorseClimb, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                        }

                        if (data.ArePlayerControlsDisabled.Changed && !data.IsInEscapeMenu.Current)
                        {
                            if (data.ArePlayerControlsDisabled.Current)
                            {
                                if (data.Location.Current == new Location(Locations.Tamriel, 13, -12))
                                {
                                    string[] templates = new string[]
                                    {
                                        SplitTemplates.DRTCHOPS,
                                        SplitTemplates.DALLETH
                                    };
                                    Split(SplitArea.CutsceneStart, templates, frameCounter);
                                }
                            }
                            else
                            {
                                if (data.Location.Current == new Location(Locations.Tamriel, 13, -12))
                                {
                                    string[] templates = new string[]
                                    {
                                        SplitTemplates.GR3YSCALE,
                                        SplitTemplates.DALLETH,
                                        SplitTemplates.DRTCHOPS
                                    };
                                    Split(SplitArea.CutsceneEnd, templates, frameCounter);
                                }
                            }
                        }

                        if (data.Alduin1Health.Current < 0 && !data.isAlduin1Defeated)
                        {
                            Debug.WriteLine($"[NoLoads] Alduin 1 has been defeated. HP: {data.Alduin1Health.Current} - {frameCounter}");
                            data.isAlduin1Defeated = true;

                            Split(SplitArea.Alduin1, new string[]{ SplitTemplates.MRWALRUS }, frameCounter);
                        }

                        if (data.LocationID.Current == (int)Locations.HelgenKeep01 && data.BearCartHealth.Current < 0 && data.BearCartHealth.Old >= 0 && frameCounter > 1)
                        {
                            Debug.WriteLine($"[NoLoads] BEAR CART! HP: {data.BearCartHealth.Current} - {frameCounter}");
                            FireEvent(OnBearCart);
                        }

                        // the only mainquest you can complete here is the council so when a quest completes, walrus' council split
                        if (data.MainQuestsCompleted.Current == data.MainQuestsCompleted.Old + 1 && data.LocationID.Current == (int)Locations.HighHrothgar)
                        {
                            data.isCouncilDone = true;

                            Split(SplitArea.Council, new string[]{ SplitTemplates.MRWALRUS }, frameCounter);
                        }

                        // if alduin is defeated in sovngarde
                        if (data.IsAlduin2Defeated.Changed && data.IsAlduin2Defeated.Current && data.LocationID.Current == (int)Locations.Sovngarde)
                        {
                            Split(SplitArea.AlduinDefeated, null, frameCounter);
                        }

                        // reset lastQuest 100 frames (1.5 seconds) after a completion to avoid splitting on a wrong questline.
                        if (frameCounter >= data.lastQuestframeCounter + 100 && data.lastQuestCompleted != SplitArea.None)
                        {
                            data.lastQuestCompleted = SplitArea.None;
                        }

                        if (data.CollegeOfWinterholdQuestsCompleted.Current > data.CollegeOfWinterholdQuestsCompleted.Old)
                        {
                            Debug.WriteLine($"[NoLoads] A College of Winterhold quest has been completed - {frameCounter}");
                            data.lastQuestCompleted = SplitArea.CollegeOfWinterholdQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }
                        else if (data.CompanionsQuestsCompleted.Current > data.CompanionsQuestsCompleted.Old)
                        {
                            Debug.WriteLine($"[NoLoads] A Companions quest has been completed - {frameCounter}");
                            data.lastQuestCompleted = SplitArea.CompanionsQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }
                        else if (data.DarkBrotherhoodQuestsCompleted.Current > data.DarkBrotherhoodQuestsCompleted.Old)
                        {
                            Debug.WriteLine($"[NoLoads] A Dark Brotherhood quest has been completed - {frameCounter}");
                            data.lastQuestCompleted = SplitArea.DarkBrotherhoodQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }
                        else if (data.ThievesGuildQuestsCompleted.Current > data.ThievesGuildQuestsCompleted.Old)
                        {
                            Debug.WriteLine($"[NoLoads] A Thieves' Guild quest has been completed - {frameCounter}");
                            data.lastQuestCompleted = SplitArea.ThievesGuildQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }

                        // if a questline is completed
                        if (data.QuestlinesCompleted.Current > data.QuestlinesCompleted.Old)
                        {
                            Debug.WriteLineIf(data.lastQuestCompleted == SplitArea.None, $"[NoLoads] A questline has been completed. - {frameCounter}");
                            Split(data.lastQuestCompleted, null, frameCounter);
                        }

                        Debug.WriteLineIf(data.LocationID.Changed, $"[NoLoads] Location changed to {data.LocationID.Current.ToString("X8")} - {frameCounter}");
                        Debug.WriteLineIf(data.WorldX.Changed || data.WorldY.Changed, $"[NoLoads] Coords changed to X: {data.WorldX.Current} Y: {data.WorldY.Current} - {frameCounter}");
                        Debug.WriteLineIf(data.IsInEscapeMenu.Changed, $"[NoLoads] isInEscapeMenu changed to {data.IsInEscapeMenu.Current} - {frameCounter}");

                        frameCounter++;

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

        private void Split(SplitArea split, string[] templates, uint frame)
        {
            FireEvent(OnSplitCompleted, this, split, templates, frame);
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
    }
}
