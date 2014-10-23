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
            Whiterun,
            ThalmorEmbassy,
            Esbern,
            Riverwood,
            TheWall,
            Septimus,
            MzarkTower,
            ClearSky,
            HorseClimb,
            CutsceneStart,
            CutsceneEnd,
            Alduin1,
            HighHrothgar,
            Solitude,
            Windhelm,
            Council,
            Odahviing,
            EnterSovngarde,
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
        private SkyrimSettings _settings;

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
        private DeepPointer _mainQuestsCompletedPtr;
        private DeepPointer _wordsOfPowerLearnedPtr;
        private DeepPointer _Alduin1HealthPtr;
        private DeepPointer _locationsDiscoveredPtr;
        private DeepPointer _arePlayerControlsDisablePtr;

        private enum Locations
        {
            Tamriel = 0x0000003C,
            Sovngarde = 0x0002EE41,
            HelgenKeep01 = 0x0005DE24,
            WhiterunWorld = 0x0001A26F,
            ThalmorEmbassy02 = 0x0007DCFC,
            WhiterunDragonsreach = 0x000165A3,
            RiftenWorld = 0x00016BB4,
            RiftenRatway01 = 0x0003B698,
            RiverwoodSleepingGiantInn = 0x000133C6,
            KarthspireRedoubtWorld = 0x00035699,
            SkyHavenTemple = 0x000161EB,
            SeptimusSignusOutpost = 0x0002D4E4,
            TowerOfMzark = 0x0002D4E3,
            HighHrothgar = 0x00087764,
            SolitudeWorld = 0x00037EDF,
            SolitudeCastleDour = 0x000213A0,
            WindhelmWorld = 0x0001691D,
            WindhelmPalaceoftheKings = 0x0001677C,
            SkuldafnWorld = 0x000278DD,
        }

        private enum ExpectedDllSizes
        {
            SkyrimSteam = 27336704,
            SkyrimCracked = 26771456,
        }

        public bool[] splitStates { get; set; }
        bool isSkyHavenTempleVisited = false;
        bool isAlduin1Defeated = false;
        int leaveSleepingGiantInnCounter = 0;
        bool isCouncilDone = false;

        public void resetSplitStates()
        {
            for (int i = 0; i <= (int)SplitArea.AlduinDefeated; i++)
            {
                splitStates[i] = false;
            }
            isSkyHavenTempleVisited = false;
            isAlduin1Defeated = false;
            leaveSleepingGiantInnCounter = 0;
            isCouncilDone = false;
        }

        public GameMemory(SkyrimSettings componentSettings)
        {
            _settings = componentSettings;
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
            _mainQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x350); // number of main quests completed (from ingame stats)
            _wordsOfPowerLearnedPtr = new DeepPointer(0x00EE6C34, 0x558); // "Words Of Power Learned" from ingame stats
            _Alduin1HealthPtr = new DeepPointer(0x00F41764, 0x74, 0x30, 0x30, 0x1c); // Alduin 1's health (if it's at 0 it's 99% of the time because it can't be found)
            _locationsDiscoveredPtr = new DeepPointer(0x00EE6C34, 0x170); // number of locations discovered (from ingame stats)
            _arePlayerControlsDisablePtr = new DeepPointer(0x172EF30, 0xf); // == 1 when player controls have been disabled (not necessarily all controls)

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
                    bool prevIsAlduin2Defeated = false;
                    int prevQuestlinesCompleted = 0;
                    int prevCompanionsQuestsCompleted = 0;
                    int prevCollegeQuestsCompleted = 0;
                    int prevDarkBrotherhoodQuestsCompleted = 0;
                    int prevThievesGuildQuestsCompleted = 0;
                    bool prevIsInEscapeMenu = false;
                    int prevLocationID = 0;
                    int prevWorld_X = 0;
                    int prevWorld_Y = 0;
                    int prevMainQuestsCompleted = 0;
                    int prevLocationsDiscovered = 0;
                    bool prevArePlayerControlsDisabled = false;

                    bool loadingStarted = false;
                    bool loadingScreenStarted = false;
                    bool loadScreenFadeoutStarted = false;
                    bool isLoadingSaveFromMenu = false;
                    int loadScreenStartLocationID = 0;
                    int loadScreenStartWorld_X = 0;
                    int loadScreenStartWorld_Y = 0;
                    bool isWaitingLocationOrCoordsUpdate = false;
                    bool isWaitingLocationIDUpdate = false;

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

                        bool isAlduin2Defeated;
                        _isAlduinDefeatedPtr.Deref(game, out isAlduin2Defeated);

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

                        int mainquestsCompleted;
                        _mainQuestsCompletedPtr.Deref(game, out mainquestsCompleted);

                        int wordsOfPowerLearned;
                        _wordsOfPowerLearnedPtr.Deref(game, out wordsOfPowerLearned);

                        float alduin1Health;
                        _Alduin1HealthPtr.Deref(game, out alduin1Health);

                        int locationsDiscovered;
                        _locationsDiscoveredPtr.Deref(game, out locationsDiscovered);

                        bool arePlayerControlsDisabled;
                        _arePlayerControlsDisablePtr.Deref(game, out arePlayerControlsDisabled);

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

                                    if (!loadScreenFadeoutStarted)
                                    {
                                        if (locationID == (int)Locations.Tamriel && world_X == 13 && (world_Y == -10 || world_Y == -9) && wordsOfPowerLearned == 3)
                                        {
                                            Split(SplitArea.ClearSky, frameCounter);
                                        }
                                    }   

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
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen Start at {0} X: {1} Y: {2} - {3}", locationID.ToString("X8"), world_X, world_Y, frameCounter));

                                loadingScreenStarted = true;
                                loadScreenStartLocationID = locationID;
                                loadScreenStartWorld_X = world_X;
                                loadScreenStartWorld_Y = world_Y;

                                if (isInFadeOut)
                                {
                                    loadScreenFadeoutStarted = true;
                                }
                                
                                if (isInEscapeMenu)
                                {
                                    isLoadingSaveFromMenu = true;
                                }

                                // if it isn't a loadscreen from loading a save
                                if (!isLoadingSaveFromMenu)
                                {
                                    isWaitingLocationOrCoordsUpdate = true;
                                    isWaitingLocationIDUpdate = true;

                                    // if loadscreen starts while leaving helgen
                                    if (loadScreenStartLocationID == (int)Locations.HelgenKeep01 && loadScreenStartWorld_X == -2 && loadScreenStartWorld_Y == -5)
                                    {
                                        // Helgen split
                                        Split(SplitArea.Helgen, frameCounter);
                                    }
                                    // if loadscreen starts in around the carriage of Whiterun Stables
                                    else if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == 4 && (loadScreenStartWorld_Y == -3 || loadScreenStartWorld_Y == -4) &&
                                            _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS)
                                    {
                                        Split(SplitArea.Whiterun, frameCounter);
                                    }
                                    // if loadscreen starts in Karthspire and Sky Haven Temple has been entered at least once
                                    else if (loadScreenStartLocationID == (int)Locations.KarthspireRedoubtWorld && isSkyHavenTempleVisited &&
                                        (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                                    {
                                        Split(SplitArea.TheWall, frameCounter);
                                    }
                                    // if loadscreen starts in Paarthurnax' mountain whereabouts
                                    else if (isAlduin1Defeated && loadScreenStartLocationID == (int)Locations.Tamriel && ((loadScreenStartWorld_X == 14 && loadScreenStartWorld_Y == -12) ||
                                        (loadScreenStartWorld_X == 14 && loadScreenStartWorld_Y == -13) || (loadScreenStartWorld_X == 13 && loadScreenStartWorld_Y == -12) ||
                                            (loadScreenStartWorld_X == 13 && loadScreenStartWorld_Y == -13)) &&
                                                (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                                    {
                                        Split(SplitArea.Alduin1, frameCounter);
                                    }
                                }
                                else
                                {
                                    isWaitingLocationOrCoordsUpdate = false;
                                    isWaitingLocationIDUpdate = false;
                                }
                            }
                            else
                            {
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen End - {0}", frameCounter));

                                if (loadingScreenStarted)
                                {
                                    loadingScreenStarted = false;
                                    isLoadingSaveFromMenu = false;
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
                                if (prevIsInFadeOut && loadScreenFadeoutStarted &&
                                    locationID == (int)Locations.Tamriel && world_X == 3 && world_Y == -20)
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

                        if ((locationID != prevLocationID || world_X != prevWorld_X || world_Y != prevWorld_Y) && isWaitingLocationOrCoordsUpdate)
                        {
                            isWaitingLocationOrCoordsUpdate = false;

                            // if loadscreen starts while in front of the door of Thalmor Embassy and doesn't end inside the Embassy
                            if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == -20 && loadScreenStartWorld_Y == 28 &&
                                locationID == (int)Locations.Tamriel &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.ThalmorEmbassy, frameCounter);
                            }
                            // if loadscreen starts while in front of the door of Thalmor Embassy and doesn't end inside the Embassy
                            else if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == -20 && loadScreenStartWorld_Y == 28 &&
                                locationID != (int)Locations.ThalmorEmbassy02 &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.ThalmorEmbassy, frameCounter);
                            }
                            // if loadscreen starts while in front of the Sleeping Giant Inn and doesn't end inside it
                            else if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == 5 && loadScreenStartWorld_Y == -11 &&
                                locationID != (int)Locations.RiverwoodSleepingGiantInn &&
                                (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.Riverwood, frameCounter);
                            }
                            // if loadscreen starts outside Septimus' Outpost and doesn't end inside it
                            else if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == 28 && loadScreenStartWorld_Y == 34 &&
                                locationID != (int)Locations.SeptimusSignusOutpost &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.Septimus, frameCounter);
                            }
                            // if loadscreen starts outside Mzark Tower and doesn't end inside it
                            else if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == 6 && loadScreenStartWorld_Y == 11 &&
                                locationID != (int)Locations.TowerOfMzark &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.MzarkTower, frameCounter);
                            }
                            // if loadscreen starts in High Hrothgar's whereabouts and doesn't end inside
                            else if (loadScreenStartLocationID == (int)Locations.Tamriel && loadScreenStartWorld_X == 13 &&
                                (loadScreenStartWorld_Y == -9 || loadScreenStartWorld_Y == -10) && 
                                    locationID != (int)Locations.HighHrothgar)
                            {
                                if (!splitStates[(int)SplitArea.HighHrothgar])
                                {
                                    Split(SplitArea.HighHrothgar, frameCounter);
                                }
                                else if (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH)
                                {
                                    Split(SplitArea.Council, frameCounter);
                                }
                            }
                        }

                        if (locationID != prevLocationID && isWaitingLocationIDUpdate)
                        {
                            isWaitingLocationIDUpdate = false;

                            if (locationID == (int)Locations.SkyHavenTemple)
                            {
                                isSkyHavenTempleVisited = true;
                            }

                            // if loadscreen starts in dragonsreach and ends in whiterun
                            if (loadScreenStartLocationID == (int)Locations.WhiterunDragonsreach &&
                                locationID == (int)Locations.WhiterunWorld && world_X == 6 && world_Y == 0 && _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.Whiterun, frameCounter);
                            }
                            // if loadscreen starts in whiterun and doesn't end in dragonsreach
                            else if (loadScreenStartLocationID == (int)Locations.WhiterunWorld && loadScreenStartWorld_X == 6 && loadScreenStartWorld_Y == 0 &&
                                locationID != (int)Locations.WhiterunDragonsreach &&
                                (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.Whiterun, frameCounter);
                            }
                            // if loadscreen starts Thalmor Embassy and ends in front of its door
                            else if (loadScreenStartLocationID == (int)Locations.ThalmorEmbassy02 &&
                                locationID == (int)Locations.Tamriel && world_X == -20 && world_Y == 28 &&
                                    _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.ThalmorEmbassy, frameCounter);
                            }
                            // if loadscreen starts while in front of the ratway door and doesn't end inside it
                            else if (loadScreenStartLocationID == (int)Locations.RiftenWorld && loadScreenStartWorld_X == 42 && loadScreenStartWorld_Y == -24 &&
                                locationID != (int)Locations.RiftenRatway01 &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.Esbern, frameCounter);
                            }
                            // if loadscreen starts inside the ratway and ends in front of its door
                            else if (loadScreenStartLocationID == (int)Locations.RiftenRatway01 &&
                                locationID == (int)Locations.RiftenWorld && world_X == 42 && world_Y == -24 &&
                                    _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.Esbern, frameCounter);
                            }
                            // if loadscreen starts while leaving the Sleeping Giant Inn and ends in front of its door
                            else if (loadScreenStartLocationID == (int)Locations.RiverwoodSleepingGiantInn &&
                                locationID == (int)Locations.Tamriel && world_X == 5 && world_Y == -11 &&
                                _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                leaveSleepingGiantInnCounter++;
                                if (leaveSleepingGiantInnCounter == 2)
                                {
                                    Split(SplitArea.Riverwood, frameCounter);
                                }
                            }
                            // if loadingscren starts in Sky Haven Temple and ends in Karthspire
                            else if (loadScreenStartLocationID == (int)Locations.SkyHavenTemple &&
                                locationID == (int)Locations.KarthspireRedoubtWorld &&
                                    _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.TheWall, frameCounter);
                            }
                            // if loadscreen starts inside Septimus' Outpost and ends in front of its door
                            else if (loadScreenStartLocationID == (int)Locations.SeptimusSignusOutpost && 
                                locationID == (int)Locations.Tamriel && world_X == 28 && world_Y == 34 &&
                                    _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.Septimus, frameCounter);
                            }
                            // if loadscreen starts inside Mzark Tower and ends outside of it
                            else if (loadScreenStartLocationID == (int)Locations.TowerOfMzark &&
                                locationID == (int)Locations.Tamriel && world_X == 6 && world_Y == 11 &&
                                    _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.MzarkTower, frameCounter);
                            }
                            // if loadscreen starts in high hrothgar and ends in front of one of its doors
                            else if (loadScreenStartLocationID == (int)Locations.HighHrothgar &&
                                locationID == (int)Locations.Tamriel && world_X == 13 && (world_Y == -9 || world_Y == -10) &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE))
                            {
                                if (!isCouncilDone)
                                {
                                    Split(SplitArea.ClearSky, frameCounter);
                                }
                                else if (isCouncilDone && _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                                {
                                    Split(SplitArea.Council, frameCounter);
                                }
                            }
                            // if loadscreen starts in Solitude in front of the door of Castle Dour and doesn't end inside it
                            else if (loadScreenStartLocationID == (int)Locations.SolitudeWorld && loadScreenStartWorld_X == -16 && loadScreenStartWorld_Y == 26 &&
                                locationID != (int)Locations.SolitudeCastleDour &&
                                (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.Solitude, frameCounter);
                            }
                            // if loadscreen starts in Solitude Castle Dour and ends outside in front of its door
                            else if (loadScreenStartLocationID == (int)Locations.SolitudeCastleDour && 
                                locationID == (int)Locations.SolitudeWorld && world_X == -16 && world_Y == 26 &&
                                _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.Solitude, frameCounter);
                            }
                            // if loadscreen starts in Windhelm and doesn't end inside
                            else if (loadScreenStartLocationID == (int)Locations.WindhelmWorld && loadScreenStartWorld_X == 32 && loadScreenStartWorld_Y == 10 &&
                                locationID != (int)Locations.WindhelmPalaceoftheKings &&
                                (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                            {
                                Split(SplitArea.Windhelm, frameCounter);
                            }
                            // if loadscreen starts in Windhelm's Palace of the Kings and ends outside
                            else if (loadScreenStartLocationID == (int)Locations.WindhelmPalaceoftheKings &&
                                locationID == (int)Locations.WindhelmWorld && world_X == 32 && world_Y == 10 &&
                                _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.Windhelm, frameCounter);
                            }
                            // if loadscreen ends in Skuldafn.
                            else if (locationID == (int)Locations.SkuldafnWorld)
                            {
                                Split(SplitArea.Odahviing, frameCounter);
                            }
                            // if loadscreen ends in Sovngarde
                            else if (locationID == (int)Locations.Sovngarde)
                            {
                                Split(SplitArea.EnterSovngarde, frameCounter);
                            }
                        }

                        if (locationsDiscovered != prevLocationsDiscovered)
                        {
                            if (locationID == (int)Locations.Tamriel && ((world_X == 14 && world_Y == -12) || (world_X == 14 && world_Y == -13) || (world_X == 13 && world_Y == -12) ||
                                (world_X == 13 && world_Y == -13)) &&
                                _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE)
                            {
                                Split(SplitArea.HorseClimb, frameCounter);
                            }
                        }

                        if (arePlayerControlsDisabled != prevArePlayerControlsDisabled)
                        {
                            if (arePlayerControlsDisabled)
                            {
                                if (locationID == (int)Locations.Tamriel && world_X == 13 && world_Y == -12 &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DRTCHOPS || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                                {
                                    Split(SplitArea.CutsceneStart, frameCounter);
                                }
                            }
                            else
                            {
                                if (locationID == (int)Locations.Tamriel && world_X == 13 && world_Y == -12 &&
                                    (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_GR3YSCALE || _settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_DALLETH))
                                {
                                    Split(SplitArea.CutsceneEnd, frameCounter);
                                }
                            }
                        }

                        if (alduin1Health < 0 && !isAlduin1Defeated)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] Alduin 1 has been defeated. HP: {1} - {0}", frameCounter, alduin1Health));
                            isAlduin1Defeated = true;
                            
                            if (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS)
                            {
                                Split(SplitArea.Alduin1, frameCounter);
                            }
                        }

                        // the only mainquest you can complete here is the council so when a quest completes, walrus' council split
                        if (mainquestsCompleted  == prevMainQuestsCompleted + 1 && locationID == (int)Locations.HighHrothgar)
                        {
                            isCouncilDone = true;
                            
                            if (_settings.AnyPercentTemplate == SkyrimSettings.TEMPLATE_MRWALRUS)
                            {
                                Split(SplitArea.Council, frameCounter);
                            }
                        }

                        // if alduin is defeated in sovngarde
                        if (isAlduin2Defeated != prevIsAlduin2Defeated && isAlduin2Defeated && locationID == (int)Locations.Sovngarde)
                        {
                            // AlduinDefeated split
                            Split(SplitArea.AlduinDefeated, frameCounter);
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
                            Split(lastQuestCompleted, frameCounter);    
                        }


                        Debug.WriteLineIf(locationID != prevLocationID, String.Format("[NoLoads] Location changed to {0} - {1}", locationID.ToString("X8"), frameCounter));
                        Debug.WriteLineIf(world_X != prevWorld_X || world_Y != prevWorld_Y, String.Format("[NoLoads] Coords changed to X: {0} Y: {1} - {2}", world_X, world_Y, frameCounter));
                        Debug.WriteLineIf(isInEscapeMenu != prevIsInEscapeMenu, String.Format("[NoLoads] isInEscapeMenu changed to {0} - {1}", isInEscapeMenu, frameCounter));

                        prevIsLoading = isLoading;
                        prevIsLoadingScreen = isLoadingScreen;
                        prevIsInFadeOut = isInFadeOut;
                        prevIsAlduin2Defeated = isAlduin2Defeated;
                        prevQuestlinesCompleted = questlinesCompleted;
                        prevCompanionsQuestsCompleted = companionsQuestsCompleted;
                        prevCollegeQuestsCompleted = collegeQuestsCompleted;
                        prevDarkBrotherhoodQuestsCompleted = darkBrotherhoodQuestsCompleted;
                        prevThievesGuildQuestsCompleted = thievesGuildQuestsCompleted;
                        prevIsInEscapeMenu = isInEscapeMenu;
                        prevLocationID = locationID;
                        prevWorld_X = world_X;
                        prevWorld_Y = world_Y;
                        prevMainQuestsCompleted = mainquestsCompleted;
                        prevLocationsDiscovered = locationsDiscovered;
                        prevArePlayerControlsDisabled = arePlayerControlsDisabled;
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

        private void Split(SplitArea split, uint frame)
        {
            _uiThread.Post(d =>
            {
                if (this.OnSplitCompleted != null)
                {
                    this.OnSplitCompleted(this, split, frame);
                }
            }, null);
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
