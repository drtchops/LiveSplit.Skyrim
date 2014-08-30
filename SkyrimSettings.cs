using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.Skyrim
{
    public partial class SkyrimSettings : UserControl
    {
        public bool DrawWithoutLoads { get; set; }
        public bool AutoStartEnd { get; set; }
        public bool Helgen { get; set; }

        public SkyrimSettings()
        {
            InitializeComponent();

            this.chkDisplayWithoutLoads.DataBindings.Add("Checked", this, "DrawWithoutLoads", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoStartEnd.DataBindings.Add("Checked", this, "AutoStartEnd", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHelgen.DataBindings.Add("Checked", this, "Helgen", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.DrawWithoutLoads = true;
            this.AutoStartEnd = true;
            this.Helgen = false;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(doc, "DrawWithoutLoads", this.DrawWithoutLoads));
            settingsNode.AppendChild(ToElement(doc, "AutoStartEnd", this.AutoStartEnd));
            settingsNode.AppendChild(ToElement(doc, "Helgen", this.Helgen));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.DrawWithoutLoads = ParseBool(settings, "DrawWithoutLoads", true);
            this.AutoStartEnd = ParseBool(settings, "AutoStartEnd", true);
            this.Helgen = ParseBool(settings, "Helgen", true);
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
