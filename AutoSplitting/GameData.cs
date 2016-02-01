using LiveSplit.ComponentUtil;
using System;
using System.Linq;
using System.Reflection;

namespace LiveSplit.AutoSplitting
{
	public abstract class GameData : MemoryWatcherList
	{
		public GameData()
		{
			//add all memory watchers to the list and set the name to the property name
			foreach (PropertyInfo p in GetType().GetProperties())
			{
				if (p.PropertyType.IsSubclassOf(typeof(MemoryWatcher)))
				{
					var watcher = p.GetValue(this, null) as MemoryWatcher;
					watcher.Name = p.Name;
					Add(watcher);
				}
			}
		}

		public new void Add(MemoryWatcher watcher)
		{
			if (string.IsNullOrEmpty(watcher.Name))
				throw new ArgumentException("Name property is undefined.");
			else if (this.Any(s => s.Name == watcher.Name))
				throw new ArgumentException("A MemoryWatcher with the same name is already in the list.");
			base.Add(watcher);
		}

		public virtual void Reset() { }
	}

	public class FakeMemoryWatcher<T> : MemoryWatcher<T> where T : struct
	{
		public Func<T> OnUpdate { get; set; }

		public FakeMemoryWatcher(T old = default(T), T current = default(T)) : base(null)
		{
			Old = old;
			Current = current;
			Changed = !current.Equals(old);
		}

		public FakeMemoryWatcher(Func<T> updateFunc) : this()
		{
			OnUpdate = updateFunc;
		}

		public bool FakeUpdate(T newCurrent)
		{
			Old = Current;
			Current = newCurrent;
			return Changed = !Current.Equals(Old);
		}

		public override bool Update(System.Diagnostics.Process game)
		{
			if (OnUpdate == null)
				return false;

			Changed = false;

			if (!Enabled || !CheckInterval())
				return false;

			return FakeUpdate(OnUpdate());
		}
	}
}
