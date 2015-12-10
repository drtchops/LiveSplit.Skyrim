using LiveSplit.AutoSplitting.Variables;
using LiveSplit.ComponentUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LiveSplit.AutoSplitting
{
	public class AutoSplitEnv
	{
		public IDictionary<Type, Type> VariableTypes { get; }
		public ISet<Type> HiddenVariables { get; }
		public ISet<Type> GameEventTypes { get; }

		public IEnumerable<MemoryWatcher> Addresses { get; set; }
		public IEnumerable<GameEvent> Events { get; set; }
		public IEnumerable<AutoSplitList> Presets { get; set; }

		Type _defaultVariableType = typeof(MemoryValue<>);
		public Type DefaultVariableType
		{
			get { return _defaultVariableType; }
			set
			{
				if (!value.IsSubclassOf(typeof(Variable)))
					throw new ArgumentException($"The variable must be a subclass of {nameof(Variable)}");
				if (!VariableTypes.ContainsKey(value))
					throw new ArgumentException("The variable type must have an accessible editor.");
				if (HiddenVariables.Contains(value))
					throw new ArgumentException("The variable type cannot be hidden.");
				_defaultVariableType = value;
			}
		}

		public AutoSplitEnv()
		{
			VariableTypes = new Dictionary<Type, Type>();
			HiddenVariables = new HashSet<Type>();
			GameEventTypes = new HashSet<Type>() { typeof(GameEvent) };

			LoadEventTypes();
			LoadVariableTypes();
		}

		public void LoadVariableTypes(Assembly assembly)
		{
			var varRegisters = assembly.GetCustomAttributes(typeof(VariableRegisterAttribute), false);
			foreach (VariableRegisterAttribute attr in varRegisters)
			{
				if (!VariableTypes.ContainsKey(attr.VariableType) || attr.EditorType != null)
					VariableTypes[attr.VariableType] = attr.EditorType;
				if (attr.Hidden)
					HiddenVariables.Add(attr.VariableType);
			}
		}
		public void LoadVariableTypes() => LoadVariableTypes(Assembly.GetCallingAssembly());

		public void LoadEventTypes(Assembly assembly)
		{
			var types = assembly.GetCustomAttributes(typeof(GameEventRegisterAttribute), false)
				.Select(a => ((GameEventRegisterAttribute)a).Type);
			foreach (var t in types)
				GameEventTypes.Add(t);
		}
		public void LoadEventTypes() => LoadEventTypes(Assembly.GetCallingAssembly());

		public Type GetEditor(Type variableType)
		{
			if (variableType.IsGenericType)
				variableType = variableType.GetGenericTypeDefinition();

			Type type = null;
			VariableTypes.TryGetValue(variableType, out type);
			return type;
		}

		public AutoSplitEnv Clone()
		{
			var clone = (AutoSplitEnv)MemberwiseClone();
			clone.Addresses = Addresses.ToList();
			clone.Events = Events.ToList();
			clone.Presets = Presets.ToList();
			return clone;
		}
	}
}
