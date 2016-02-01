using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	public partial class LocationArrayControl : UserControl
	{
		public Location[] Value
		{
			get
			{
				var locations = new List<Location>();
				foreach (TabPage page in tabControl1.TabPages)
				{
					var locationControl = (LocationControl)page.Controls[0];
					locations.Add(locationControl.Value);
				}

				return locations.ToArray();
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				SetTabCount(value.Length);
				SetTabValues(value);
			}
		}

		public int TabCount => tabControl1.TabCount;

		public int SelectedTabIndex => tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);

		public LocationArrayControl()
		{
			InitializeComponent();

			SetTabCount(5);
			SetTabCount(1);

			ContextMenuStrip = new ContextMenuStrip();
			ContextMenuStrip.Items.Add("Add location", null, (s, e) =>
			{
				AddLocationTab();
				tabControl1.SelectTab(tabControl1.TabCount - 1);
			});
		}

		public Location[] GetResult()
		{
			var locations = new List<Location>();
			foreach (TabPage page in tabControl1.TabPages)
			{
				var locationControl = (LocationControl)page.Controls[0];
				locations.Add(locationControl.Value);
			}

			return locations.ToArray();
		}

		public void AddLocationTab(Location location)
		{
			var newPage = new TabPage($"Location {tabControl1.TabCount + 1}");

			if (tabControl1.TabCount > 0)
			{
				newPage.ContextMenuStrip = new ContextMenuStrip();
				newPage.ContextMenuStrip.Items.Add("Remove location", null, (s, e) =>
				{
					tabControl1.TabPages.Remove(newPage);
					UpdateTabPagesTitle();
				});
			}

			var locationControl = new LocationControl()
			{
				Dock = DockStyle.Fill,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink
			};
			locationControl.Value = location;
			newPage.Controls.Add(locationControl);
			tabControl1.TabPages.Add(newPage);
		}

		public void AddLocationTab() => AddLocationTab(new Location());

		public void SetTabCount(int nbr)
		{
			if (tabControl1.TabCount == nbr)
				return;

			if (nbr < 1)
				nbr = 1;

			if (tabControl1.TabCount < nbr)
			{
				for (int i = tabControl1.TabCount; i < nbr; i++)
					AddLocationTab();
			}
			else
			{
				for (int i = tabControl1.TabCount - 1; i >= nbr; i--)
					tabControl1.TabPages.RemoveAt(i);
				UpdateTabPagesTitle();
			}
		}

		public void RemoveTabAt(int index)
		{
			tabControl1.TabPages.RemoveAt(index);
			UpdateTabPagesTitle();
		}

		void SetTabValues(Location[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				var control = (LocationControl)tabControl1.TabPages[i].Controls[0];
				control.Value = array[i];
			}
		}

		void UpdateTabPagesTitle()
		{
			for (int i = 0; i < tabControl1.TabCount; i++)
				tabControl1.TabPages[i].Text = "Location " + (i + 1);
		}
	}
}