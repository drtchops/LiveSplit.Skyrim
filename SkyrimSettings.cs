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
        public bool DarkBrotherhood { get; set; }
        public bool Companions { get; set; }
        public bool ThievesGuild { get; set; }
        public bool CollegeOfWinterhold { get; set; }

        private const bool DEFAULT_DRAWWITHOUTLOADS = true;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_ALDUINDEFEATED = true;
        private const bool DEFAULT_HELGEN = false;
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
            this.chkDarkBrotherhood.DataBindings.Add("Checked", this, "DarkBrotherhood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCompanions.DataBindings.Add("Checked", this, "Companions", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThievesGuild.DataBindings.Add("Checked", this, "ThievesGuild", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCollege.DataBindings.Add("Checked", this, "CollegeOfWinterhold", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.DrawWithoutLoads = DEFAULT_DRAWWITHOUTLOADS;
            this.AutoStart = DEFAULT_AUTOSTART;
            this.AlduinDefeated = DEFAULT_ALDUINDEFEATED;
            this.Helgen = DEFAULT_HELGEN;
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
