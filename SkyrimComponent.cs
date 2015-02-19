using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace LiveSplit.Skyrim
{
    class SkyrimComponent : LogicComponent
    {
        public override string ComponentName
        {
            get { return "Skyrim"; }
        }

        public SkyrimSettings Settings { get; set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;

        public SkyrimComponent(LiveSplitState state)
        {
            bool debug = false;
            #if DEBUG
                debug = true;
            #endif
            Trace.WriteLine("[NoLoads] Using LiveSplit.Skyrim component version " + Assembly.GetExecutingAssembly().GetName().Version + " " + ((debug) ? "Debug" : "Release") + " build");
            _state = state;

            this.Settings = new SkyrimSettings();

           _timer = new TimerModel { CurrentState = state };

            _gameMemory = new GameMemory(this.Settings);
            _gameMemory.OnFirstLevelLoading += gameMemory_OnFirstLevelLoading;
            _gameMemory.OnPlayerGainedControl += gameMemory_OnPlayerGainedControl;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            state.OnStart += State_OnStart;
            _gameMemory.StartMonitoring();
        }

        public override void Dispose()
        {
            _state.OnStart -= State_OnStart;

            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }
        }

        void State_OnStart(object sender, EventArgs e)
        {
            _gameMemory.resetSplitStates();
        }

        void gameMemory_OnFirstLevelLoading(object sender, EventArgs e)
        {
            if (this.Settings.AutoReset)
            {
                _timer.Reset();
            }
        }

        void gameMemory_OnPlayerGainedControl(object sender, EventArgs e)
        {
            if (this.Settings.AutoStart)
            {
                _timer.Start();
            }
        }

        void gameMemory_OnLoadStarted(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = true;
        }

        void gameMemory_OnLoadFinished(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = false;
        }

        void gameMemory_OnSplitCompleted(object sender, GameMemory.SplitArea split, uint frame)
        {
            Debug.WriteLineIf(split != GameMemory.SplitArea.None, String.Format("[NoLoads] Trying to split {0} with {1} template, State: {2} - {3}", split, this.Settings.AnyPercentTemplate, _gameMemory.splitStates[(int)split], frame));
            if (_state.CurrentPhase == TimerPhase.Running && !_gameMemory.splitStates[(int)split] &&
                ((split == GameMemory.SplitArea.Helgen && this.Settings.Helgen) ||
                (split == GameMemory.SplitArea.Whiterun && this.Settings.Whiterun) ||
                (split == GameMemory.SplitArea.ThalmorEmbassy && this.Settings.ThalmorEmbassy) ||
                (split == GameMemory.SplitArea.Esbern && this.Settings.Esbern) ||
                (split == GameMemory.SplitArea.Riverwood && this.Settings.Riverwood) ||
                (split == GameMemory.SplitArea.TheWall && this.Settings.TheWall) ||
                (split == GameMemory.SplitArea.Septimus && this.Settings.Septimus) ||
                (split == GameMemory.SplitArea.MzarkTower && this.Settings.MzarkTower) ||
                (split == GameMemory.SplitArea.ClearSky && this.Settings.ClearSky) ||
                (split == GameMemory.SplitArea.HorseClimb && this.Settings.HorseClimb) ||
                (split == GameMemory.SplitArea.CutsceneEnd && this.Settings.CutsceneEnd) ||
                (split == GameMemory.SplitArea.CutsceneStart && this.Settings.CutsceneStart) ||
                (split == GameMemory.SplitArea.Alduin1 && this.Settings.Alduin1) ||
                (split == GameMemory.SplitArea.HighHrothgar && this.Settings.HighHrothgar) ||
                (split == GameMemory.SplitArea.Solitude && this.Settings.Solitude) ||
                (split == GameMemory.SplitArea.Windhelm && this.Settings.Windhelm) ||
                (split == GameMemory.SplitArea.Council && this.Settings.Council) ||
                (split == GameMemory.SplitArea.Odahviing && this.Settings.Odahviing) ||
                (split == GameMemory.SplitArea.EnterSovngarde && this.Settings.EnterSovngarde) ||
                (split == GameMemory.SplitArea.CollegeOfWinterholdQuestlineCompleted && this.Settings.CollegeOfWinterhold) ||
                (split == GameMemory.SplitArea.CompanionsQuestlineCompleted && this.Settings.Companions) ||
                (split == GameMemory.SplitArea.DarkBrotherhoodQuestlineCompleted && this.Settings.DarkBrotherhood) ||
                (split == GameMemory.SplitArea.ThievesGuildQuestlineCompleted && this.Settings.ThievesGuild) ||
                (split == GameMemory.SplitArea.AlduinDefeated && this.Settings.AlduinDefeated)))
            {
                Trace.WriteLine(String.Format("[NoLoads] {0} Split with {2} template - {1}", split, frame, this.Settings.AnyPercentTemplate));
                _timer.Split();
                _gameMemory.splitStates[(int)split] = true;
            }
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return this.Settings.GetSettings(document);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return this.Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            this.Settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
        public override void RenameComparison(string oldName, string newName) { }
    }
}
