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
        public enum SplitArea : int
        {
            Helgen,
            AlduinDefeated,
        }

        public event EventHandler OnFirstLevelLoading;
        public event EventHandler OnPlayerGainedControl;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        public event EventHandler OnLoadScreenStarted;
        public event EventHandler OnLoadScreenFinished;
        public delegate void SplitCompletedEventHandler(object sender, SplitArea type);
        public event SplitCompletedEventHandler OnSplitCompleted;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;

        private DeepPointer _isLoadingPtr;
        private DeepPointer _isLoadingScreenPtr;
        private DeepPointer _isInLoadScreenFadeOutPtr;
        private DeepPointer _isInTamriel;
        private DeepPointer _world_XPtr;
        private DeepPointer _world_YPtr;
        private DeepPointer _isAlduinDefeatedPtr;

        private enum ExpectedDllSizes
        {
            SkyrimSteam = 27336704,
            SkyrimCracked = 26771456,
        }

        private bool[] splitStates = new bool[(int)SplitArea.AlduinDefeated + 1];

        public GameMemory()
        {
            // Loads
            _isLoadingPtr = new DeepPointer("TESV.exe", 0x17337CC); // == 1 if a load is happening (any except loading screens in Helgen for some reason)
            _isLoadingScreenPtr = new DeepPointer("TESV.exe", 0xEE3561); // == 1 if in a loading screen
            // _isPausedPtr = new DeepPointer("TESV.exe", 0x172E85F); // == 1 if in a menu or a loading screen
            _isInLoadScreenFadeOutPtr = new DeepPointer("TESV.exe", 0x172EE2E); // == 1 from the fade out of a loading, it goes back to 0 once control is gained

            // Position
            _isInTamriel = new DeepPointer("TESV.exe", 0x173815C); // == 1 if the player is in the Tamriel world space
            _world_XPtr = new DeepPointer("TESV.exe", 0x0172E864, 0x64); // X world position (cell)
            _world_YPtr = new DeepPointer("TESV.exe", 0x0172E864, 0x68); // Y world position (cell)

            // Game state
            _isAlduinDefeatedPtr = new DeepPointer("TESV.exe", 0x1711608); // == 1 when last blow is struck on alduin
            // _playerHasControlPtr = new DeepPointer("TESV.exe", 0x74814710); // == 1 when player has full control

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

        public void setSplitState(SplitArea split, bool value)
        {
            splitStates[(int)split] = value;
        }

        public void resetSplitStates()
        {
            for (int i = 0; i <= (int)SplitArea.AlduinDefeated; i++)
            {
                splitStates[i] = false;
            }
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

                    bool prevIsLoading = false;
                    bool prevIsLoadingScreen = false;
                    bool prevIsAlduinDefeated = false;
                    bool prevIsInLoadScreenFadeOut = false;

                    bool loadingStarted = false;
                    bool loadingScreenStarted = false;

                    while (!game.HasExited)
                    {
                        bool isLoading;
                        _isLoadingPtr.Deref(game, out isLoading);

                        bool isLoadingScreen;
                        _isLoadingScreenPtr.Deref(game, out isLoadingScreen);

                        if (isLoadingScreen)
                        {
                            isLoading = true;
                        }

                        bool isInLoadScreenFadeOut;
                        _isInLoadScreenFadeOutPtr.Deref(game, out isInLoadScreenFadeOut);

                        bool isInTamriel;
                        _isInTamriel.Deref(game, out isInTamriel);

                        int world_X;
                        _world_XPtr.Deref(game, out world_X);

                        int world_Y;
                        _world_YPtr.Deref(game, out world_Y);

                        bool isAlduinDefeated;
                        _isAlduinDefeatedPtr.Deref(game, out isAlduinDefeated);

                        if (isLoading != prevIsLoading)
                        {
                            if (isLoading)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Load Start - {0}", frameCounter));

                                loadingStarted = true;

                                // pause game timer
                                _uiThread.Post(d => {
                                    if (this.OnLoadStarted != null)
                                    {
                                        this.OnLoadStarted(this, EventArgs.Empty);
                                    }
                                }, null);
                            }
                            else
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Load End - {0}", frameCounter));

                                if (loadingStarted)
                                {
                                    loadingStarted = false;

                                    // unpause game timer
                                    _uiThread.Post(d => {
                                        if (this.OnLoadFinished != null)
                                        {
                                            this.OnLoadFinished(this, EventArgs.Empty);
                                        }
                                    }, null);
                                }
                            }
                        }

                        if (isLoadingScreen != prevIsLoadingScreen)
                        {
                            if (isLoadingScreen)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen Start - {0}", frameCounter));

                                loadingScreenStarted = true;

                                // nothing currently
                                // _uiThread.Post(d =>
                                // {
                                //     if (this.OnLoadScreenStarted != null)
                                //     {
                                //         this.OnLoadScreenStarted(this, EventArgs.Empty);
                                //     }
                                // }, null);

                                // if loadscreen starts while leaving helgen
                                if (!isInTamriel && world_X == -2 && world_Y == -5 && !splitStates[(int)SplitArea.Helgen])
                                {
                                    // Helgen split
                                    Trace.WriteLine(String.Format("[NoLoads] Helgen Split - {0}", frameCounter));
                                    _uiThread.Post(d =>
                                    {
                                        if (this.OnSplitCompleted != null)
                                        {
                                            this.OnSplitCompleted(this, SplitArea.Helgen);
                                        }
                                    }, null);
                                }
                            }
                            else
                            {
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen End - {0}", frameCounter));

                                if (loadingScreenStarted)
                                {
                                    loadingScreenStarted = false;

                                    // nothing currently
                                    // _uiThread.Post(d =>
                                    // {
                                    //     if (this.OnLoadScreenFinished != null)
                                    //     {
                                    //         this.OnLoadScreenFinished(this, EventArgs.Empty);
                                    //     }
                                    // }, null);
                                }
                            }
                        }

                        // if loadscreen fadeout finishes in helgen
                        if (isInLoadScreenFadeOut != prevIsInLoadScreenFadeOut && isInTamriel && world_X == 3 && world_Y == -20)
                        {
                            if (!isInLoadScreenFadeOut && prevIsInLoadScreenFadeOut)
                            {
                                // reset
                                Trace.WriteLine(String.Format("[NoLoads] Reset - {0}", frameCounter));
                                _uiThread.Post(d =>
                                {
                                    if (this.OnFirstLevelLoading != null)
                                    {
                                        this.OnFirstLevelLoading(this, EventArgs.Empty);
                                    }
                                }, null);

                                // start
                                Trace.WriteLine(String.Format("[NoLoads] Start - {0}", frameCounter));
                                _uiThread.Post(d =>
                                {
                                    if (this.OnPlayerGainedControl != null)
                                    {
                                        this.OnPlayerGainedControl(this, EventArgs.Empty);
                                    }
                                }, null);
                            }
                        }

                        // if alduin is defeated in sovngarde
                        if (isAlduinDefeated != prevIsAlduinDefeated && isAlduinDefeated && !splitStates[(int)SplitArea.AlduinDefeated]
                            && !isInTamriel && ((world_X == 15 && world_Y == 19) || (world_X == 15 && world_Y == 20)))
                        {
                            // AlduinDefeated split
                            Trace.WriteLine(String.Format("[NoLoads] AlduinDefeated Split - {0}", frameCounter));
                            _uiThread.Post(d =>
                            {
                                if (this.OnSplitCompleted != null)
                                {
                                    this.OnSplitCompleted(this, SplitArea.AlduinDefeated);
                                }
                            }, null);
                        }

                        prevIsLoading = isLoading;
                        prevIsLoadingScreen = isLoadingScreen;
                        prevIsAlduinDefeated = isAlduinDefeated;
                        prevIsInLoadScreenFadeOut = isInLoadScreenFadeOut;
                        frameCounter++;

                        Thread.Sleep(15);

                        if (_cancelSource.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                    Thread.Sleep(1000);
                }
            }
        }

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
