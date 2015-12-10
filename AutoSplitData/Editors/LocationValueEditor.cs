using LiveSplit.AutoSplitting;
using LiveSplit.AutoSplitting.Editors;
using LiveSplit.AutoSplitting.Variables;
using LiveSplit.Skyrim.AutoSplitData.Variables;

namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	public partial class LocationValueEditor : VariableControl
	{
		public override Variable Value => new LocationValue(locationValueControl1.ValueEquals, locationValueControl1.Value, chkOnChange.Checked);

		readonly AutoSplitEnv _env;

		public LocationValueEditor(AutoSplitEnv env, LocationValue source = null) : base(env, source)
		{
			InitializeComponent();

			_env = env;

			cbAddresses.Text = "Location";

			if (source != null)
			{
				locationValueControl1.Value = source.Value;
				locationValueControl1.ValueEquals = source.ValueEquals;
				chkOnChange.Checked = source.OnValueChanged;
			}
		}
	}
}
