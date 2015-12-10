using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveSplit.Properties;
using LiveSplit.AutoSplitting.Variables;
using System.Xml;
using LiveSplit.Skyrim.AutoSplitData.Variables;

namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	public partial class LocationValueControl : UserControl
	{
		public bool ValueEquals
		{
			get { return (Comparison)cbComparison.SelectedItem == Comparison.Equals; }
			set { cbComparison.SelectedItem = value ? Comparison.Equals : Comparison.Unequals; }
		}

		public Location[] Value
		{
			get { return locationArrayControl1.Value; }
			set { locationArrayControl1.Value = value; }
		}

		public LocationValueControl(Location[] value, bool valueEquals = true)
		{
			InitializeComponent();

			btnAddTab.Text = btnRemoveTab.Text = string.Empty;
			btnAddTab.BackgroundImage = Resources.Add;
			btnRemoveTab.BackgroundImage = Resources.Remove;
			foreach (var btn in flpTabButtons.Controls.OfType<Button>())
			{
				btn.FlatAppearance.MouseOverBackColor = Color.LightGray;
				btn.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
			}

			toolTipTabButtons.SetToolTip(btnAddTab, "Add a location tab");
			toolTipTabButtons.SetToolTip(btnRemoveTab, "Remove selected location tab");
			cbComparison.DataSource = new Comparison[] { Comparison.Equals, Comparison.Unequals };

			btnAddTab.Click += BtnAddTab_Click;

			ValueEquals = valueEquals;
			Value = value;
		}

		public LocationValueControl() : this(new Location[] { AutoSplitData.Location.Anywhere }) { }

		void BtnAddTab_Click(object sender, EventArgs ea)
		{
			var addTabBtnCms = new ContextMenuStrip();

			addTabBtnCms.Items.Add("Add new tab", null, (s, e) => locationArrayControl1.AddLocationTab());

			var locationListCms = new ToolStripMenuItem("Add from list");
			foreach (var pair in AutoSplitData.Location.StaticLocations.OrderBy(p => p.Key))
			{
				locationListCms.DropDownItems.Add(pair.Key, null, (s, e) =>
				{
					foreach (var location in pair.Value)
						locationArrayControl1.AddLocationTab(location);
				});
			}
			addTabBtnCms.Items.Add(locationListCms);

			if (Clipboard.ContainsData(DataFormats.StringFormat))
			{
				LoadScreen loadScreen = null;
				try
				{
					var clipboard = (string)Clipboard.GetData(DataFormats.StringFormat);

					var doc = new XmlDocument();
					doc.LoadXml(clipboard);
					if (Variable.IsValidXml(doc.DocumentElement))
						loadScreen = new LoadScreen(doc.DocumentElement);
				}
				catch { loadScreen = null; }

				if (loadScreen != null)
				{
					addTabBtnCms.Items.Add("Paste Start Location from clipboard", null, (s, e) =>
					{
						locationArrayControl1.Value = loadScreen.StartLocation;
                    });
					addTabBtnCms.Items.Add("Paste End Location from clipboard", null, (s, e) =>
					{
						locationArrayControl1.Value = loadScreen.EndLocation;
                    });
				}
			}

            addTabBtnCms.Show(btnAddTab, new Point(0, 0));
		}

		void btnRemoveTab_Click(object sender, EventArgs e)
		{
			if (locationArrayControl1.TabCount > 1)
				locationArrayControl1.RemoveTabAt(locationArrayControl1.SelectedTabIndex);
		}
	}
}
