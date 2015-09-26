using LiveSplit.AutoSplitting.Variables;
using LiveSplit.Skyrim.AutoSplitData.Editors;
using LiveSplit.Skyrim.AutoSplitData.Variables;
using System.Xml;

[assembly: VariableRegister(typeof(LocationValue), typeof(LocationValueEditor))]

namespace LiveSplit.Skyrim.AutoSplitData.Variables
{
	public sealed class LocationValue : MemoryValue
	{
		static readonly string _watcherName = new SkyrimData().Location.Name;
		public new string MemoryWatcherName => _watcherName;

		public new Location[] Value
		{
			get { return (Location[])base.Value; }
			set { base.Value = value; }
		}

		public bool ValueEquals
		{
			get { return Comparison == Comparison.Equals; }
			set { Comparison = value ? Comparison.Equals : Comparison.Unequals; }
		}

		public LocationValue(bool valueEquals, Location[] location, bool onChange = false)
			: base(_watcherName, Comparison.Equals, location, onChange)
		{
			ValueEquals = valueEquals;
		}

		public LocationValue(XmlElement elem) : base(elem)
		{
			Value = Location.ArrayFromXml(elem["Value"]);
			base.MemoryWatcherName = _watcherName;
		}

		public override XmlElement ToXml(XmlDocument doc)
		{
			var root = base.ToXml(doc);

			root.RemoveChild(root["MemoryWatcherName"]);
			root.ReplaceChild(Value.ToXml(doc, "Value"), root["Value"]);

			return root;
		}
	}
}
