using LiveSplit.AutoSplitting.Variables;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace LiveSplit.AutoSplitting
{
	[DebuggerDisplay("Name = {Name}, Event = {Event}")]
	public class AutoSplit : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<Variable> Variables { get; }

		string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				var old = _name;
				_name = value;
				if (old != value)
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
			}
		}

		GameEvent _event;
		public GameEvent Event
		{
			get { return _event; }
			protected set
			{
				if (!IsEventAvailable(value))
					throw new ArgumentException("Event unavailable.");

				var old = _event;
				_event = value;
				if (old != value)
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Event)));
			}
		}

		bool _triggered;
		public bool Triggered
		{
			get { return _triggered; }
			set
			{
				var old = _triggered;
				_triggered = value;
				if (old != value)
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Triggered)));
			}
		}

		bool _enabled;
		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				var old = _enabled;
				_enabled = value;
				if (old != value)
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
			}
		}

		public AutoSplit(string name = "", params Variable[] vars)
		{
			Name = name;
			Variables = new ObservableCollection<Variable>(vars);
			Variables.CollectionChanged += Variables_CollectionChanged;
			_event = GetDefaultEvent();
			Enabled = true;
		}

		public AutoSplit(string name, IEnumerable<Variable> vars) : this(name, vars.ToArray()) { }

		void Variables_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Move)
				return;

			Event = GetDefaultEvent();
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Variables)));
		}

		public bool Verify(GameData data)
		{
			for (int i = 0; i < Variables.Count; i++)
			{
				if (!Variables[i].Verify(data))
					return false;
			}
			return Variables.Count > 0;
		}

		public GameEvent GetDefaultEvent() => Variables.GetRestrictedEvents()?.Min() ?? GameEvent.Always;

		public bool IsEventAvailable(GameEvent evnt) => Variables.GetRestrictedEvents()?.Contains(evnt) ?? true;

		public XmlElement ToXml(XmlDocument doc, string name = "AutoSplit")
		{
			var root = doc.CreateElement(name);

			root.AppendChild(SettingsHelper.ToElement(doc, "Name", Name));
			root.AppendChild(SettingsHelper.ToElement(doc, "Enabled", Enabled));
			root.AppendChild(Variables.ToXml(doc));

			return root;
		}

		public static AutoSplit FromXml(XmlElement elem, AutoSplitEnv env)
		{
			return new AutoSplit(elem["Name"].InnerText, Variable.VariablesFromXml(elem["Variables"], env).ToArray())
			{
				Enabled = SettingsHelper.ParseBool(elem["Enabled"]),
			};
		}

		public static bool IsValidXml(XmlElement elem)
		{
			var expectedNodes = new string[]
			{
				"Name",
				"Enabled",
				"Variables"
			};

			if (elem == null || !expectedNodes.All(name => elem[name] != null))
				return false;

			foreach (XmlElement varElem in elem["Variables"].ChildNodes)
			{
				if (!Variable.IsValidXml(varElem))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Returns a deep copy of the current <see cref="AutoSplit"/>.
		/// </summary>
		public AutoSplit Clone(AutoSplitEnv env) => new AutoSplit(Name, Variables.Clone(env))
		{
			Enabled = Enabled,
			Triggered = Triggered,
			Event = Event
		};
	}
}
