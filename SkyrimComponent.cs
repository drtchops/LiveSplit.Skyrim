﻿using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

namespace LiveSplit.Skyrim
{
    class SkyrimComponent : LogicComponent
    {
        public override string ComponentName
        {
            get { return "Skyrim"; }
        }

        public SkyrimSettings Settings { get; set; }

        public bool Disposed { get; private set; }
        public bool IsLayoutComponent { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;

        public SkyrimComponent(LiveSplitState state, bool isLayoutComponent)
        {
            _state = state;
            this.IsLayoutComponent = isLayoutComponent;

            this.Settings = new SkyrimSettings();

           _timer = new TimerModel { CurrentState = state };

            _gameMemory = new GameMemory();
            _gameMemory.OnFirstLevelLoading += gameMemory_OnFirstLevelLoading;
            _gameMemory.OnPlayerGainedControl += gameMemory_OnPlayerGainedControl;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            // _gameMemory.OnLoadScreenStarted += gameMemory_OnLoadScreenStarted;
            // _gameMemory.OnLoadScreenFinished += gameMemory_OnLoadScreenFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            state.OnReset += state_OnReset;
            _gameMemory.StartMonitoring();
        }

        public override void Dispose()
        {
            this.Disposed = true;

            _state.OnReset -= state_OnReset;

            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }
        }

        void state_OnReset(object sender, TimerPhase e)
        {
            _gameMemory.resetSplitStates();
        }

        void gameMemory_OnFirstLevelLoading(object sender, EventArgs e)
        {
            if (this.Settings.AutoStart)
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

        // void gameMemory_OnLoadScreenStarted(object sender, EventArgs e)
        // {
        //     // Nothing to do
        // }

        // void gameMemory_OnLoadScreenFinished(object sender, EventArgs e)
        // {
        //     // Nothing to do
        // }

        void gameMemory_OnSplitCompleted(object sender, GameMemory.SplitArea split, uint frame)
        {
            Trace.WriteLineIf(split != GameMemory.SplitArea.None, String.Format("[NoLoads] {0} Split - {1}", split, frame));
            if (!_gameMemory.splitStates[(int)split] &&
                ((split == GameMemory.SplitArea.Helgen && this.Settings.Helgen) ||
                (split == GameMemory.SplitArea.DarkBrotherhoodQuestlineCompleted && this.Settings.DarkBrotherhood) ||
                (split == GameMemory.SplitArea.CompanionsQuestlineCompleted && this.Settings.Companions) ||
                (split == GameMemory.SplitArea.CollegeQuestlineCompleted && this.Settings.CollegeOfWinterhold) ||
                (split == GameMemory.SplitArea.ThievesGuildQuestlineCompleted && this.Settings.ThievesGuild) ||
                (split == GameMemory.SplitArea.AlduinDefeated && this.Settings.AlduinDefeated)))
            {
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
