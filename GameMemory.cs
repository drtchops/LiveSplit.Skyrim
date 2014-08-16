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
        // public event EventHandler OnFirstLevelLoading;
        // public event EventHandler OnPlayerGainedControl;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        public event EventHandler OnLoadScreenStarted;
        public event EventHandler OnLoadScreenFinished;
        public event EventHandler OnAlduinDefeated;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;

        // private DeepPointer _currentLevelPtr;
        private DeepPointer _isLoadingPtr;
        private DeepPointer _isLoadingScreenPtr;
        private DeepPointer _isAlduinDefeatedPtr;
        // private int _stringBase;

        private enum ExpectedDllSizes
        {
            SkyrimSteam = 27336704,
            SkyrimCracked = 26771456,
        }

        public GameMemory()
        {
            _isLoadingPtr = new DeepPointer("TESV.exe", 0x17337CC); // == 1 if a loading is happening (any except loading screens in Helgen for some reason)
            _isLoadingScreenPtr = new DeepPointer("TESV.exe", 0xEE3561); // == 1 if in a loading screen
            // _isPausedPtr = new DeepPointer("TESV.exe", 0x172E85F); // == 1 if in a menu or a loading screen

            _isAlduinDefeatedPtr = new DeepPointer("TESV.exe", 0x12ACF78C); // == 1 when last blow is struck on alduin
            // possible: 0x12ACF78C, 0x12FD23DB

            // possible for start of splits: 13682838 == 18A4A

            _ignorePIDs = new List<int>();
        }

        public void StartMonitoring()
        {
            if (_thread != null && _thread.Status == TaskStatus.Running)
                throw new InvalidOperationException();
            if (!(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
                throw new InvalidOperationException("SynchronizationContext.Current is not a UI thread.");

            _uiThread = SynchronizationContext.Current;
            _cancelSource = new CancellationTokenSource();
            _thread = Task.Factory.StartNew(MemoryReadThread);
        }

        public void Stop()
        {
            if (_cancelSource == null || _thread == null || _thread.Status != TaskStatus.Running)
                return;

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
                            return;
                    }

                    Trace.WriteLine("[NoLoads] Got TESV.exe!");

                    uint frameCounter = 0;

                    // int prevCurrentLevel = 0;
                    bool prevIsLoading = false;
                    bool prevIsLoadingScreen = false;
                    bool prevIsAlduinDefeated = false;

                    bool loadingStarted = false;
                    bool loadingScreenStarted = false;

                    while (!game.HasExited)
                    {
                        // int currentLevel;
                        // _currentLevelPtr.Deref(game, out currentLevel);
                        // string currentLevelStr = GetEngineStringByID(game, currentLevel);

                        bool isLoading;
                        _isLoadingPtr.Deref(game, out isLoading);

                        bool isLoadingScreen;
                        _isLoadingScreenPtr.Deref(game, out isLoadingScreen);

                        if (isLoadingScreen)
                            isLoading = true;

                        bool isAlduinDefeated;
                        _isAlduinDefeatedPtr.Deref(game, out isAlduinDefeated);

                        // if (currentLevel != prevCurrentLevel)
                        // {
                        //     Trace.WriteLine(String.Format("{0} [NoLoads] Level Changed - {1} -> {2} '{3}'", frameCounter, prevCurrentLevel, currentLevel, currentLevelStr));

                        //     if (currentLevelStr == "l_tower_p" || currentLevelStr == "L_DLC07_BaseIntro_P" || currentLevelStr == "DLC06_Tower_P")
                        //     {
                        //         _uiThread.Post(d => {
                        //             if (this.OnFirstLevelLoading != null)
                        //                 this.OnFirstLevelLoading(this, EventArgs.Empty);
                        //         }, null);
                        //     }
                        // }

                        if (isLoading != prevIsLoading)
                        {
                            if (isLoading)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Load Start - {0}", frameCounter));

                                loadingStarted = true;

                                _uiThread.Post(d => {
                                    if (this.OnLoadStarted != null)
                                        this.OnLoadStarted(this, EventArgs.Empty);
                                }, null);
                            }
                            else
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Load End - {0}", frameCounter));

                                if (loadingStarted)
                                {
                                    loadingStarted = false;

                                    _uiThread.Post(d => {
                                        if (this.OnLoadFinished != null)
                                            this.OnLoadFinished(this, EventArgs.Empty);
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

                                _uiThread.Post(d =>
                                {
                                    if (this.OnLoadScreenStarted != null)
                                        this.OnLoadScreenStarted(this, EventArgs.Empty);
                                }, null);
                            }
                            else
                            {
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen End - {0}", frameCounter));

                                if (loadingScreenStarted)
                                {
                                    loadingScreenStarted = false;

                                    _uiThread.Post(d =>
                                    {
                                        if (this.OnLoadScreenFinished != null)
                                            this.OnLoadScreenFinished(this, EventArgs.Empty);
                                    }, null);
                                }

                                // if (((currentMovie == "LoadingEmpressTower" || currentMovie == "INTRO_LOC") && currentLevelStr == "l_tower_p")
                                //     || (currentMovie == "Loading" || currentMovie == "LoadingDLC06Tower") && currentLevelStr == "DLC06_Tower_P") // KoD
                                // {
                                //     _uiThread.Post(d => {
                                //         if (this.OnPlayerGainedControl != null)
                                //             this.OnPlayerGainedControl(this, EventArgs.Empty);
                                //     }, null);
                                // }
                            }
                        }

                        //if (isAlduinDefeated != prevIsAlduinDefeated && isAlduinDefeated)
                        //{
                        //    _uiThread.Post(d => {
                        //        if (this.OnAlduinDefeated != null)
                        //            this.OnAlduinDefeated(this, EventArgs.Empty);
                        //    }, null);
                        //}

                        // prevCurrentLevel = currentLevel;
                        prevIsLoading = isLoading;
                        prevIsLoadingScreen = isLoadingScreen;
                        prevIsAlduinDefeated = isAlduinDefeated;
                        frameCounter++;

                        Thread.Sleep(15);

                        if (_cancelSource.IsCancellationRequested)
                            return;
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
                return null;

            if (game.MainModule.ModuleMemorySize != (int)ExpectedDllSizes.SkyrimSteam && game.MainModule.ModuleMemorySize != (int)ExpectedDllSizes.SkyrimCracked)
            {
                _ignorePIDs.Add(game.Id);
                _uiThread.Send(d => MessageBox.Show("Unexpected game version. Skyrim 1.9.32.0.8 is required.", "LiveSplit.Skyrim",
                MessageBoxButtons.OK, MessageBoxIcon.Error), null);
                return null;
            }

            return game;
        }

        // string GetEngineStringByID(Process p, int id)
        // {
        //     string str;
        //     var ptr = new DeepPointer(_stringBase, (id*4), 0x10);
        //     ptr.Deref(p, out str, 32);
        //     return str;
        // }
    }
}
