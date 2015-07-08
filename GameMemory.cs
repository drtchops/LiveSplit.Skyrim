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
        public event EventHandler OnFirstLevelLoading;
        public event EventHandler OnPlayerGainedControl;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        public delegate void SplitCompletedEventHandler(object sender, SplitArea type, string[] template, uint frame);
        public event SplitCompletedEventHandler OnSplitCompleted;
        public event EventHandler OnBearCart;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;

        private DeepPointer _isLoadingPtr;
        private DeepPointer _isLoadingScreenPtr;
        private DeepPointer _isInFadeOutPtr;
        private DeepPointer _locationIDPtr;
        private DeepPointer _world_XPtr;
        private DeepPointer _world_YPtr;
        private DeepPointer _isAlduin2DefeatedPtr;
        private DeepPointer _questlinesCompletedPtr;
        private DeepPointer _collegeOfWinterholdQuestsCompletedPtr;
        private DeepPointer _companionsQuestsCompletedPtr;
        private DeepPointer _darkBrotherhoodQuestsCompletedPtr;
        private DeepPointer _thievesGuildQuestsCompletedPtr;
        private DeepPointer _isInEscapeMenuPtr;
        private DeepPointer _mainQuestsCompletedPtr;
        private DeepPointer _wordsOfPowerLearnedPtr;
        private DeepPointer _Alduin1HealthPtr;
        private DeepPointer _locationsDiscoveredPtr;
        private DeepPointer _arePlayerControlsDisablePtr;
        private DeepPointer _bearCartHealthPtr;

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

            // Loads
            _isLoadingPtr = new DeepPointer(0x17337CC); // == 1 if a load is happening (any except loading screens in Helgen for some reason)
            _isLoadingScreenPtr = new DeepPointer(0xEE3561); // == 1 if in a loading screen
            _isInFadeOutPtr = new DeepPointer(0x172EE2E); // == 1 when in a fadeout, it goes back to 0 once control is gained

            // Position
            _locationIDPtr = new DeepPointer(0x01738308, 0x4, 0x78, 0x670, 0xEC); // ID of the current location (see http://steamcommunity.com/sharedfiles/filedetails/?id=148834641 or http://www.skyrimsearch.com/cells.php)
            _world_XPtr = new DeepPointer(0x0172E864, 0x64); // X world position (cell)
            _world_YPtr = new DeepPointer(0x0172E864, 0x68); // Y world position (cell)

            // Game state
            _isAlduin2DefeatedPtr = new DeepPointer(0x1711608); // == 1 when last blow is struck on alduin
            _questlinesCompletedPtr = new DeepPointer(0x00EE6C34, 0x3F0); // number of questlines completed (from ingame stats)
            _collegeOfWinterholdQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x38c); // number of college of winterhold quests completed (from ingame stats)
            _companionsQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x378); // number of companions quests completed (from ingame stats)
            _darkBrotherhoodQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x3b4); // number of dark brotherhood quests completed (from ingame stats)
            _thievesGuildQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x3a0); // number of thieves guild quests completed (from ingame stats)
            _isInEscapeMenuPtr = new DeepPointer(0x172E85E); // == 1 when in the pause menu or level up menu
            _mainQuestsCompletedPtr = new DeepPointer(0x00EE6C34, 0x350); // number of main quests completed (from ingame stats)
            _wordsOfPowerLearnedPtr = new DeepPointer(0x00EE6C34, 0x558); // "Words Of Power Learned" from ingame stats
            _Alduin1HealthPtr = new DeepPointer(0x00F41764, 0x74, 0x30, 0x30, 0x1c); // Alduin 1's health (if it's at 0 it's 99% of the time because it can't be found)
            _locationsDiscoveredPtr = new DeepPointer(0x00EE6C34, 0x170); // number of locations discovered (from ingame stats)
            _arePlayerControlsDisablePtr = new DeepPointer(0x172EF30, 0xf); // == 1 when player controls have been disabled (not necessarily all controls)
            _bearCartHealthPtr = new DeepPointer(0x00F354DC, 0x74, 0x30, 0x30, 0x1C);

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
                    data = new SkyrimData(game);

                    while (!game.HasExited)
                    {
                        data.IsLoadingScreen.SetValue(_isLoadingScreenPtr);

                        //need to avoid doing more than one SetValue in the same iteration to not make the previous value invalid
                        if (data.IsLoadingScreen.Current)
                            data.IsLoading.SetValue(true);
                        else
                            data.IsLoading.SetValue(_isLoadingPtr);

                        data.IsInFadeOut.SetValue(_isInFadeOutPtr);
                        data.LocationID.SetValue(_locationIDPtr);
                        data.WorldX.SetValue(_world_XPtr);
                        data.WorldY.SetValue(_world_YPtr);
                        data.IsAlduin2Defeated.SetValue(_isAlduin2DefeatedPtr);
                        data.QuestlinesCompleted.SetValue(_questlinesCompletedPtr);
                        data.CollegeOfWinterholdQuestsCompleted.SetValue(_collegeOfWinterholdQuestsCompletedPtr);
                        data.CompanionsQuestsCompleted.SetValue(_companionsQuestsCompletedPtr);
                        data.DarkBrotherhoodQuestsCompleted.SetValue(_darkBrotherhoodQuestsCompletedPtr);
                        data.ThievesGuildQuestsCompleted.SetValue(_thievesGuildQuestsCompletedPtr);
                        data.IsInEscapeMenu.SetValue(_isInEscapeMenuPtr);
                        data.MainQuestsCompleted.SetValue(_mainQuestsCompletedPtr);
                        data.WordsOfPowerLearned.SetValue(_wordsOfPowerLearnedPtr);
                        data.Alduin1Health.SetValue(_Alduin1HealthPtr);
                        data.LocationsDiscovered.SetValue(_locationsDiscoveredPtr);
                        data.ArePlayerControlsDisabled.SetValue(_arePlayerControlsDisablePtr);
                        data.BearCartHealth.SetValue(_bearCartHealthPtr);

                        if (data.IsLoading.HasChanged)
                        {
                            if (data.IsLoading.Current)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] Load Start - {0}", frameCounter));

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

                                if (!data.loadScreenFadeoutStarted)
                                {
                                    if (data.Location == new Location[]{ new Location(Locations.Tamriel, 13, -10), new Location(Locations.Tamriel, 13, -9) } && data.WordsOfPowerLearned.Current == 3)
                                    {
                                        Split(SplitArea.ClearSky, null, frameCounter);
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

                        if (data.IsLoadingScreen.HasChanged)
                        {
                            if (data.IsLoadingScreen.Current)
                            {
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen Start at {0} X: {1} Y: {2} - {3}", data.LocationID.Current.ToString("X8"), data.WorldX.Current, data.WorldY.Current, frameCounter));

                                data.loadingScreenStarted = true;
                                data.loadScreenStartLocation = data.Location;

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
                                    if (data.Location == new Location(Locations.HelgenKeep01, -2, -5))
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
                                Trace.WriteLine(String.Format("[NoLoads] LoadScreen End at {0} X: {1} Y: {2} - {3}", data.LocationID.Current.ToString("X8"), data.WorldX.Current, data.WorldY.Current, frameCounter));

                                if (data.loadingScreenStarted)
                                {
                                    data.loadingScreenStarted = false;
                                    data.isLoadingSaveFromMenu = false;
                                }
                            }
                        }

                        if (data.IsInFadeOut.HasChanged)
                        {
                            if (data.IsInFadeOut.Current)
                            {
                                Debug.WriteLine(String.Format("[NoLoads] Fadeout started - {0}", frameCounter));
                                if (data.IsLoadingScreen.Current)
                                {
                                    data.loadScreenFadeoutStarted = true;
                                }
                            }
                            else
                            {
                                Debug.WriteLine(String.Format("[NoLoads] Fadeout ended - {0}", frameCounter));
                                // if loadscreen fadeout finishes in helgen
                                if (data.IsInFadeOut.Previous && data.loadScreenFadeoutStarted &&
                                    data.Location == new Location(Locations.Tamriel, 3, -20))
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
                                data.loadScreenFadeoutStarted = false;
                            }
                        }

                        if (data.Location != data.PreviousLocation && data.isWaitingLocationUpdate)
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
                                if (!splitStates[(int)SplitArea.HighHrothgar])
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

                        if (data.LocationID.HasChanged && data.isWaitingLocationIDUpdate)
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
                                && data.Location == new Location(Locations.Tamriel, -20, 28))
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
                                && data.Location == new Location(Locations.RiftenWorld, 42, -24))
                            {
                                Split(SplitArea.Esbern, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts while leaving the Sleeping Giant Inn and ends in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.RiverwoodSleepingGiantInn
                                && data.Location == new Location(Locations.Tamriel, 5, -11))
                            {
                                data.leaveSleepingGiantInnCounter++;
                                if (data.leaveSleepingGiantInnCounter == 2)
                                {
                                    Split(SplitArea.Riverwood, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                                }
                            }
                            // if loadingscren starts in Sky Haven Temple and ends in Karthspire
                            else if (data.loadScreenStartLocation.ID == (int)Locations.SkyHavenTemple
                                && data.LocationID.Current == (int)Locations.KarthspireRedoubtWorld)
                            {
                                Split(SplitArea.TheWall, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts inside Septimus' Outpost and ends in front of its door
                            else if (data.loadScreenStartLocation.ID == (int)Locations.SeptimusSignusOutpost
                                && data.Location == new Location(Locations.Tamriel, 28, 34))
                            {
                                Split(SplitArea.Septimus, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if loadscreen starts inside Mzark Tower and ends outside of it
                            else if (data.loadScreenStartLocation.ID == (int)Locations.TowerOfMzark
                                && data.Location == new Location(Locations.Tamriel, 6, 11))
                            {
                                Split(SplitArea.MzarkTower, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                            }
                            // if Alduin1 has been defeated once and loadscreen starts in Paarthurnax' mountain whereabouts and ends in front of dragonsreach (fast travel)
                            else if (data.loadScreenStartLocation == Location.ThroatOfTheWorld
                                && data.Location == new Location(Locations.WhiterunWorld, 6, -1) && data.isAlduin1Defeated)
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
                                && data.Location == new Location[]{ new Location(Locations.Tamriel, 13, -9), new Location(Locations.Tamriel, 13, -10) })
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
                                && data.Location == new Location(Locations.SolitudeWorld, -16, 26))
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
                                && data.Location == new Location(Locations.WindhelmWorld, 32, 10))
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

                        if (data.LocationsDiscovered.Current == data.LocationsDiscovered.Previous + 1 && data.Location == Location.ThroatOfTheWorld)
                        {
                                Split(SplitArea.HorseClimb, new string[]{ SplitTemplates.GR3YSCALE }, frameCounter);
                        }

                        if (data.ArePlayerControlsDisabled.HasChanged && !data.IsInEscapeMenu.Current)
                        {
                            if (data.ArePlayerControlsDisabled.Current)
                            {
                                if (data.Location == new Location(Locations.Tamriel, 13, -12))
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
                                if (data.Location == new Location(Locations.Tamriel, 13, -12))
                                {
                                    string[] templates = new string[]
                                    {
                                        SplitTemplates.GR3YSCALE,
                                        SplitTemplates.DALLETH
                                    };
                                    Split(SplitArea.CutsceneEnd, templates, frameCounter);
                                }
                            }
                        }

                        if (data.Alduin1Health.Current < 0 && !data.isAlduin1Defeated)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] Alduin 1 has been defeated. HP: {1} - {0}", frameCounter, data.Alduin1Health.Current));
                            data.isAlduin1Defeated = true;

                            Split(SplitArea.Alduin1, new string[]{ SplitTemplates.MRWALRUS }, frameCounter);
                        }

                        if (data.LocationID.Current == (int)Locations.HelgenKeep01 && data.BearCartHealth.Current < 0 && data.BearCartHealth.Previous >= 0 && data.BearCartHealth.PrevDerefSuccess)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] BEAR CART! HP: {1} - {0}", frameCounter, data.BearCartHealth.Current));

                            _uiThread.Post(d =>
                            {
                                if (this.OnBearCart != null)
                                {
                                    this.OnBearCart(this, EventArgs.Empty);
                                }
                            }, null);
                        }

                        // the only mainquest you can complete here is the council so when a quest completes, walrus' council split
                        if (data.MainQuestsCompleted.Current == data.MainQuestsCompleted.Previous + 1 && data.LocationID.Current == (int)Locations.HighHrothgar)
                        {
                            data.isCouncilDone = true;

                            Split(SplitArea.Council, new string[]{ SplitTemplates.MRWALRUS }, frameCounter);
                        }

                        // if alduin is defeated in sovngarde
                        if (data.IsAlduin2Defeated.HasChanged && data.IsAlduin2Defeated.Current && data.LocationID.Current == (int)Locations.Sovngarde)
                        {
                            Split(SplitArea.AlduinDefeated, null, frameCounter);
                        }

                        // reset lastQuest 100 frames (1.5 seconds) after a completion to avoid splitting on a wrong questline.
                        if (frameCounter >= data.lastQuestframeCounter + 100 && data.lastQuestCompleted != SplitArea.None)
                        {
                            data.lastQuestCompleted = SplitArea.None;
                        }

                        if (data.CollegeOfWinterholdQuestsCompleted.Current > data.CollegeOfWinterholdQuestsCompleted.Previous)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A College of Winterhold quest has been completed - {0}", frameCounter));
                            data.lastQuestCompleted = SplitArea.CollegeOfWinterholdQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }
                        else if (data.CompanionsQuestsCompleted.Current > data.CompanionsQuestsCompleted.Previous)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A Companions quest has been completed - {0}", frameCounter));
                            data.lastQuestCompleted = SplitArea.CompanionsQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }
                        else if (data.DarkBrotherhoodQuestsCompleted.Current > data.DarkBrotherhoodQuestsCompleted.Previous)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A Dark Brotherhood quest has been completed - {0}", frameCounter));
                            data.lastQuestCompleted = SplitArea.DarkBrotherhoodQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }
                        else if (data.ThievesGuildQuestsCompleted.Current > data.ThievesGuildQuestsCompleted.Previous)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] A Thieves' Guild quest has been completed - {0}", frameCounter));
                            data.lastQuestCompleted = SplitArea.ThievesGuildQuestlineCompleted;
                            data.lastQuestframeCounter = frameCounter;
                        }

                        // if a questline is completed
                        if (data.QuestlinesCompleted.Current > data.QuestlinesCompleted.Previous)
                        {
                            Debug.WriteLineIf(data.lastQuestCompleted == SplitArea.None, String.Format("[NoLoads] A questline has been completed. - {0}", frameCounter));
                            Split(data.lastQuestCompleted, null, frameCounter);
                        }

                        Debug.WriteLineIf(data.LocationID.HasChanged, String.Format("[NoLoads] Location changed to {0} - {1}", data.LocationID.Current.ToString("X8"), frameCounter));
                        Debug.WriteLineIf(data.WorldX.HasChanged || data.WorldY.HasChanged, String.Format("[NoLoads] Coords changed to X: {0} Y: {1} - {2}", data.WorldX.Current, data.WorldY.Current, frameCounter));
                        Debug.WriteLineIf(data.IsInEscapeMenu.HasChanged, String.Format("[NoLoads] isInEscapeMenu changed to {0} - {1}", data.IsInEscapeMenu.Current, frameCounter));

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

        private void Split(SplitArea split, string[] templates, uint frame)
        {
            _uiThread.Post(d =>
            {
                if (this.OnSplitCompleted != null)
                {
                    this.OnSplitCompleted(this, split, templates, frame);
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
