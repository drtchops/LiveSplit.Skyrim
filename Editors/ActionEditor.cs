using System.Linq;
using Action = LiveSplit.AutoSplitting.Variables.Action;

namespace LiveSplit.AutoSplitting.Editors
{
	public partial class ActionEditor : VariableControl
	{
		public new Action Value
		{
			get { return (Action)base.Value; }
			set { base.Value = value; }
		}

		public ActionEditor(AutoSplitEnv env, Action source) : base(env, source)
		{
			InitializeComponent();

			var events = env.Events.Select(e => GameEvent.GetOriginal(env.GameEventTypes, e) ?? e)
				.Where(e => e != GameEvent.Always && !e.Hidden)
				.OrderBy(e => e)
				.ToList();

			Value = (Action)source?.Clone(env) ?? new Action(events.Min());

			cbEvent.DataSource = events;
			cbEvent.DataBindings.Add("SelectedItem", Value, nameof(Value.Value));
		}
	}
}
