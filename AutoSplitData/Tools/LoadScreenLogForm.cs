using LiveSplit.AutoSplitting;
using LiveSplit.Skyrim.AutoSplitData.Variables;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LiveSplit.Skyrim.AutoSplitData.Tools
{
	public partial class LoadScreenLogForm : Form
	{
		AutoSplitManager _manager;
		SynchronizationContext _uiThread;
		BindingList<LoadScreenEntry> _loadScreens;

		public LoadScreenLogForm()
		{
			InitializeComponent();

			locationStart.Value = locationEnd.Value = new Location(0, 0, 0);
			MaximumSize = new Size(Size.Width, Screen.AllScreens.Max(s => s.WorkingArea.Height));
			MinimumSize = new Size(Size.Width, Size.Height);

			_uiThread = SynchronizationContext.Current;
			_loadScreens = new BindingList<LoadScreenEntry>();

			Disposed += RamWatch_Disposed;
		}

		void AddLoadScreen(LoadScreenEntry loadscreen)
		{
			_loadScreens.Insert(0, loadscreen);
			lbLoadScreens.Items.Insert(0, loadscreen);
		}

		void RefreshListBox()
		{
			if (_loadScreens.Count == 0 && lbLoadScreens.Items.Count == 0)
				return;

			for (int i = 0; i < lbLoadScreens.Items.Count; i++)
				lbLoadScreens.Items[i] = lbLoadScreens.Items[i];
		}

		void RamWatch_Disposed(object sender, EventArgs e)
		{
			Detach();
		}

		public void Attach(AutoSplitManager manager)
		{
			if (_manager != null)
				Detach();

			_manager = manager;
			_manager.Updated += Manager_Updated;
		}

		public void Detach()
		{
			if (_manager != null)
			{
				_manager.Updated -= Manager_Updated;
				_manager = null;
			}
		}

		void Manager_Updated(object sender, AutoSplitManagerUpdateEventArgs e)
		{
			if (e.Event != SkyrimEvent.LoadScreenEnd)
				return;

			var data = (SkyrimData)e.Data;
			var start = data.LoadScreenStartLocation;
			var end = data.Location.Current;
			_uiThread.Post(d => AddLoadScreen(new LoadScreenEntry(start, end)), null);
		}

		void lbLoadScreens_SelectedIndexChanged(object sender, EventArgs e)
		{
			var isEntrySelected = lbLoadScreens.SelectedIndex != -1;
            gbLoadScreenInfo.Enabled = lCopyToClipboard.Visible = isEntrySelected;

			if (!isEntrySelected)
				return;

			var loadscreen = (LoadScreenEntry)lbLoadScreens.SelectedItem;
			locationStart.Value = loadscreen.StartLocation;
			locationEnd.Value = loadscreen.EndLocation;
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (_loadScreens.Count == 0)
				return;

			RefreshListBox();
		}

		void linkClear_Click(object sender, EventArgs e)
		{
			_loadScreens.Clear();
			lbLoadScreens.Items.Clear();
		}

		void lCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (lbLoadScreens.SelectedIndex == -1)
				return;
			var selectedEntry = (LoadScreenEntry)lbLoadScreens.SelectedItem;
			Clipboard.SetData(DataFormats.StringFormat,
				new LoadScreen(selectedEntry.StartLocation, selectedEntry.EndLocation)
					.ToXml(new System.Xml.XmlDocument())
					.OuterXml);
		}

		internal class LoadScreenEntry
		{
			public Location StartLocation { get; }
			public Location EndLocation { get; }
			public DateTime CreationDate { get; }

			public LoadScreenEntry(Location start, Location end)
			{
				CreationDate = DateTime.UtcNow;
				StartLocation = start;
				EndLocation = end;
			}

			public override string ToString()
			{
				var startStr = ((int)StartLocation.ID).ToString("X8");
				var endStr = ((int)EndLocation.ID).ToString("X8");

				var startWorldName = Enum.GetName(typeof(Worlds), StartLocation.ID);
				var endWorldName = Enum.GetName(typeof(Worlds), EndLocation.ID);
				if (!string.IsNullOrEmpty(startWorldName))
					startStr = startWorldName;
				if (!string.IsNullOrEmpty(endWorldName))
					endStr = endWorldName;

				var minutesSinceCreation = Math.Round((DateTime.UtcNow - CreationDate).TotalMinutes, 1);

                return $"{minutesSinceCreation.ToString("0.0")} min ago: {startStr} -> {endStr}";
			}
		}
	}
}
