using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;

namespace LiveSplit.Skyrim
{
    class SkyrimComponent : IComponent
    {
        public string ComponentName
        {
            get { return "Skyrim"; }
        }

        public IDictionary<string, Action> ContextMenuControls { get; protected set; }
        protected InfoTimeComponent InternalComponent { get; set; }
        public SkyrimSettings Settings { get; set; }

        public bool Disposed { get; private set; }
        public bool IsLayoutComponent { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;
        private GraphicsCache _cache;

        public SkyrimComponent(LiveSplitState state, bool isLayoutComponent)
        {
            _state = state;
            this.IsLayoutComponent = isLayoutComponent;

            this.Settings = new SkyrimSettings();
            this.ContextMenuControls = new Dictionary<String, Action>();
            this.InternalComponent = new InfoTimeComponent(null, null, new RegularTimeFormatter(TimeAccuracy.Hundredths));

            _cache = new GraphicsCache();
            _timer = new TimerModel { CurrentState = state };

            _gameMemory = new GameMemory();
            _gameMemory.OnFirstLevelLoading += gameMemory_OnFirstLevelLoading;
            _gameMemory.OnPlayerGainedControl += gameMemory_OnPlayerGainedControl;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            // _gameMemory.OnLoadScreenStarted += gameMemory_OnLoadScreenStarted;
            // _gameMemory.OnLoadScreenFinished += gameMemory_OnLoadScreenFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            _gameMemory.OnEscapeMenuChanged += gameMemory_OnEscapeMenuChanged;
            state.OnReset += state_OnReset;
            _gameMemory.StartMonitoring();
        }

        public void Dispose()
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

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (!this.Settings.DrawWithoutLoads)
            {
                return;
            }

            this.InternalComponent.TimeValue =
                state.CurrentTime[state.CurrentTimingMethod == TimingMethod.GameTime
                    ? TimingMethod.RealTime : TimingMethod.GameTime];
            this.InternalComponent.InformationName = state.CurrentTimingMethod == TimingMethod.GameTime
                ? "Real Time" : "Without Loads";

            _cache.Restart();
            _cache["TimeValue"] = this.InternalComponent.ValueLabel.Text;
            _cache["TimingMethod"] = state.CurrentTimingMethod;
            if (invalidator != null && _cache.HasChanged)
            {
                invalidator.Invalidate(0f, 0f, width, height);
            }
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region region)
        {
            this.PrepareDraw(state);
            this.InternalComponent.DrawVertical(g, state, width, region);
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region region)
        {
            this.PrepareDraw(state);
            this.InternalComponent.DrawHorizontal(g, state, height, region);
        }

        void PrepareDraw(LiveSplitState state)
        {
            this.InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            this.InternalComponent.ValueLabel.ForeColor = state.LayoutSettings.TextColor;
            this.InternalComponent.NameLabel.HasShadow = this.InternalComponent.ValueLabel.HasShadow = state.LayoutSettings.DropShadows;
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

        void gameMemory_OnSplitCompleted(object sender, GameMemory.SplitArea split)
        {
            if (!_gameMemory.splitStates[(int)split] &&
                ((split == GameMemory.SplitArea.Helgen && this.Settings.Helgen) ||
                (split == GameMemory.SplitArea.HailSithisCompleted && this.Settings.HailSithis) ||
                (split == GameMemory.SplitArea.GloryOfTheDeadCompleted && this.Settings.GloryOfTheDead) ||
                (split == GameMemory.SplitArea.TheEyeOfMagnusCompleted && this.Settings.TheEyeofMagnus) ||
                (split == GameMemory.SplitArea.DarknessReturnsCompleted && this.Settings.DarknessReturns) ||
                (split == GameMemory.SplitArea.AlduinDefeated && this.Settings.AlduinDefeated)))
            {
                _timer.Split();
                _gameMemory.splitStates[(int)split] = true;
            }
        }

        void gameMemory_OnEscapeMenuChanged(object sender, bool isInEscapeMenu)
        {
            if (this.Settings.PauseInEscapeMenu && _state.CurrentPhase != TimerPhase.NotRunning
                && (_state.CurrentPhase == TimerPhase.Paused) != isInEscapeMenu)
            {
                _timer.Pause();
            }
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return this.Settings.GetSettings(document);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return this.Settings;
        }

        public void SetSettings(XmlNode settings)
        {
            this.Settings.SetSettings(settings);
        }

        public float VerticalHeight { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.VerticalHeight : 0; } }
        public float HorizontalWidth { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.HorizontalWidth : 0; } }
        public float MinimumWidth { get { return this.InternalComponent.MinimumWidth; } }
        public float MinimumHeight { get { return this.InternalComponent.MinimumHeight; } }
        public float PaddingLeft { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingLeft : 0; } }
        public float PaddingRight { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingRight : 0; } }
        public float PaddingTop { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingTop : 0; } }
        public float PaddingBottom { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingBottom : 0; } }
        public void RenameComparison(string oldName, string newName) { }
    }
}
