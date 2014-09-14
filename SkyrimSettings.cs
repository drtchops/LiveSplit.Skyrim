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
        public bool GloryOfTheDead { get; set; }
        public bool UnderNewManagement { get; set; }
        public bool TheEyeofMagnus { get; set; }

        private const bool DEFAULT_DRAWWITHOUTLOADS = true;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_ALDUINDEFEATED = true;
        private const bool DEFAULT_HELGEN = false;
        private const bool DEFAULT_HAILSITHIS = false;
        private const bool DEFAULT_GLORYOFTHEDEAD = false;
        private const bool DEFAULT_UNDERNEWMANAGEMENT = false;
        private const bool DEFAULT_THEEYEOFMAGNUS = false;

        public SkyrimSettings()
        {
            InitializeComponent();

            this.chkDisplayWithoutLoads.DataBindings.Add("Checked", this, "DrawWithoutLoads", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduinDefeated.DataBindings.Add("Checked", this, "AlduinDefeated", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHelgen.DataBindings.Add("Checked", this, "Helgen", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHailSithis.DataBindings.Add("Checked", this, "HailSithis", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkGloryOfTheDead.DataBindings.Add("Checked", this, "GloryOfTheDead", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkUnderNewManagement.DataBindings.Add("Checked", this, "UnderNewManagement", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTheEyeofMagnus.DataBindings.Add("Checked", this, "TheEyeofMagnus", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.DrawWithoutLoads = DEFAULT_DRAWWITHOUTLOADS;
            this.AutoStart = DEFAULT_AUTOSTART;
            this.AlduinDefeated = DEFAULT_ALDUINDEFEATED;
            this.Helgen = DEFAULT_HELGEN;
            this.HailSithis = DEFAULT_HAILSITHIS;
            this.GloryOfTheDead = DEFAULT_GLORYOFTHEDEAD;
            this.TheEyeofMagnus = DEFAULT_THEEYEOFMAGNUS;
            this.UnderNewManagement = DEFAULT_UNDERNEWMANAGEMENT;
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
            settingsNode.AppendChild(ToElement(doc, "GloryOfTheDead", this.GloryOfTheDead));
            settingsNode.AppendChild(ToElement(doc, "UnderNewManagement", this.UnderNewManagement));
            settingsNode.AppendChild(ToElement(doc, "TheEyeofMagnus", this.TheEyeofMagnus));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.DrawWithoutLoads = ParseBool(settings, "DrawWithoutLoads", DEFAULT_DRAWWITHOUTLOADS);
            this.AutoStart = ParseBool(settings, "AutoStart", DEFAULT_AUTOSTART);
            this.AlduinDefeated = ParseBool(settings, "AlduinDefeated", DEFAULT_ALDUINDEFEATED);
            this.Helgen = ParseBool(settings, "Helgen", DEFAULT_HELGEN);
            this.HailSithis = ParseBool(settings, "HailSithis", DEFAULT_HAILSITHIS);
            this.GloryOfTheDead = ParseBool(settings, "GloryOfTheDead", DEFAULT_GLORYOFTHEDEAD);
            this.UnderNewManagement = ParseBool(settings, "UnderNewManagement", DEFAULT_UNDERNEWMANAGEMENT);
            this.TheEyeofMagnus = ParseBool(settings, "TheEyeofMagnus", DEFAULT_THEEYEOFMAGNUS);
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
