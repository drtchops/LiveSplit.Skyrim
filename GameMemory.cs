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
            None,
            Helgen,
            DarkBrotherhoodQuestlineCompleted,
            CompanionsQuestlineCompleted,
            ThievesGuildQuestlineCompleted,
            CollegeQuestlineCompleted,
            AlduinDefeated
        }

        public event EventHandler OnFirstLevelLoading;
        public event EventHandler OnPlayerGainedControl;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        // public event EventHandler OnLoadScreenStarted;
        // public event EventHandler OnLoadScreenFinished;
        public delegate void SplitCompletedEventHandler(object sender, SplitArea type, uint frame);
        public event SplitCompletedEventHandler OnSplitCompleted;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;

        private DeepPointer _isLoadingPtr;
        private DeepPointer _isLoadingScreenPtr;
        private DeepPointer _isInFadeOutPtr;
        private DeepPointer _locationID;
        private DeepPointer _world_XPtr;
        private DeepPointer _world_YPtr;
        private DeepPointer _isAlduinDefeatedPtr;
        private DeepPointer _questlinesCompleted;
        private DeepPointer _companionsQuestsCompletedPtr;
        private DeepPointer _collegeQuestsCompletedPtr;
        private DeepPointer _darkBrotherhoodQuestsCompletedPtr;
        private DeepPointer _thievesGuildQuestsCompletedPtr;
        private DeepPointer _isInEscapeMenuPtr;

        private enum Locations
        {
            Tamriel = 0x0000003C,
            Sovngarde = 0x0002EE41,
            HelgenKeep01 = 0x0005DE24,
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
            _isInFadeOutPtr = new DeepPointer(0x172EE2E); // == 1 when in a fadeout, it goes back to 0 once control is gained

            // Position
            _locationID = new DeepPointer(0x01738308, 0x4, 0x78, 0x670, 0xEC); // ID of the current location (see http://steamcommunity.com/sharedfiles/filedetails/?id=148834641 or http://www.skyrimsearch.com/cells.php)
            _world_XPtr = new DeepPointer(0x0172E864, 0x64); // X world position (cell)
            _world_YPtr = new DeepPointer(0x0172E864, 0x68); // Y world position (cell)

            // Game state
            _isAlduinDefeatedPtr = new DeepPointer(0x1711608); // == 1 when last blow is struck on alduin
            _questlinesCompleted = new DeepPointer(0x00EE6C34, 0x3F0); // number of questlines completed (from ingame stats)
            _companionsQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x378); // number of companions quests completed (from ingame stats)
            _collegeQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x38c); // number of college of winterhold quests completed (from ingame stats)
            _darkBrotherhoodQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x3b4); // number of dark brotherhood quests completed (from ingame stats)
            _thievesGuildQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x3a0); // number of thieves' guild quests completed (from ingame stats)
            _isInEscapeMenuPtr = new DeepPointer(0x172E85E); // == 1 when in the pause menu or level up menu

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
                    bool prevIsInFadeOut = false;
                    bool prevIsAlduinDefeated = false;
                    int prevQuestlinesCompleted = 0;
                    int prevCompanionsQuestsCompleted = 0;
                    int prevCollegeQuestsCompleted = 0;
                    int prevDarkBrotherhoodQuestsCompleted = 0;
                    int prevThievesGuildQuestsCompleted = 0;
                    bool prevIsInEscapeMenu = false;
                    int prevLocationID = 0;

                    bool loadingStarted = false;
                    bool loadingScreenStarted = false;
                    bool loadScreenFadeoutStarted = false;

                    SplitArea lastQuestCompleted = SplitArea.None;
                    uint lastQuestframeCounter = 0;

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

                        bool isInFadeOut;
                        _isInFadeOutPtr.Deref(game, out isInFadeOut);

                        int locationID;
                        _locationID.Deref(game, out locationID);

                        int world_X;
                        _world_XPtr.Deref(game, out world_X);

                        int world_Y;
                        _world_YPtr.Deref(game, out world_Y);

                        bool isAlduinDefeated;
                        _isAlduinDefeatedPtr.Deref(game, out isAlduinDefeated);

                        int questlinesCompleted;
                        _questlinesCompleted.Deref(game, out questlinesCompleted);

                        int companionsQuestsCompleted;
                        _companionsQuestsCompletedPtr.Deref(game, out companionsQuestsCompleted);

                        int collegeQuestsCompleted;
                        _collegeQuestsCompletedPtr.Deref(game, out collegeQuestsCompleted);

                        int darkBrotherhoodQuestsCompleted;
                        _darkBrotherhoodQuestsCompletedPtr.Deref(game, out darkBrotherhoodQuestsCompleted);

                        int thievesGuildQuestsCompleted;
                        _thievesGuildQuestsCompletedPtr.Deref(game, out thievesGuildQuestsCompleted);

                        bool isInEscapeMenu;
                        _isInEscapeMenuPtr.Deref(game, out isInEscapeMenu);

                        if (isLoading != prevIsLoading)
                        {
                            if (isLoading)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Load Start - {0}", frameCounter));

                                loadingStarted = true;

                                // pause game timer
                                _uiThread.Post(d =>
                                {
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
                                    _uiThread.Post(d =>
                                    {
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

                                if (isInFadeOut)
                                {
                                    loadScreenFadeoutStarted = true;
                                }

                                // nothing currently
                                // _uiThread.Post(d =>
                                // {
                                //     if (this.OnLoadScreenStarted != null)
                                //     {
                                //         this.OnLoadScreenStarted(this, EventArgs.Empty);
                                //     }
                                // }, null);

                                // if it isn't a loadscreen from loading a save
                                if (!isInEscapeMenu)
                                {
                                    // if loadscreen starts while leaving helgen
                                    if (locationID == (int)Locations.HelgenKeep01 && world_X == -2 && world_Y == -5)
                                    {
                                        // Helgen split
                                        _uiThread.Post(d =>
                                        {
                                            if (this.OnSplitCompleted != null)
                                            {
                                                this.OnSplitCompleted(this, SplitArea.Helgen, frameCounter);
                                            }
                                        }, null);
                                    }
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

                        if (isInFadeOut != prevIsInFadeOut)
                        {
                            if (isInFadeOut)
                            {
                                Debug.WriteLine(String.Format("[NoLoads] Fadeout started - {0}", frameCounter));
                                if (isLoadingScreen)
                                {
                                    loadScreenFadeoutStarted = true;
                                }
                            }
                            else
                            {
                                Debug.WriteLine(String.Format("[NoLoads] Fadeout ended - {0}", frameCounter));
                                // if loadscreen fadeout finishes in helgen
                                if (prevIsInFadeOut && loadScreenFadeoutStarted
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
                                loadScreenFadeoutStarted = false;
                            }
                        }

                        // if alduin is defeated in sovngarde
                        if (isAlduinDefeated != prevIsAlduinDefeated && isAlduinDefeated && locationID == (int)Locations.Sovngarde)
                        {
                            // AlduinDefeated split
                            _uiThread.Post(d =>
                            {
                                if (this.OnSplitCompleted != null)
                                {
                                    this.OnSplitCompleted(this, SplitArea.AlduinDefeated, frameCounter);
                                }
                            }, null);
                        }

                        // reset lastQuest 100 frames (1.5 seconds) after a completion to avoid splitting on a wrong questline.
                        if (frameCounter >= lastQuestframeCounter + 100 && lastQuestCompleted != SplitArea.None)
                        {
                            lastQuestCompleted = SplitArea.None;
                        }

                        if (darkBrotherhoodQuestsCompleted > prevDarkBrotherhoodQuestsCompleted)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A Dark Brotherhood quest has been completed - {0}", frameCounter));
                            lastQuestCompleted = SplitArea.DarkBrotherhoodQuestlineCompleted;
                            lastQuestframeCounter = frameCounter;
                        }
                        else if (thievesGuildQuestsCompleted > prevThievesGuildQuestsCompleted)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A Thieves' Guild quest has been completed - {0}", frameCounter));
                            lastQuestCompleted = SplitArea.ThievesGuildQuestlineCompleted;
                            lastQuestframeCounter = frameCounter;
                        }
                        else if (companionsQuestsCompleted > prevCompanionsQuestsCompleted)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A Companions quest has been completed - {0}", frameCounter));
                            lastQuestCompleted = SplitArea.CompanionsQuestlineCompleted;
                            lastQuestframeCounter = frameCounter;
                        }
                        else if (collegeQuestsCompleted > prevCollegeQuestsCompleted)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A College of Winterhold quest has been completed - {0}", frameCounter));
                            lastQuestCompleted = SplitArea.CollegeQuestlineCompleted;
                            lastQuestframeCounter = frameCounter;
                        }

                        // if a questline is completed
                        if (questlinesCompleted > prevQuestlinesCompleted)
                        {
                            Debug.WriteLineIf(lastQuestCompleted == SplitArea.None, String.Format("[NoLoads] A questline has been completed. - {0}", frameCounter));
                            _uiThread.Post(d =>
                            {
                                if (this.OnSplitCompleted != null)
                                {
                                    this.OnSplitCompleted(this, lastQuestCompleted, frameCounter);
                                }
                            }, null);
                        }

                        Debug.WriteLineIf(locationID != prevLocationID, String.Format("[NoLoads] LocationID changed from {0} to {1} - {2}", prevLocationID.ToString("X8"), locationID.ToString("X8"), frameCounter));
                        Debug.WriteLineIf(isInEscapeMenu != prevIsInEscapeMenu, String.Format("[NoLoads] isInEscapeMenu changed to {0} - {1}", isInEscapeMenu, frameCounter));

                        prevIsLoading = isLoading;
                        prevIsLoadingScreen = isLoadingScreen;
                        prevIsInFadeOut = isInFadeOut;
                        prevIsAlduinDefeated = isAlduinDefeated;
                        prevQuestlinesCompleted = questlinesCompleted;
                        prevCompanionsQuestsCompleted = companionsQuestsCompleted;
                        prevCollegeQuestsCompleted = collegeQuestsCompleted;
                        prevDarkBrotherhoodQuestsCompleted = darkBrotherhoodQuestsCompleted;
                        prevThievesGuildQuestsCompleted = thievesGuildQuestsCompleted;
                        prevIsInEscapeMenu = isInEscapeMenu;
                        prevLocationID = locationID;
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
