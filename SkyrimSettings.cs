using LiveSplit.AutoSplitting;
using LiveSplit.Model;
using LiveSplit.Skyrim.AutoSplitData;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UpdateManager;

namespace LiveSplit.Skyrim
{
	public partial class SkyrimSettings : UserControl
	{
		public bool AutoStart { get; set; }
		public bool AutoReset { get; set; }
		public bool AutoUpdatePresets { get; set; }

		public AutoSplitList AutoSplitList { get; }
		public BindingList<AutoSplitList> Presets { get; }
		public string Preset { get; set; }
		public AutoSplitList CustomAutosplits { get; }

		public Time BearCartPB { get; set; }
		public bool BearCartPBNotification { get; set; }
		public bool PlayBearCartSound { get; set; }
		public string BearCartSoundPath { get; set; }
		public bool IsBearCartSecret { get; set; }
		public bool PlayBearCartSoundOnlyOnPB { get; set; }

		const bool DEFAULT_AUTOSTART = true;
		const bool DEFAULT_AUTORESET = true;
		const bool DEFAULT_AUTOUPDATEPRESETS = true;
		const bool DEFAULT_BEARCARTPBNOTIFICATION = true;
		const bool DEFAULT_PLAYBEARCARTSOUND = true;
		const bool DEFAULT_PLAYBEARCARTSOUNDONLYONPB = false;
		const string BEAR_CART_CFG_FILE = "LiveSplit.Skyrim.cfg";
		const string PRESETS_FILE_NAME = "LiveSplit.Skyrim.Presets.xml";
		readonly string DefaultPreset;
		readonly string PRESETS_FILE_PATH;

		SkyrimComponent _component;
		LiveSplitState _state;
		SynchronizationContext _uiThread;
		AutoSplitEnv _autoSplitEnv;
		HashSet<string> _hiddenAddresses;
		bool disableNbrSplitCheck;
		bool updatedPresets;

		public SkyrimSettings(SkyrimComponent component, LiveSplitState state)
		{
			InitializeComponent();

			_component = component;
			_state = state;
			_uiThread = SynchronizationContext.Current;
			PRESETS_FILE_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + PRESETS_FILE_NAME;

			// defaults
			AutoStart = DEFAULT_AUTOSTART;
			AutoReset = DEFAULT_AUTORESET;
			AutoUpdatePresets = DEFAULT_AUTOUPDATEPRESETS;

			CustomAutosplits = new AutoSplitList("Custom");
			DefaultPreset = CustomAutosplits.Name;
			AutoSplitList = new AutoSplitList();
			Presets = new BindingList<AutoSplitList>() { CustomAutosplits };

			var addr = new SkyrimData();
			_hiddenAddresses = new HashSet<string>()
			{
				addr.IsQuickSaving.Name,
				addr.Location.Name,
				addr.WorldID.Name,
				addr.CellX.Name,
				addr.CellY.Name
			};
			_autoSplitEnv = new AutoSplitEnv()
			{
				Addresses = addr.Where(w => !_hiddenAddresses.Contains(w.Name)),
				Events = GameEvent.GetValues(typeof(SkyrimEvent)),
				Presets = Presets.Except(new AutoSplitList[] { CustomAutosplits }),
				DefaultVariableType = typeof(AutoSplitData.Variables.LoadScreen)
			};

			if (File.Exists(PRESETS_FILE_PATH))
				LoadPresets();

			Preset = DefaultPreset;

			BearCartPBNotification = DEFAULT_BEARCARTPBNOTIFICATION;
			PlayBearCartSound = DEFAULT_PLAYBEARCARTSOUND;
			BearCartSoundPath = string.Empty;
			IsBearCartSecret = true;
			PlayBearCartSoundOnlyOnPB = DEFAULT_PLAYBEARCARTSOUNDONLYONPB;
			LoadBearCartConfig();

			InitializeFormLogic();
			InitializeToolTab();
		}

		void UsePreset(AutoSplitList preset)
		{
			Preset = preset.Name;
			cbPreset.SelectedItem = preset;
			AutoSplitList.Clear();
			AutoSplitList.AddRange(preset);
			RefreshSplitsListControl();
		}

