using LiveSplit.Model;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.Skyrim
{
    public partial class SkyrimSettings : UserControl
    {
        public bool AutoStart { get; set; }
        public bool AutoReset { get; set; }
        public bool AlduinDefeated { get; set; }
        public bool Helgen { get; set; }
        public bool Whiterun { get; set; }
        public bool ThalmorEmbassy { get; set; }
        public bool Esbern { get; set; }
        public bool Riverwood { get; set; }
        public bool Karthspire { get; set; }
        public bool Septimus { get; set; }
        public bool TheWall { get; set; }
        public bool MzarkTower { get; set; }
        public bool ClearSky { get; set; }
        public bool HorseClimb { get; set; }
        public bool CutsceneStart { get; set; }
        public bool CutsceneEnd { get; set; }
        public bool Alduin1 { get; set; }
        public bool HighHrothgar { get; set; }
        public bool Solitude { get; set; }
        public bool Windhelm { get; set; }
        public bool Council { get; set; }
        public bool Odahviing { get; set; }
        public bool EnterSovngarde { get; set; }
        public bool CollegeOfWinterhold { get; set; }
        public bool Companions { get; set; }
        public bool DarkBrotherhood { get; set; }
        public bool ThievesGuild { get; set; }
        public string AnyPercentTemplate { get; set; }
        public Time BearCartPB { get; set; }
        public bool BearCartPBNotification { get; set; }
        public bool PlayBearCartSound { get; set; }
        public string BearCartSoundPath { get; set; }
        public bool IsBearCartSecret { get; set; }
        public bool PlayBearCartSoundOnlyOnPB { get; set; }

        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_AUTORESET = true;
        private const bool DEFAULT_ALDUINDEFEATED = true;
        private const bool DEFAULT_HELGEN = false;
        private const bool DEFAULT_WHITERUN = false;
        private const bool DEFAULT_THALMOREMBASSY = false;
        private const bool DEFAULT_ESBERN = false;
        private const bool DEFAULT_RIVERWOOD = false;
        private const bool DEFAULT_KARTHSPIRE = false;
        private const bool DEFAULT_SEPTIMUS = false;
        private const bool DEFAULT_THEWALL = false;
        private const bool DEFAULT_MZARKTOWER = false;
        private const bool DEFAULT_CLEARSKY = false;
        private const bool DEFAULT_HORSECLIMB = false;
        private const bool DEFAULT_CUTSCENESTART = false;
        private const bool DEFAULT_CUTSCENEEND = false;
        private const bool DEFAULT_ALDUIN1 = false;
        private const bool DEFAULT_HIGHHROTHGAR = false;
        private const bool DEFAULT_SOLITUDE = false;
        private const bool DEFAULT_WINDHELM = false;
        private const bool DEFAULT_COUNCIL = false;
        private const bool DEFAULT_ODAHVIING = false;
        private const bool DEFAULT_ENTERSOVNGARDE = false;
        private const bool DEFAULT_COLLEGEOFWINTERHOLD = false;
        private const bool DEFAULT_COMPANIONS = false;
        private const bool DEFAULT_DARKBROTHERHOOD = false;
        private const bool DEFAULT_THIEVESGUILD = false;
        private const string DEFAULT_ANYPERCENTTEMPLATE = SplitTemplates.MRWALRUS;
        private const bool DEFAULT_BEARCARTPBNOTIFICATION = true;
        private const bool DEFAULT_PLAYBEARCARTSOUND = true;
        private const bool DEFAULT_PLAYBEARCARTSOUNDONLYONPB = false;

        private SkyrimComponent _component;
        private LiveSplitState _state;
        private bool disableNbrSplitCheck;
        private const string BEAR_CART_CFG_FILE = "LiveSplit.Skyrim.cfg";

        public SkyrimSettings(SkyrimComponent component, LiveSplitState state)
        {
            InitializeComponent();
            this._component = component;
            this._state = state;
            this.Load += Settings_OnLoad;

            this.chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoReset.DataBindings.Add("Checked", this, "AutoReset", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduinDefeated.DataBindings.Add("Checked", this, "AlduinDefeated", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHelgen.DataBindings.Add("Checked", this, "Helgen", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWhiterun.DataBindings.Add("Checked", this, "Whiterun", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThalmorEmbassy.DataBindings.Add("Checked", this, "ThalmorEmbassy", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEsbern.DataBindings.Add("Checked", this, "Esbern", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkRiverwood.DataBindings.Add("Checked", this, "Riverwood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkKarthspire.DataBindings.Add("Checked", this, "Karthspire", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSeptimus.DataBindings.Add("Checked", this, "Septimus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTheWall.DataBindings.Add("Checked", this, "TheWall", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMzarkTower.DataBindings.Add("Checked", this, "MzarkTower", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkClearSky.DataBindings.Add("Checked", this, "ClearSky", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHorseClimb.DataBindings.Add("Checked", this, "HorseClimb", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCutsceneStart.DataBindings.Add("Checked", this, "CutsceneStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCutsceneEnd.DataBindings.Add("Checked", this, "CutsceneEnd", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduin1.DataBindings.Add("Checked", this, "Alduin1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHighHrothgar.DataBindings.Add("Checked", this, "HighHrothgar", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSolitude.DataBindings.Add("Checked", this, "Solitude", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWindhelm.DataBindings.Add("Checked", this, "Windhelm", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCouncil.DataBindings.Add("Checked", this, "Council", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkOdahviing.DataBindings.Add("Checked", this, "Odahviing", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEnterSovngarde.DataBindings.Add("Checked", this, "EnterSovngarde", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCollegeOfWinterhold.DataBindings.Add("Checked", this, "CollegeOfWinterhold", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCompanions.DataBindings.Add("Checked", this, "Companions", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDarkBrotherhood.DataBindings.Add("Checked", this, "DarkBrotherhood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThievesGuild.DataBindings.Add("Checked", this, "ThievesGuild", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkBearCartPBNotification.DataBindings.Add("Checked", this, "BearCartPBNotification", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkPlayBearCartSound.DataBindings.Add("Checked", this, "PlayBearCartSound", false, DataSourceUpdateMode.OnPropertyChanged);
            this.txtBearCartSoundPath.DataBindings.Add("Text", this, "BearCartSoundPath");
            this.chkPlayBearCartSoundOnlyOnPB.DataBindings.Add("Checked", this, "PlayBearCartSoundOnlyOnPB", false, DataSourceUpdateMode.OnPropertyChanged);
            if (_component.MediaPlayer != null)
                this.tbGeneralVolume.DataBindings.Add("Value", _component.MediaPlayer, "GeneralVolume");

            // defaults
            this.AutoStart = DEFAULT_AUTOSTART;
            this.AutoReset = DEFAULT_AUTORESET;
            this.AlduinDefeated = DEFAULT_ALDUINDEFEATED;
            this.Helgen = DEFAULT_HELGEN;
            this.Whiterun = DEFAULT_WHITERUN;
            this.ThalmorEmbassy = DEFAULT_THALMOREMBASSY;
            this.Esbern = DEFAULT_ESBERN;
            this.Riverwood = DEFAULT_RIVERWOOD;
            this.Karthspire = DEFAULT_KARTHSPIRE;
            this.Septimus = DEFAULT_SEPTIMUS;
            this.TheWall = DEFAULT_THEWALL;
            this.MzarkTower = DEFAULT_MZARKTOWER;
            this.ClearSky = DEFAULT_CLEARSKY;
            this.HorseClimb = DEFAULT_HORSECLIMB;
            this.CutsceneStart = DEFAULT_CUTSCENESTART;
            this.CutsceneEnd = DEFAULT_CUTSCENEEND;
            this.Alduin1 = DEFAULT_ALDUIN1;
            this.HighHrothgar = DEFAULT_HIGHHROTHGAR;
            this.Solitude = DEFAULT_SOLITUDE;
            this.Windhelm = DEFAULT_WINDHELM;
            this.Council = DEFAULT_COUNCIL;
            this.Odahviing = DEFAULT_ODAHVIING;
            this.EnterSovngarde = DEFAULT_ENTERSOVNGARDE;
            this.CollegeOfWinterhold = DEFAULT_COLLEGEOFWINTERHOLD;
            this.Companions = DEFAULT_COMPANIONS;
            this.DarkBrotherhood = DEFAULT_DARKBROTHERHOOD;
            this.ThievesGuild = DEFAULT_THIEVESGUILD;
            this.AnyPercentTemplate = DEFAULT_ANYPERCENTTEMPLATE;
            this.BearCartPBNotification = DEFAULT_BEARCARTPBNOTIFICATION;
            this.PlayBearCartSound = DEFAULT_PLAYBEARCARTSOUND;
            this.BearCartSoundPath = String.Empty;
            this.IsBearCartSecret = true;
            this.PlayBearCartSoundOnlyOnPB = DEFAULT_PLAYBEARCARTSOUNDONLYONPB;
            LoadBearCartConfig();

            UpdateTemplate(); //thanks twitch.tv/hurimaru

            foreach (CheckBox c in GetAutoSplitCheckboxes())
            {
                c.CheckedChanged += (s, e) =>
                {
                    if (!disableNbrSplitCheck) //checking all the checkboxes is very slow otherwise
                        CheckNbrAutoSplits();
                };
            }
        }

        void Settings_OnLoad(object sender, EventArgs e)
        {
            if (_component.MediaPlayer == null)
            {
                gbBearCartSound.Enabled = false;
                gbBearCartSound.Text = "Sound (NAudio.dll is missing)";
            }

            if (BearCartPB.RealTime != null && BearCartPB.RealTime != new TimeSpan(0))
            {
                this.lBearCartPB.Text = String.Format("Personal Best:\n Game Time: {0}, Real Time: {1}", BearCartPB.GameTime.Value.ToString(@"mm\:ss\.fff"), BearCartPB.RealTime.Value.ToString(@"mm\:ss\.fff"));
                this.lBearCartPB.Visible = true;
            }
            else
                this.lBearCartPB.Visible = false;

            tabsSplits.TabPages.Remove(tabBearCart);
            if (!this.IsBearCartSecret)
                tabsSplits.TabPages.Add(tabBearCart);

            CheckNbrAutoSplits();
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AutoStart", this.AutoStart));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AutoReset", this.AutoReset));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AlduinDefeated", this.AlduinDefeated));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Helgen", this.Helgen));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Whiterun", this.Whiterun));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "ThalmorEmbassy", this.ThalmorEmbassy));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Esbern", this.Esbern));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Riverwood", this.Riverwood));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Karthspire", this.Karthspire));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Septimus", this.Septimus));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "TheWall", this.TheWall));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "MzarkTower", this.MzarkTower));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "ClearSky", this.ClearSky));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "HorseClimb", this.HorseClimb));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "CutsceneStart", this.CutsceneStart));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "CutsceneEnd", this.CutsceneEnd));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Alduin1", this.Alduin1));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "HighHrothgar", this.HighHrothgar));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Solitude", this.Solitude));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Windhelm", this.Windhelm));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Council", this.Council));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Odahviing", this.Odahviing));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "EnterSovngarde", this.EnterSovngarde));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "CollegeOfWinterhold", this.CollegeOfWinterhold));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Companions", this.Companions));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "DarkBrotherhood", this.DarkBrotherhood));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "ThievesGuild", this.ThievesGuild));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AnyPercentTemplate", this.AnyPercentTemplate));

            SaveBearCartConfig();
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "BearCartPBNotification", this.BearCartPBNotification));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "PlayBearCartSound", this.PlayBearCartSound));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "BearCartSoundPath", this.BearCartSoundPath));
            settingsNode.AppendChild(SettingsHelper.ToElement(doc, "PlayBearCartSoundOnlyOnPB", this.PlayBearCartSoundOnlyOnPB));
            if (_component.MediaPlayer != null)
                settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Volume", _component.MediaPlayer.GeneralVolume));

            return settingsNode;
        }

        public void SaveBearCartConfig()
        {
            XmlDocument doc = new XmlDocument();
            var rootNode = doc.AppendChild(doc.CreateElement("BearCart"));
            var realTimePB = new TimeSpan(0);
            var gameTimePB = new TimeSpan(0);

            rootNode.AppendChild(SettingsHelper.ToElement(doc, "Secret", this.IsBearCartSecret));
            rootNode.AppendChild(SettingsHelper.ToElement(doc, "RealTime", this.BearCartPB.RealTime.Value));
            rootNode.AppendChild(SettingsHelper.ToElement(doc, "GameTime", this.BearCartPB.GameTime.Value));

            doc.Save(BEAR_CART_CFG_FILE);
        }

        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;

            this.disableNbrSplitCheck = true;

            this.AutoStart = SettingsHelper.ParseBool(settings["AutoStart"], DEFAULT_AUTOSTART);
            this.AutoReset = SettingsHelper.ParseBool(settings["AutoReset"], DEFAULT_AUTORESET);
            this.AlduinDefeated = SettingsHelper.ParseBool(settings["AlduinDefeated"], DEFAULT_ALDUINDEFEATED);
            this.Helgen = SettingsHelper.ParseBool(settings["Helgen"], DEFAULT_HELGEN);
            this.Whiterun = SettingsHelper.ParseBool(settings["Whiterun"], DEFAULT_WHITERUN);
            this.ThalmorEmbassy = SettingsHelper.ParseBool(settings["ThalmorEmbassy"], DEFAULT_THALMOREMBASSY);
            this.Esbern = SettingsHelper.ParseBool(settings["Esbern"], DEFAULT_ESBERN);
            this.Riverwood = SettingsHelper.ParseBool(settings["Riverwood"], DEFAULT_RIVERWOOD);
            this.Karthspire = SettingsHelper.ParseBool(settings["Karthspire"], DEFAULT_KARTHSPIRE);
            this.Septimus = SettingsHelper.ParseBool(settings["Septimus"], DEFAULT_SEPTIMUS);
            this.TheWall = SettingsHelper.ParseBool(settings["TheWall"], DEFAULT_THEWALL);
            this.MzarkTower = SettingsHelper.ParseBool(settings["MzarkTower"], DEFAULT_RIVERWOOD);
            this.ClearSky = SettingsHelper.ParseBool(settings["ClearSky"], DEFAULT_CLEARSKY);
            this.HorseClimb = SettingsHelper.ParseBool(settings["HorseClimb"], DEFAULT_HORSECLIMB);
            this.CutsceneStart = SettingsHelper.ParseBool(settings["CutsceneStart"], DEFAULT_CUTSCENESTART);
            this.CutsceneEnd = SettingsHelper.ParseBool(settings["CutsceneEnd"], DEFAULT_CUTSCENEEND);
            this.Alduin1 = SettingsHelper.ParseBool(settings["Alduin1"], DEFAULT_ALDUIN1);
            this.HighHrothgar = SettingsHelper.ParseBool(settings["HighHrothgar"], DEFAULT_HIGHHROTHGAR);
            this.Solitude = SettingsHelper.ParseBool(settings["Solitude"], DEFAULT_SOLITUDE);
            this.Windhelm = SettingsHelper.ParseBool(settings["Windhelm"], DEFAULT_WINDHELM);
            this.Council = SettingsHelper.ParseBool(settings["Council"], DEFAULT_COUNCIL);
            this.Odahviing = SettingsHelper.ParseBool(settings["Odahviing"], DEFAULT_ODAHVIING);
            this.EnterSovngarde = SettingsHelper.ParseBool(settings["EnterSovngarde"], DEFAULT_ENTERSOVNGARDE);
            this.CollegeOfWinterhold = SettingsHelper.ParseBool(settings["CollegeOfWinterhold"], DEFAULT_COLLEGEOFWINTERHOLD);
            this.Companions = SettingsHelper.ParseBool(settings["Companions"], DEFAULT_COMPANIONS);
            this.DarkBrotherhood = SettingsHelper.ParseBool(settings["DarkBrotherhood"], DEFAULT_DARKBROTHERHOOD);
            this.ThievesGuild = SettingsHelper.ParseBool(settings["ThievesGuild"], DEFAULT_THIEVESGUILD);

            LoadBearCartConfig();
            this.BearCartPBNotification = SettingsHelper.ParseBool(settings["BearCartPBNotification"], DEFAULT_BEARCARTPBNOTIFICATION);
            this.PlayBearCartSound = SettingsHelper.ParseBool(settings["PlayBearCartSound"], DEFAULT_PLAYBEARCARTSOUND);
            this.BearCartSoundPath = SettingsHelper.ParseString(settings["BearCartSoundPath"], String.Empty);
            this.PlayBearCartSoundOnlyOnPB = SettingsHelper.ParseBool(settings["PlayBearCartSoundOnlyOnPB"], DEFAULT_PLAYBEARCARTSOUNDONLYONPB);
            if (_component.MediaPlayer != null)
                _component.MediaPlayer.GeneralVolume = SettingsHelper.ParseInt(settings["Volume"], 100);

            this.AnyPercentTemplate = SettingsHelper.ParseString(element["AnyPercentTemplate"], DEFAULT_ANYPERCENTTEMPLATE);
            if (!SplitTemplates.Exists(this.AnyPercentTemplate))
                this.AnyPercentTemplate = DEFAULT_ANYPERCENTTEMPLATE;

            this.rbMrwalrus.Checked = this.AnyPercentTemplate == SplitTemplates.MRWALRUS;
            this.rbDrtchops.Checked = this.AnyPercentTemplate == SplitTemplates.DRTCHOPS;
            this.rbGr3yscale.Checked = this.AnyPercentTemplate == SplitTemplates.GR3YSCALE;
            this.rbDalleth.Checked = this.AnyPercentTemplate == SplitTemplates.DALLETH;
            UpdateTemplate();

            this.disableNbrSplitCheck = false;
            CheckNbrAutoSplits();
        }

        public void LoadBearCartConfig()
        {
            var pbFile = new XmlDocument();
            TimeSpan realTime = new TimeSpan(0);
            TimeSpan gameTime = new TimeSpan(0);

            if (System.IO.File.Exists(BEAR_CART_CFG_FILE))
            {
                pbFile.Load(BEAR_CART_CFG_FILE);
                if (pbFile["BearCart"] != null)
                {
                    if (TimeSpan.TryParse(pbFile["BearCart"]["RealTime"].InnerText, out realTime)
                        && TimeSpan.TryParse(pbFile["BearCart"]["GameTime"].InnerText, out gameTime))
                    {
                        this.BearCartPB = new Time(realTime, gameTime);
                    }
                    else
                        this.BearCartPB = new Time(new TimeSpan(0), new TimeSpan(0));

                    this.IsBearCartSecret = SettingsHelper.ParseBool(pbFile["BearCart"]["Secret"], true);
                }
            }

            this.BearCartPB = new Time(realTime, gameTime);
        }

        private void UpdateTemplate()
        {
            if (rbMrwalrus.Checked)
            {
                this.AnyPercentTemplate = SplitTemplates.MRWALRUS;
            }
            else if (rbDrtchops.Checked)
            {
                this.AnyPercentTemplate = SplitTemplates.DRTCHOPS;
            }
            else if (rbDalleth.Checked)
            {
                this.AnyPercentTemplate = SplitTemplates.DALLETH;
            }
            else if (rbGr3yscale.Checked)
            {
                this.AnyPercentTemplate = SplitTemplates.GR3YSCALE;
            }

            this.chkHorseClimb.Enabled = this.AnyPercentTemplate == SplitTemplates.GR3YSCALE;

            var csStartT = new string[]{ SplitTemplates.DRTCHOPS, SplitTemplates.DALLETH };
            this.chkCutsceneStart.Enabled = Array.IndexOf(csStartT, this.AnyPercentTemplate) >= 0;

            var csEndT = new string[]{ SplitTemplates.GR3YSCALE, SplitTemplates.DALLETH, SplitTemplates.DRTCHOPS };
            this.chkCutsceneEnd.Enabled = Array.IndexOf(csEndT, this.AnyPercentTemplate) >= 0;

            CheckNbrAutoSplits();
        }

        private void templateRadioButtonChanged(object sender, EventArgs e)
        {
            UpdateTemplate();
        }

        private void llCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!AreAllEnabledCheckboxChecked(flp_AnyPercentSplits.Controls))
            {
                checkAll(flp_AnyPercentSplits.Controls);
            }
            else
            {
                checkAll(flp_AnyPercentSplits.Controls, false);
            }
        }

        private bool AreAllEnabledCheckboxChecked(ControlCollection collection, bool state = true)
        {
            foreach (Control c in collection)
            {
                if (c.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)c;
                    if (checkBox.Checked != state && checkBox.Enabled)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void checkAll(ControlCollection collection, bool state = true)
        {
            disableNbrSplitCheck = true;
            foreach (Control c in collection)
            {
                if (c.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)c;
                    if (checkBox.Checked != state && checkBox.Enabled)
                    {
                        checkBox.Checked = state;
                    }
                }
            }
            disableNbrSplitCheck = false;
            CheckNbrAutoSplits();
        }

        private List<CheckBox> GetAutoSplitCheckboxes()
        {
            List<CheckBox> list = new List<CheckBox>();

            list.Add(this.chkHelgen);
            foreach (Control c in flp_AnyPercentSplits.Controls)
            {
                if (c is CheckBox)
                    list.Add(c as CheckBox);
            }
            foreach (Control c in tlpGuildsSplits.Controls)
            {
                if (c is CheckBox)
                    list.Add(c as CheckBox);
            }

            return list;
        }

        private void CheckNbrAutoSplits()
        {
            uint checkedCount = 0;
            foreach (CheckBox c in GetAutoSplitCheckboxes())
            {
                if (c.Enabled && c.Checked)
                    checkedCount++;
            }

            if (checkedCount != 0 && _state.Run.Count != checkedCount)
            {
                this.lWarningNbrAutoSplit.Text = String.Format("The number of enabled autosplits and segments don't match!\n Autosplits count: {0}   Segments count: {1}", checkedCount, _state.Run.Count);
                this.lWarningNbrAutoSplit.Visible = true;
            }
            else
                this.lWarningNbrAutoSplit.Visible = false;
        }

        protected String BrowseForPath(String path)
        {
            var fileDialog = new OpenFileDialog()
            {
                FileName = path ?? "",
                Filter = "Media Files|*.avi;*.mp3;*.wav;*.mid;*.midi;*.mpeg;*.mpg;*.mp4;*.m4a;*.aac;*.m4v;*.mov;*.wmv;|All Files (*.*)|*.*"
            };
            var result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
                path = fileDialog.FileName;
            return path;
        }

        private void chkBearCartSoundTest_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(BearCartSoundPath) || !System.IO.File.Exists(BearCartSoundPath))
            {
                _component.MediaPlayer?.PlaySound(_component.BearCartDefaultSoundPath);
            }
            else if (System.IO.File.Exists(BearCartSoundPath))
            {
                _component.MediaPlayer?.PlaySound(BearCartSoundPath);
            }
        }

        private void btnBrowseBearCartSound_Click(object sender, EventArgs e)
        {
            txtBearCartSoundPath.Text = BearCartSoundPath = BrowseForPath(BearCartSoundPath);
        }

        private void chkPlayBearCartSound_CheckedChanged(object sender, EventArgs e)
        {
            var enable = chkPlayBearCartSound.Checked;

            this.btnBrowseBearCartSound.Enabled = enable;
            this.lSound.Enabled = enable;
            this.txtBearCartSoundPath.Enabled = enable;
            this.btnBearCartSoundTest.Enabled = enable;
            this.chkPlayBearCartSoundOnlyOnPB.Enabled = enable;
            this.lVolume.Enabled = enable;
            this.tbGeneralVolume.Enabled = enable;

            if (!enable)
                _component.MediaPlayer?.Stop();
        }
    }

    public static class SplitTemplates
    {
        public const string MRWALRUS = "MrWalrus";
        public const string DRTCHOPS = "DrTChops";
        public const string GR3YSCALE = "gr3yscale";
        public const string DALLETH = "Dalleth";

        public static bool Exists(string template)
        {
            foreach (var fieldInfo in typeof(SplitTemplates).GetFields())
            {
                if (fieldInfo.IsStatic && fieldInfo.FieldType == typeof(string) && fieldInfo.GetValue(null) as string == template)
                    return true;
            }

            return false;
        }
    }
}
