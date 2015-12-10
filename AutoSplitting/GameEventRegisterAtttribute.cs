using System;

namespace LiveSplit.AutoSplitting
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class GameEventRegisterAttribute : Attribute
	{
		public Type Type { get; }

		public bool Hidden { get; set; }

		public GameEventRegisterAttribute(Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));
			else if (!type.IsSubclassOf(typeof(GameEvent)))
				throw new ArgumentException($"{nameof(type)} must be a sub class of GameEvent", nameof(type));

			Type = type;
		}
	}
}
