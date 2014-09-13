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
        public bool HailSithis { get; set; }

        private const bool DEFAULT_DRAWWITHOUTLOADS = true;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_ALDUINDEFEATED = true;
        private const bool DEFAULT_HELGEN = false;
        private const bool DEFAULT_HAILSITHIS = false;

        public SkyrimSettings()
        {
            InitializeComponent();

            this.chkDisplayWithoutLoads.DataBindings.Add("Checked", this, "DrawWithoutLoads", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduinDefeated.DataBindings.Add("Checked", this, "AlduinDefeated", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHelgen.DataBindings.Add("Checked", this, "Helgen", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHailSithis.DataBindings.Add("Checked", this, "HailSithis", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.DrawWithoutLoads = DEFAULT_DRAWWITHOUTLOADS;
            this.AutoStart = DEFAULT_AUTOSTART;
            this.AlduinDefeated = DEFAULT_ALDUINDEFEATED;
            this.Helgen = DEFAULT_HELGEN;
            this.HailSithis = DEFAULT_HAILSITHIS;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(doc, "DrawWithoutLoads", this.DrawWithoutLoads));
            settingsNode.AppendChild(ToElement(doc, "AutoStart", this.AutoStart));
            settingsNode.AppendChild(ToElement(doc, "AlduinDefeated", this.AlduinDefeated));
            settingsNode.AppendChild(ToElement(doc, "Helgen", this.Helgen));
            settingsNode.AppendChild(ToElement(doc, "HailSithis", this.HailSithis));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.DrawWithoutLoads = ParseBool(settings, "DrawWithoutLoads", DEFAULT_DRAWWITHOUTLOADS);
            this.AutoStart = ParseBool(settings, "AutoStart", DEFAULT_AUTOSTART);
            this.AlduinDefeated = ParseBool(settings, "AlduinDefeated", DEFAULT_ALDUINDEFEATED);
            this.Helgen = ParseBool(settings, "Helgen", DEFAULT_HELGEN);
            this.HailSithis = ParseBool(settings, "HailSithis", DEFAULT_HAILSITHIS);
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
