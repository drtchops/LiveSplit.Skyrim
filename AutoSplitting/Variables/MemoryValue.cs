using LiveSplit.AutoSplitting.Editors;
using LiveSplit.AutoSplitting.Variables;
using LiveSplit.ComponentUtil;
using LiveSplit.UI;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;

[assembly: VariableRegister(typeof(MemoryValue<>), typeof(MemoryValueEditor))]
[assembly: VariableRegister(typeof(StringValue), typeof(MemoryValueEditor), Hidden = true)]

namespace LiveSplit.AutoSplitting.Variables
{
	public enum Comparison
	{
		Equals,
		Unequals,
		LessThan,
		LessThanOrEqualTo,
		GreaterThan,
		GreaterThanOrEqualTo,
		IncreasedBy,
		DecreasedBy,
	}

	public abstract class MemoryValue : Variable
	{
		public string MemoryWatcherName { get; set; }

		public Comparison Comparison { get; set; }

		public object Value { get; set; }

		public bool OnValueChanged { get; set; }

		protected MemoryValue() { }

		public MemoryValue(string watcher, Comparison comparison, object value, bool onValueChanged = false)
		{
			MemoryWatcherName = watcher;
			Comparison = comparison;
			Value = value;
			OnValueChanged = onValueChanged;
		}

		/// <summary>
		/// Initialize all properties from XML except <see cref="Value"/>.
		/// </summary>
		/// <param name="elem"></param>
		public MemoryValue(XmlElement elem)
		{
			MemoryWatcherName = elem["MemoryWatcherName"]?.InnerText;
			if (elem["Comparison"] != null)
				Comparison = ParseComparison(elem["Comparison"].InnerText);
			if (elem["OnValueChanged"] != null)
				OnValueChanged = bool.Parse(elem["OnValueChanged"].InnerText);
		}

		public override bool Verify(GameData data)
		{
			var watcher = data.FirstOrDefault(w => w.Name == MemoryWatcherName);
			if (watcher == null || (OnValueChanged && !watcher.Changed))
				return false;
			return GetPredicate().Invoke(watcher);
		}

		public override XmlElement ToXml(XmlDocument doc)
		{
			var root = base.ToXml(doc);

			root.AppendChild(SettingsHelper.ToElement(doc, "MemoryWatcherName", MemoryWatcherName));
			root.AppendChild(SettingsHelper.ToElement(doc, "Comparison", Enum.GetName(typeof(Comparison), Comparison)));
			root.AppendChild(SettingsHelper.ToElement(doc, "Value", Value));
			root.AppendChild(SettingsHelper.ToElement(doc, "OnValueChanged", OnValueChanged));

			return root;
		}

		protected virtual Predicate<MemoryWatcher> GetPredicate()
		{
			dynamic value = Value;
			switch (Comparison)
			{
				case Comparison.Equals:
					return w => (dynamic)w.Current == value;
				case Comparison.Unequals:
					return w => (dynamic)w.Current != value;
				case Comparison.GreaterThan:
					return w => (dynamic)w.Current > value;
				case Comparison.GreaterThanOrEqualTo:
					return w => (dynamic)w.Current >= value;
				case Comparison.LessThan:
					return w => (dynamic)w.Current < value;
				case Comparison.LessThanOrEqualTo:
					return w => (dynamic)w.Current <= value;
				case Comparison.IncreasedBy:
					return w => (dynamic)w.Current - (dynamic)w.Old == value;
				case Comparison.DecreasedBy:
					return w => (dynamic)w.Old - (dynamic)w.Current == value;
				default:
					throw new InvalidOperationException("Undefined comparison behavior.");
			}
		}

		protected Comparison ParseComparison(string str)
		{
			return (Comparison)Enum.Parse(typeof(Comparison), str);
		}
	}

	[VariableDescriptor(Description = "Checks the value of a memory address.")]
	sealed class MemoryValue<T> : MemoryValue where T : struct
	{
		public new T Value
		{
			get { return (T)base.Value; }
			set { base.Value = value; }
		}

		public MemoryValue(string watcher, Comparison comparison, T value, bool onValueChanged = false)
			: base(watcher, comparison, value, onValueChanged)
		{ }

		public MemoryValue(XmlElement elem) : base(elem)
		{
			Value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(elem["Value"].InnerText);
		}

		public override string ToString() => $"MemoryValue: {MemoryWatcherName} -> {Value}";
	}

	sealed class StringValue : MemoryValue
	{
		public new string Value
		{
			get { return (string)base.Value; }
			set { base.Value = value; }
		}

		public bool IgnoreCase { get; set; }

		public bool IsContained { get; set; }

		public StringValue(string watcher, Comparison comparison, string value, bool onValueChanged = false)
			: base(watcher, comparison, value, onValueChanged)
		{ }

		protected override Predicate<MemoryWatcher> GetPredicate()
		{
			var value = IgnoreCase ? Value.ToLowerInvariant() : Value;
			return w =>
				{
					var str = IgnoreCase
						? ((string)w.Current).ToLowerInvariant()
						: (string)w.Current;

					if (IsContained)
						return str.Contains(value);

					return (str == value) == (Comparison == Comparison.Equals);
				};
		}

		public StringValue(XmlElement elem) : base(elem)
		{
			Value = elem["Value"].InnerText;
			IgnoreCase = bool.Parse(elem["Comparison"].GetAttribute("ignoreCase"));
			IsContained = bool.Parse(elem["Comparison"].GetAttribute("isContained"));
		}

		public override XmlElement ToXml(XmlDocument doc)
		{
			var root = base.ToXml(doc);
			root["Comparison"].SetAttribute("ignoreCase", IgnoreCase.ToString());
			root["Comparison"].SetAttribute("isContained", IsContained.ToString());
			return root;
		}

		public override string ToString() => $"MemoryValue: {MemoryWatcherName} -> {Value}";
	}
}
