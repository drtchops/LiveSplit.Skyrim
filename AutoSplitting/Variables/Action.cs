using LiveSplit.AutoSplitting.Editors;
using LiveSplit.AutoSplitting.Variables;
using System.Xml;

[assembly: VariableRegister(typeof(Action), typeof(ActionEditor))]

namespace LiveSplit.AutoSplitting.Variables
{
	[VariableDescriptor(AllowMultiple = false, Description = "Makes the autosplit trigger only on a certain action.")]
	public sealed class Action : Variable
	{
		public override GameEvent[] RestrictedEvents => new GameEvent[] { Value };

		public GameEvent Value { get; set; }

		public override bool Verify(GameData data) => true;

		public Action(GameEvent evnt)
		{
			Value = evnt;
		}

		public Action(XmlElement element)
		{
			Value = GameEvent.FromXml(element[nameof(Value)]);
		}

		public override XmlElement ToXml(XmlDocument doc)
		{
			var root = base.ToXml(doc);
            root.AppendChild(Value.ToXml(doc, nameof(Value)));
			return root;
		}

		public override string ToString(AutoSplitEnv env) => $"{nameof(Action)} -> {GameEvent.GetOriginal(env.GameEventTypes, Value)?.Name ?? Value.InternalValue.ToString()}";
	}
}
