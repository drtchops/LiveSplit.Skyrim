using LiveSplit.AutoSplitting;
using LiveSplit.AutoSplitting.Editors;
using LiveSplit.AutoSplitting.Variables;
using LiveSplit.Skyrim.AutoSplitData.Variables;

namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	public partial class LoadScreenEditor : VariableControl
	{
		public override Variable Value => new LoadScreen(locationStart.Value, locationStart.ValueEquals,
				locationEnd.Value, locationEnd.ValueEquals);

		public LoadScreenEditor(AutoSplitEnv env, LoadScreen source) : base(env, source)
		{
			InitializeComponent();

			if (source != null)
			{
				locationStart.Value = source.StartLocation;
				locationEnd.Value = source.EndLocation;
				locationStart.ValueEquals = source.EqualsStartLocation;
				locationEnd.ValueEquals = source.EqualsEndLocation;
			}
		}
	}
}
