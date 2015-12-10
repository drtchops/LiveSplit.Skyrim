using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace LiveSplit.AutoSplitting
{
	public class GameEvent : IComparable<GameEvent>
	{
		public static readonly GameEvent Always = new GameEvent(int.MinValue);

		Lazy<string> _name;
		public string Name => _name.Value;

		public bool Hidden { get; }

		public int InternalValue { get; }

		protected GameEvent(int value, bool hidden = false)
		{
			InternalValue = value;
			Hidden = hidden;
			_name = new Lazy<string>(() =>
			{
				return GetFields(GetType())
					.FirstOrDefault(f => ((GameEvent)f.GetValue(null)).InternalValue == InternalValue)
					?.Name;
            });
		}

		public XmlElement ToXml(XmlDocument doc, string name = "Event")
		{
			return LiveSplit.UI.SettingsHelper.ToElement(doc, name, InternalValue);
		}

		public static GameEvent FromXml(XmlElement elem)
		{
			return (GameEvent)int.Parse(elem.InnerText);
		}

		public static GameEvent GetOriginal(IEnumerable<Type> eventTypes, GameEvent fake)
		{
			if (fake.Name != null)
				return fake;
			return eventTypes.SelectMany(t => GetValues(t))
				.FirstOrDefault(g => g.InternalValue == fake.InternalValue);
        }
		public static GameEvent GetOriginal(Type eventType, GameEvent fake) => GetOriginal(new Type[] { eventType }, fake);

		/// <summary>
		/// Returns all Events in a <see cref="GameEvent"/> type
		/// </summary>
		public static GameEvent[] GetValues(Type type)
		{
			if (type != typeof(GameEvent) && !type.IsSubclassOf(typeof(GameEvent)))
				throw new ArgumentException("The type must derive from GameEvent.", nameof(type));

			var events = new List<GameEvent>();
			foreach (var f in GetFields(type))
				events.Add((GameEvent)f.GetValue(null));

			return events.ToArray();
		}
		public static GameEvent[] GetValues() => GetValues(typeof(GameEvent));

		static IEnumerable<FieldInfo> GetFields(Type TGameEvent)
		{
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
			return TGameEvent.GetFields(bindingFlags)
				.Where(f => f.IsStatic && (f.FieldType == typeof(GameEvent) || f.FieldType.IsSubclassOf(typeof(GameEvent))));
		}

		public override string ToString() => Name ?? InternalValue.ToString();

		public int CompareTo(GameEvent other) => InternalValue.CompareTo(other.InternalValue);

		public override bool Equals(object obj) => obj is GameEvent && this == (GameEvent)obj;

		public static bool operator ==(GameEvent g, GameEvent g2) => g?.InternalValue == g2?.InternalValue;

		public static bool operator !=(GameEvent g, GameEvent g2) => !(g == g2);

		public override int GetHashCode() => InternalValue.GetHashCode();

		public static explicit operator GameEvent(int nbr) => new GameEvent(nbr, true);
	}
}
