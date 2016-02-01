using System.Windows.Forms;

namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	public partial class LocationEditor : Form
	{
		public Location[] Value => locationArrayControl1.Value;

		public LocationEditor(Location[] location)
		{
			InitializeComponent();

			if (location != null)
				locationArrayControl1.Value = location;
		}

		public static Location[] ShowEditor(Location[] location = null)
		{
			using (var form = new LocationEditor(location))
			{
				return form.ShowDialog() != DialogResult.Cancel
					? form.Value
					: location;
			}
		}
	}
}
