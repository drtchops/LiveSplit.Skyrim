using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	public partial class LocationControl : UserControl
	{
		public Location Value
		{
			get { return new Location(WorldID, CellX, CellY); }
			set
			{
				WorldID = value.ID;
				CellX = value.worldX;
				CellY = value.worldY;
			}
		}

		public int? WorldID
		{
			get
			{
				if (chkWorldAny.Checked)
					return null;
				return (int)numWorldID.Value;
			}
			set
			{
				chkWorldAny.Checked = value == null;
				if (value != null)
					numWorldID.Value = value.Value;
			}
		}

		public int? CellX
		{
			get
			{
				if (chkCellXAny.Checked)
					return null;
				return (int)numCellX.Value;
			}
			set
			{
				chkCellXAny.Checked = value == null;
				if (value != null)
					numCellX.Value = value.Value;
			}
		}

		public int? CellY
		{
			get
			{
				if (chkCellYAny.Checked)
					return null;
				return (int)numCellY.Value;
			}
			set
			{
				chkCellYAny.Checked = value == null;
				if (value != null)
					numCellY.Value = value.Value;
			}
		}

		public bool ReadOnly
		{
			get { return !chkWorldAny.Enabled; }
			set
			{
				chkWorldAny.Enabled = chkCellXAny.Enabled = chkCellYAny.Enabled =
					btnWorldIDList.Visible = !value;
				numWorldID.ReadOnly = numCellX.ReadOnly = numCellY.ReadOnly = value;
				numWorldID.Increment = numCellX.Increment = numCellY.Increment = 0;

				chkWorldAny.Visible = !value || chkWorldAny.Checked;
				chkCellXAny.Visible = !value || chkCellXAny.Checked;
				chkCellYAny.Visible = !value || chkCellYAny.Checked;
			}
		}

		public LocationControl()
		{
			InitializeComponent();

			numWorldID.ValueChanged += (s, e) => UpdateWorldName();
			numWorldID.EnabledChanged += (s, e) => UpdateWorldName();
			chkWorldAny.CheckedChanged += checkBox_HideOnChecked;
			chkCellXAny.CheckedChanged += checkBox_HideOnChecked;
			chkCellYAny.CheckedChanged += checkBox_HideOnChecked;

			var cms = new ContextMenuStrip();
			cms.MaximumSize = new Size(0, 300);
			var locationNames = Enum.GetNames(typeof(Worlds)).OrderBy(n => n);
			foreach (string name in locationNames)
			{
				var value = (Worlds)Enum.Parse(typeof(Worlds), name);
				cms.Items.Add(name, null, (s, e) => WorldID = (int)value);
			}

			btnWorldIDList.Click += (s, e) => cms.Show(btnWorldIDList.Parent.PointToScreen(btnWorldIDList.Location));
		}

		void UpdateWorldName()
		{
			var locationName = numWorldID.Enabled
				? Enum.GetName(typeof(Worlds), (int)numWorldID.Value)
				: string.Empty;
			if (!string.IsNullOrEmpty(locationName))
				locationName = $" ({locationName})";
			gbWorld.Text = "World" + locationName;
		}

		void chkWorldAny_CheckedChanged(object sender, EventArgs e)
		{
			numWorldID.Enabled = !chkWorldAny.Checked;
		}

		void chkCellXAny_CheckedChanged(object sender, EventArgs e)
		{
			numCellX.Enabled = !chkCellXAny.Checked;
		}

		void chkCellYAny_CheckedChanged(object sender, EventArgs e)
		{
			numCellY.Enabled = !chkCellYAny.Checked;
		}

		void checkBox_HideOnChecked(object sender, EventArgs e)
		{
			var chk = (CheckBox)sender;
			chk.Visible = !ReadOnly || chk.Checked;
		}
	}
}
