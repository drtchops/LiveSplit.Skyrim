using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LiveSplit.AutoSplitting.Variables
{
	public class Variable
	{
		/// <summary>
		/// If not null, the variable will only be available for these events.
		/// </summary>
		public virtual GameEvent[] RestrictedEvents { get; }

		protected Func<GameData, bool> Predicate { get; set; }

		public Variable(Func<GameData, bool> predicate = null)
		{
			Predicate = predicate;
		}

		public virtual bool Verify(GameData data) => Predicate?.Invoke(data) ?? false;

		public virtual XmlElement ToXml(XmlDocument doc)
		{
			var root = doc.CreateElement("Variable");
			root.SetAttribute("type", RemoveNamespace(GetType().FullName));
			return root;
		}

		public static Variable FromXml(XmlElement elem, AutoSplitEnv env)
		{
			var typeStr = elem.GetAttribute("type");
			var type = DeserializeType(typeStr, env);
			return (Variable)Activator.CreateInstance(type, elem);
		}

		static Type DeserializeType(string str, AutoSplitEnv env)
		{
			str = RemoveNamespace(str);
			var genericTypeStr = str;
			Type[] genericParams = null;

			if (str.IndexOf('`') != -1)
			{
				var startIndex = str.IndexOf("[[") + 2;
				var endIndex = str.LastIndexOf("]]") - 1;
				var paramsStr = str.Substring(startIndex, endIndex - startIndex);
				genericParams = paramsStr.Split(new string[] { "],[" }, StringSplitOptions.RemoveEmptyEntries)
					.Select(s => Type.GetType(s)).ToArray();
				genericTypeStr = str.Substring(0, str.IndexOf("[["));
			}

			var type = env.VariableTypes.Keys.FirstOrDefault(t => t.Name == genericTypeStr);
			if (genericParams != null)
				type = type.MakeGenericType(genericParams);
			return type;
		}

		public static string RemoveNamespace(string str)
		{
			var space = str;
			var genericParams = "";
			if (str.IndexOf('`') != -1)
			{
				space = str.Substring(0, str.IndexOf('`'));
				genericParams = str.Replace(space, "");
			}

			var split = space.Split('.');
			var typeName = split[split.Length - 1];
			return typeName + genericParams;
		}

		public static bool IsValidXml(XmlElement elem)
		{
			if (elem == null || elem.GetAttribute("type") == null)
				return false;

			return true;
		}

		public static IEnumerable<Variable> VariablesFromXml(XmlElement elem, AutoSplitEnv env)
		{
			var vars = new List<Variable>();

			foreach (XmlElement child in elem.ChildNodes)
				vars.Add(FromXml(child, env));

			return vars;
		}

		/// <summary>
		/// Returns a deep copy of the current <see cref="Variable"/>.
		/// </summary>
		public virtual Variable Clone(AutoSplitEnv env) => FromXml(ToXml(new XmlDocument()), env);

		public override string ToString() => GetType().Name;
		public virtual string ToString(AutoSplitEnv env) => ToString();
	}

	static class VariableExtensions
	{
		public static GameEvent[] GetRestrictedEvents(this IEnumerable<Variable> variables)
		{
			IEnumerable<GameEvent> commonEvents = null;
			foreach (var variable in variables)
			{
				if (variable.RestrictedEvents == null)
					continue;

				commonEvents = commonEvents != null
					? variable.RestrictedEvents.Intersect(commonEvents)
					: variable.RestrictedEvents;
			}

			return commonEvents?.ToArray();
		}

		public static bool AreCompatible(this IEnumerable<Variable> variables, Variable variable)
		{
			var splitRestrictedEvents = variables.GetRestrictedEvents();
			return splitRestrictedEvents == null || variable.RestrictedEvents == null ||
				variable.RestrictedEvents.Any(varEvent => splitRestrictedEvents.Any(splitEvent => splitEvent == varEvent));
		}

		public static XmlElement ToXml(this IEnumerable<Variable> array, XmlDocument doc)
		{
			var root = doc.CreateElement("Variables");
			foreach (var var in array)
				root.AppendChild(var.ToXml(doc));
			return root;
		}

		public static Variable[] Clone(this IEnumerable<Variable> vars, AutoSplitEnv env)
		{
			var source = vars.ToArray();
			var clone = new Variable[source.Length];

			for (int i = 0; i < source.Length; i++)
				clone[i] = source[i].Clone(env);

			return clone;
		}
	}
}
