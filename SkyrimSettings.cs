using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.Skyrim
{
    public partial class SkyrimSettings : UserControl
    {
        public bool DrawWithoutLoads { get; set; }
        public bool AutoStart { get; set; }
        public bool AlduinDefeated { get; set; }
        public bool Helgen { get; set; }
        public bool Whiterun { get; set; }
        public bool ThalmorEmbassy { get; set; }
        public bool Esbern { get; set; }
        public bool Riverwood { get; set; }
        public bool TheWall { get; set; }
        public bool Septimus { get; set; }
        public bool MzarkTower { get; set; }
        public bool ClearSky { get; set; }
        public bool Alduin1 { get; set; }
        public bool HighHrothgar{ get; set; }
        public bool Solitude { get; set; }
        public bool Windhelm { get; set; }
        public bool Council { get; set; }
        public bool Odahviing { get; set; }
        public bool EnterSovngarde{ get; set; }
        public bool DarkBrotherhood { get; set; }
        public bool Companions { get; set; }
        public bool ThievesGuild { get; set; }
        public bool CollegeOfWinterhold { get; set; }

        private const bool DEFAULT_DRAWWITHOUTLOADS = true;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_ALDUINDEFEATED = true;
        private const bool DEFAULT_HELGEN = false;
        private const bool DEFAULT_WHITERUN = false;
        private const bool DEFAULT_THALMOREMBASSY = false;
        private const bool DEFAULT_ESBERN = false;
        private const bool DEFAULT_RIVERWOOD = false;
        private const bool DEFAULT_THEWALL = false;
        private const bool DEFAULT_SEPTIMUS = false;
        private const bool DEFAULT_MZARKTOWER = false;
        private const bool DEFAULT_CLEARSKY = false;
        private const bool DEFAULT_ALDUIN1 = false;
        private const bool DEFAULT_HIGHHROTHGAR = false;
        private const bool DEFAULT_SOLITUDE = false;
        private const bool DEFAULT_WINDHELM = false;
        private const bool DEFAULT_COUNCIL = false;
        private const bool DEFAULT_ODAHVIING = false;
        private const bool DEFAULT_ENTERSOVNGARDE = false;
        private const bool DEFAULT_DARKBROTHERHOOD = false;
        private const bool DEFAULT_COMPANIONS = false;
        private const bool DEFAULT_THIEVESGUILD = false;
        private const bool DEFAULT_COLLEGEOFWINTERHOLD = false;

        public SkyrimSettings()
        {
            InitializeComponent();

            this.chkDisplayWithoutLoads.DataBindings.Add("Checked", this, "DrawWithoutLoads", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduinDefeated.DataBindings.Add("Checked", this, "AlduinDefeated", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHelgen.DataBindings.Add("Checked", this, "Helgen", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWhiterun.DataBindings.Add("Checked", this, "Whiterun", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThalmorEmbassy.DataBindings.Add("Checked", this, "ThalmorEmbassy", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEsbern.DataBindings.Add("Checked", this, "Esbern", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkRiverwood.DataBindings.Add("Checked", this, "Riverwood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTheWall.DataBindings.Add("Checked", this, "TheWall", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSeptimus.DataBindings.Add("Checked", this, "Septimus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMzarkTower.DataBindings.Add("Checked", this, "MzarkTower", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkClearSky.DataBindings.Add("Checked", this, "ClearSky", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduin1.DataBindings.Add("Checked", this, "Alduin1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHighHrothgar.DataBindings.Add("Checked", this, "HighHrothgar", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSolitude.DataBindings.Add("Checked", this, "Solitude", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWindhelm.DataBindings.Add("Checked", this, "Windhelm", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCouncil.DataBindings.Add("Checked", this, "Council", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkOdahviing.DataBindings.Add("Checked", this, "Odahviing", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEnterSovngarde.DataBindings.Add("Checked", this, "EnterSovngarde", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDarkBrotherhood.DataBindings.Add("Checked", this, "DarkBrotherhood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCompanions.DataBindings.Add("Checked", this, "Companions", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThievesGuild.DataBindings.Add("Checked", this, "ThievesGuild", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCollege.DataBindings.Add("Checked", this, "CollegeOfWinterhold", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.DrawWithoutLoads = DEFAULT_DRAWWITHOUTLOADS;
            this.AutoStart = DEFAULT_AUTOSTART;
            this.AlduinDefeated = DEFAULT_ALDUINDEFEATED;
            this.Helgen = DEFAULT_HELGEN;
            this.Whiterun = DEFAULT_WHITERUN;
            this.ThalmorEmbassy = DEFAULT_THALMOREMBASSY;
            this.Esbern = DEFAULT_ESBERN;
            this.Riverwood = DEFAULT_RIVERWOOD;
            this.TheWall = DEFAULT_THEWALL;
            this.Septimus = DEFAULT_SEPTIMUS;
            this.MzarkTower = DEFAULT_MZARKTOWER;
            this.ClearSky = DEFAULT_CLEARSKY;
            this.Alduin1 = DEFAULT_ALDUIN1;
            this.HighHrothgar = DEFAULT_HIGHHROTHGAR;
            this.Solitude = DEFAULT_SOLITUDE;
            this.Windhelm = DEFAULT_WINDHELM;
            this.Council = DEFAULT_COUNCIL;
            this.Odahviing = DEFAULT_ODAHVIING;
            this.EnterSovngarde = DEFAULT_ENTERSOVNGARDE;
            this.DarkBrotherhood = DEFAULT_DARKBROTHERHOOD;
            this.Companions = DEFAULT_COMPANIONS;
            this.CollegeOfWinterhold = DEFAULT_COLLEGEOFWINTERHOLD;
            this.ThievesGuild = DEFAULT_THIEVESGUILD;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(doc, "DrawWithoutLoads", this.DrawWithoutLoads));
            settingsNode.AppendChild(ToElement(doc, "AutoStart", this.AutoStart));
            settingsNode.AppendChild(ToElement(doc, "AlduinDefeated", this.AlduinDefeated));
            settingsNode.AppendChild(ToElement(doc, "Helgen", this.Helgen));
            settingsNode.AppendChild(ToElement(doc, "Whiterun", this.Whiterun));
            settingsNode.AppendChild(ToElement(doc, "ThalmorEmbassy", this.ThalmorEmbassy));
            settingsNode.AppendChild(ToElement(doc, "Esbern", this.Esbern));
            settingsNode.AppendChild(ToElement(doc, "Riverwood", this.Riverwood));
            settingsNode.AppendChild(ToElement(doc, "TheWall", this.TheWall));
            settingsNode.AppendChild(ToElement(doc, "Septimus", this.Septimus));
            settingsNode.AppendChild(ToElement(doc, "MzarkTower", this.MzarkTower));
            settingsNode.AppendChild(ToElement(doc, "ClearSky", this.ClearSky));
            settingsNode.AppendChild(ToElement(doc, "Alduin1", this.Alduin1));
            settingsNode.AppendChild(ToElement(doc, "HighHrothgar", this.HighHrothgar));
            settingsNode.AppendChild(ToElement(doc, "Solitude", this.Solitude));
            settingsNode.AppendChild(ToElement(doc, "Windhelm", this.Windhelm));
            settingsNode.AppendChild(ToElement(doc, "Council", this.Council));
            settingsNode.AppendChild(ToElement(doc, "Odahviing", this.Odahviing));
            settingsNode.AppendChild(ToElement(doc, "EnterSovngarde", this.EnterSovngarde));
            settingsNode.AppendChild(ToElement(doc, "DarkBrotherhood", this.DarkBrotherhood));
            settingsNode.AppendChild(ToElement(doc, "Companions", this.Companions));
            settingsNode.AppendChild(ToElement(doc, "ThievesGuild", this.ThievesGuild));
            settingsNode.AppendChild(ToElement(doc, "CollegeOfWinterhold", this.CollegeOfWinterhold));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.DrawWithoutLoads = ParseBool(settings, "DrawWithoutLoads", DEFAULT_DRAWWITHOUTLOADS);
            this.AutoStart = ParseBool(settings, "AutoStart", DEFAULT_AUTOSTART);
            this.AlduinDefeated = ParseBool(settings, "AlduinDefeated", DEFAULT_ALDUINDEFEATED);
            this.Helgen = ParseBool(settings, "Helgen", DEFAULT_HELGEN);
            this.Whiterun = ParseBool(settings, "Whiterun", DEFAULT_WHITERUN);
            this.ThalmorEmbassy = ParseBool(settings, "ThalmorEmbassy", DEFAULT_THALMOREMBASSY);
            this.Esbern = ParseBool(settings, "Esbern", DEFAULT_ESBERN);
            this.Riverwood = ParseBool(settings, "Riverwood", DEFAULT_RIVERWOOD);
            this.TheWall = ParseBool(settings, "TheWall", DEFAULT_THEWALL);
            this.Septimus = ParseBool(settings, "Septimus", DEFAULT_SEPTIMUS);
            this.MzarkTower = ParseBool(settings, "MzarkTower", DEFAULT_RIVERWOOD);
            this.ClearSky = ParseBool(settings, "ClearSky", DEFAULT_CLEARSKY);
            this.Alduin1 = ParseBool(settings, "Alduin1", DEFAULT_ALDUIN1);
            this.HighHrothgar = ParseBool(settings, "HighHrothgar", DEFAULT_HIGHHROTHGAR);
            this.Solitude = ParseBool(settings, "Solitude", DEFAULT_SOLITUDE);
            this.Windhelm = ParseBool(settings, "Windhelm", DEFAULT_WINDHELM);
            this.Council = ParseBool(settings, "Council", DEFAULT_COUNCIL);
            this.Odahviing = ParseBool(settings, "Odahviing", DEFAULT_ODAHVIING);
            this.EnterSovngarde = ParseBool(settings, "EnterSovngarde", DEFAULT_ENTERSOVNGARDE);
            this.DarkBrotherhood = ParseBool(settings, "DarkBrotherhood", DEFAULT_DARKBROTHERHOOD);
            this.Companions = ParseBool(settings, "Companions", DEFAULT_COMPANIONS);
            this.ThievesGuild = ParseBool(settings, "ThievesGuild", DEFAULT_THIEVESGUILD);
            this.CollegeOfWinterhold = ParseBool(settings, "CollegeOfWinterhold", DEFAULT_COLLEGEOFWINTERHOLD);
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ?
                (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_)
                : default_;
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }
    }
}