		public XmlNode GetSettings(XmlDocument doc)
		{
			XmlElement settingsNode = doc.CreateElement("Settings");

			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AutoStart", AutoStart));
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AutoReset", AutoReset));
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "Preset", Preset));
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "AutoUpdatePresets", AutoUpdatePresets));
			settingsNode.AppendChild(CustomAutosplits.ToXml(doc, "AutoSplitList"));

			SaveBearCartConfig();
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "BearCartPBNotification", BearCartPBNotification));
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "PlayBearCartSound", PlayBearCartSound));
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "BearCartSoundPath", BearCartSoundPath));
			settingsNode.AppendChild(SettingsHelper.ToElement(doc, "PlayBearCartSoundOnlyOnPB", PlayBearCartSoundOnlyOnPB));
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

			rootNode.AppendChild(SettingsHelper.ToElement(doc, "Secret", IsBearCartSecret));
			rootNode.AppendChild(SettingsHelper.ToElement(doc, "RealTime", BearCartPB.RealTime.Value));
			rootNode.AppendChild(SettingsHelper.ToElement(doc, "GameTime", BearCartPB.GameTime.Value));

			doc.Save(BEAR_CART_CFG_FILE);
		}

		public void SetSettings(XmlNode settings)
		{
			ConvertOldSettings(settings);
			var element = (XmlElement)settings;
			disableNbrSplitCheck = true;

			AutoStart = SettingsHelper.ParseBool(settings["AutoStart"], DEFAULT_AUTOSTART);
			AutoReset = SettingsHelper.ParseBool(settings["AutoReset"], DEFAULT_AUTORESET);
			AutoUpdatePresets = SettingsHelper.ParseBool(settings["AutoUpdatePresets"], DEFAULT_AUTOUPDATEPRESETS);

			if (settings["AutoSplitList"] != null)
			{
				CustomAutosplits.Clear();
				AutoSplitList customList = null;
				try { customList = AutoSplitList.FromXml(settings["AutoSplitList"], _autoSplitEnv); }
				catch { }
				if (customList != null)
					CustomAutosplits.AddRange(customList);
			}

			Preset = SettingsHelper.ParseString(settings["Preset"], DefaultPreset);
			if (!updatedPresets)
			{
				if ((AutoUpdatePresets || !File.Exists(PRESETS_FILE_PATH)) && !CheckForComponentUpdate())
				{
					if (DownloadPresetsFile())
						LoadPresets();
				}
				updatedPresets = true;
			}
			UsePreset(Presets.FirstOrDefault(p => p.Name == Preset) ?? CustomAutosplits);

			LoadBearCartConfig();
			BearCartPBNotification = SettingsHelper.ParseBool(settings["BearCartPBNotification"], DEFAULT_BEARCARTPBNOTIFICATION);
			PlayBearCartSound = SettingsHelper.ParseBool(settings["PlayBearCartSound"], DEFAULT_PLAYBEARCARTSOUND);
			BearCartSoundPath = SettingsHelper.ParseString(settings["BearCartSoundPath"], String.Empty);
			PlayBearCartSoundOnlyOnPB = SettingsHelper.ParseBool(settings["PlayBearCartSoundOnlyOnPB"], DEFAULT_PLAYBEARCARTSOUNDONLYONPB);
			if (_component.MediaPlayer != null)
				_component.MediaPlayer.GeneralVolume = SettingsHelper.ParseInt(settings["Volume"], 100);

			disableNbrSplitCheck = false;
			CheckNbrAutoSplits();
		}

		void ConvertOldSettings(XmlNode settings)
		{
			Version version;
			if (!Version.TryParse(settings["Version"]?.InnerText, out version))
				return;

			if (version < new Version(3, 0) && version >= new Version(2, 0))
			{
				var preset = DefaultPreset;
				switch (settings["AnyPercentTemplate"]?.InnerText)
				{
					case "MrWalrus":
						preset = "Any% (MrWalrus)";
						break;
					case "DrTChops":
						preset = "Any% (DrTChops)";
						break;
					case "gr3yscale":
						preset = "Any % (gr3yscale)";
						break;
					case "Dalleth":
						preset = "Any% (Dalleth)";
						break;
					case null:
						preset = null;
						break;
				}
				if (string.IsNullOrEmpty(settings["Preset"]?.InnerText) && preset != null)
					settings.AppendChild(SettingsHelper.ToElement(settings.OwnerDocument, "Preset", preset));
			}
		}

		public void LoadBearCartConfig()
		{
			var pbFile = new XmlDocument();
			TimeSpan realTime = new TimeSpan(0);
			TimeSpan gameTime = new TimeSpan(0);

			if (File.Exists(BEAR_CART_CFG_FILE))
			{
				pbFile.Load(BEAR_CART_CFG_FILE);
				if (pbFile["BearCart"] != null)
				{
					if (TimeSpan.TryParse(pbFile["BearCart"]["RealTime"].InnerText, out realTime)
						&& TimeSpan.TryParse(pbFile["BearCart"]["GameTime"].InnerText, out gameTime))
					{
						BearCartPB = new Time(realTime, gameTime);
					}
					else
						BearCartPB = new Time(new TimeSpan(0), new TimeSpan(0));

					IsBearCartSecret = SettingsHelper.ParseBool(pbFile["BearCart"]["Secret"], true);
				}
			}

			BearCartPB = new Time(realTime, gameTime);
		}

		bool LoadPresets(bool silentErrors = true)
		{
			var loadedPresets = new List<AutoSplitList>();
			var corruptedPresets = new List<string>();
			var success = true;
			try
			{
				var doc = new XmlDocument();
				doc.Load(PRESETS_FILE_PATH);
				foreach (XmlElement listElem in doc.DocumentElement)
				{
					AutoSplitList preset;
					try
					{
						preset = AutoSplitList.FromXml(listElem, _autoSplitEnv);
					}
					catch
					{
						corruptedPresets.Add(listElem["Name"]?.InnerText);
						continue;
					}
					loadedPresets.Add(preset);
				}

				if (!silentErrors && corruptedPresets.Count != 0)
				{
					var errorMessage = new System.Text.StringBuilder();
					errorMessage.AppendLine("The following presets could not be loaded:");
					foreach (var preset in corruptedPresets)
					{
						string name = preset != null ? $"\"{preset}\"" : "(could not retrieve the name)";
						errorMessage.AppendLine($"\t{name}");
					}
					MessageBox.Show(errorMessage.ToString(), "Preset import error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch
			{
				success = false;
				if (!silentErrors)
					MessageBox.Show("Update failed.", "Presets update failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Presets.Clear();
			Presets.Add(CustomAutosplits);
			foreach (var item in loadedPresets.OrderBy(x => x.Name))
				Presets.Add(item);
			return success;
		}

		bool DownloadPresetsFile(bool silentErrors = true)
		{
			var client = new WebClient();
			var url = new SkyrimFactory().UpdateURL + "presets/" + PRESETS_FILE_NAME;

			try { client.DownloadFile(url, PRESETS_FILE_PATH); }
			catch (Exception e)
			{
				if (!silentErrors)
					MessageBox.Show("The download failed. Error message:\n" + e.Message, "Presets update failed",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			finally { client.Dispose(); }

			return true;
		}

		bool CheckForComponentUpdate()
		{
			var updateAvailable = false;
			var componentFactory = new SkyrimFactory();

			updateAvailable = componentFactory.CheckForUpdate();
			if (!updateAvailable)
			{
				try
				{
					using (XmlReader reader = XmlReader.Create(componentFactory.XMLURL))
					{
						XmlDocument doc = new XmlDocument();
						doc.Load(reader);
						foreach (XmlNode updateNode in doc.DocumentElement.ChildNodes)
						{
							Update update = UpdateManager.Update.Parse(updateNode);
							if (update.Version > componentFactory.Version)
							{
								updateAvailable = true;
								break;
							}
						}
					}
				}
				catch { }
			}

			if (updateAvailable)
				btnUpdatePresets.Enabled = false;

			return updateAvailable;
		}
	}
}
