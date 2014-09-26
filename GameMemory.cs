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
            HailSithisCompleted,
            GloryOfTheDeadCompleted,
            UnderNewManagenementCompleted,
            TheEyeOfMagnusCompleted,
            AlduinDefeated
        }

        public event EventHandler OnFirstLevelLoading;
        public event EventHandler OnPlayerGainedControl;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        // public event EventHandler OnLoadScreenStarted;
        // public event EventHandler OnLoadScreenFinished;
        public delegate void SplitCompletedEventHandler(object sender, SplitArea type);
        public event SplitCompletedEventHandler OnSplitCompleted;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;

        private DeepPointer _isLoadingPtr;
        private DeepPointer _isLoadingScreenPtr;
        private DeepPointer _isInLoadScreenFadeOutPtr;
        private DeepPointer _locationID;
        private DeepPointer _world_XPtr;
        private DeepPointer _world_YPtr;
        private DeepPointer _isAlduinDefeatedPtr;
        private DeepPointer _guildsCompleted;
        private DeepPointer _isGloryOfTheDeadCompleted;
        private DeepPointer _isTheEyeOfMagnusCompleted;

        private enum Locations
        {
            Tamriel = 0x0000003C,
            Sovngarde = 0x0002EE41,
            HelgenKeep01 = 0x0005DE24,
            DawnstarSanctuary = 0x000193EE,
            ThievesGuildHQ = 0x00016BD0,
            YsgramorsTomb = 0x00015254,
            HallOfTheElements = 0x0001380E
        }

        private enum ExpectedDllSizes
        {
            SkyrimSteam = 27336704,
            SkyrimCracked = 26771456,
        }

        public bool[] splitStates { get; set; }
                
        public void resetSplitStates()
        {
            for (int i = 0; i <= (int)SplitArea.AlduinDefeated; i++)
            {
                splitStates[i] = false;
            }
        }

        public GameMemory()
        {
            splitStates = new bool[(int)SplitArea.AlduinDefeated + 1];

            // Loads
            _isLoadingPtr = new DeepPointer(0x17337CC); // == 1 if a load is happening (any except loading screens in Helgen for some reason)
            _isLoadingScreenPtr = new DeepPointer(0xEE3561); // == 1 if in a loading screen
            // _isPausedPtr = new DeepPointer(0x172E85F); // == 1 if in a menu or a loading screen
            _isInLoadScreenFadeOutPtr = new DeepPointer(0x172EE2E); // == 1 from the fade out of a loading, it goes back to 0 once control is gained

            // Position
            _locationID = new DeepPointer(0x01738308, 0x4, 0x78, 0x670, 0xEC); // ID of the current location (see http://steamcommunity.com/sharedfiles/filedetails/?id=148834641 or http://www.skyrimsearch.com/cells.php)
            _world_XPtr = new DeepPointer(0x0172E864, 0x64); // X world position (cell)
            _world_YPtr = new DeepPointer(0x0172E864, 0x68); // Y world position (cell)

            // Game state
            _isAlduinDefeatedPtr = new DeepPointer(0x1711608); // == 1 when last blow is struck on alduin
            _guildsCompleted = new DeepPointer(0x00EE6C34, 0x3F0); // == 1 once Hail Sithis quest is completed
            _isGloryOfTheDeadCompleted = new DeepPointer(0x00EE6C34, 0x378);
            _isTheEyeOfMagnusCompleted = new DeepPointer(0x0172E2DC, 0x13c);
            // _playerHasControlPtr = new DeepPointer(0x74814710); // == 1 when player has full control

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

                    bool prevIsLoading = false;
                    bool prevIsLoadingScreen = false;
                    bool prevIsAlduinDefeated = false;
                    int prevGuildsCompleted = 0;
                    bool prevIsGloryOfTheDeadCompleted = false;
                    bool prevIsTheEyeOfMagnusCompleted = false;
                    bool prevIsInLoadScreenFadeOut = false;

                    bool loadingStarted = false;
                    bool loadingScreenStarted = false;
                    bool helgenFadeoutStarted = false;

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

                        int locationID;
                        _locationID.Deref(game, out locationID);

                        int world_X;
                        _world_XPtr.Deref(game, out world_X);

                        int world_Y;
                        _world_YPtr.Deref(game, out world_Y);

                        bool isAlduinDefeated;
                        _isAlduinDefeatedPtr.Deref(game, out isAlduinDefeated);

                        int guildsCompleted;
                        _guildsCompleted.Deref(game, out guildsCompleted);

                        bool isGloryOfTheDeadCompleted;
                        _isGloryOfTheDeadCompleted.Deref(game, out isGloryOfTheDeadCompleted);

                        bool isTheEyeOfMagnusCompleted;
                        _isTheEyeOfMagnusCompleted.Deref(game, out isTheEyeOfMagnusCompleted);

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
                                if (locationID == (int)Locations.HelgenKeep01)
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

                        if (isInLoadScreenFadeOut != prevIsInLoadScreenFadeOut)
                        {
                            if(isInLoadScreenFadeOut)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Fadeout started - {0}", frameCounter));
                                if (isLoadingScreen)
                                {
                                    helgenFadeoutStarted = true;
                                }
                            }
                            else
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Fadeout ended - {0}", frameCounter));
                                // if loadscreen fadeout finishes in helgen
                                if (prevIsInLoadScreenFadeOut && helgenFadeoutStarted
                                    && locationID == (int)Locations.Tamriel && world_X == 3 && world_Y == -20)
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
                                helgenFadeoutStarted = false;
                            }
                        }

                        // if alduin is defeated in sovngarde
                        if (isAlduinDefeated != prevIsAlduinDefeated && isAlduinDefeated && locationID == (int)Locations.Sovngarde)
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

                        // if a guild is completed
                        if (guildsCompleted == prevGuildsCompleted + 1)
                        {
                            // while in Dawnstar's Sanctuary
                            if (locationID == (int)Locations.DawnstarSanctuary)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] HailSithisCompleted Split - {0}", frameCounter));
                                _uiThread.Post(d =>
                                {
                                    if (this.OnSplitCompleted != null)
                                    {
                                        this.OnSplitCompleted(this, SplitArea.HailSithisCompleted);
                                    }
                                }, null);
                            }
                            // while in the Thieves Guild Headquarters
                            else if (locationID == (int)Locations.ThievesGuildHQ)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] UnderNewManagenementCompleted Split - {0}", frameCounter));
                                _uiThread.Post(d =>
                                {
                                    if (this.OnSplitCompleted != null)
                                    {
                                        this.OnSplitCompleted(this, SplitArea.UnderNewManagenementCompleted);
                                    }
                                }, null);
                            }
                            // while in Ysgramor's Tomb
                            else if (locationID == (int)Locations.YsgramorsTomb)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] GloryOfTheDeadCompleted Split - {0}", frameCounter));
                                _uiThread.Post(d =>
                                {
                                    if (this.OnSplitCompleted != null)
                                    {
                                        this.OnSplitCompleted(this, SplitArea.GloryOfTheDeadCompleted);
                                    }
                                }, null);
                            }
                            // while in the Hall of the Elements
                            else if (locationID == (int)Locations.HallOfTheElements)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] TheEyeOfMagnusCompleted Split - {0}", frameCounter));
                                _uiThread.Post(d =>
                                {
                                    if (this.OnSplitCompleted != null)
                                    {
                                        this.OnSplitCompleted(this, SplitArea.TheEyeOfMagnusCompleted);
                                    }
                                }, null);
                            }
                        }
                        prevIsLoading = isLoading;
                        prevIsLoadingScreen = isLoadingScreen;
                        prevIsAlduinDefeated = isAlduinDefeated;
                        prevGuildsCompleted = guildsCompleted;
                        prevIsGloryOfTheDeadCompleted = isGloryOfTheDeadCompleted;
                        prevIsTheEyeOfMagnusCompleted = isTheEyeOfMagnusCompleted;
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
