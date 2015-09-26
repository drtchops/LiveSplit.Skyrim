using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveSplit.Properties;
using LiveSplit.AutoSplitting.Variables;

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

			var addTabBtnCms = new ContextMenuStrip();
			addTabBtnCms.Items.Add("Add new tab", null, (s, e) => locationArrayControl1.AddLocationTab());
			var locationListCms = new ToolStripMenuItem("Add from list");
			foreach (var pair in AutoSplitData.Location.StaticLocations)
			{
				locationListCms.DropDownItems.Add(pair.Key, null, (s, e) =>
				{
					foreach (var location in pair.Value)
						locationArrayControl1.AddLocationTab(location);
				});
			}
			addTabBtnCms.Items.Add(locationListCms);

			btnAddTab.Click += (s, e) => addTabBtnCms.Show(btnAddTab, new Point(0, 0));

			ValueEquals = valueEquals;
			Value = value;
		}

		public LocationValueControl() : this(new Location[] { AutoSplitData.Location.Anywhere }) { }

		void btnRemoveTab_Click(object sender, EventArgs e)
		{
			if (locationArrayControl1.TabCount > 1)
				locationArrayControl1.RemoveTabAt(locationArrayControl1.SelectedTabIndex);
		}
	}
}
