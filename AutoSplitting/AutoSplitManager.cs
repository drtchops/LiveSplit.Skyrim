using System;

namespace LiveSplit.AutoSplitting
{
	public class AutoSplitManager
	{
		public event EventHandler<SplitEventArgs> SplitTriggered;
		public event EventHandler<AutoSplitManagerUpdateEventArgs> Updated;

		public AutoSplitList AutoSplitList { get; set; }

		public AutoSplitManager()
		{
			AutoSplitList = new AutoSplitList();
		}

		public void Update(GameData data, GameEvent evnt)
		{
			foreach (var split in AutoSplitList[evnt])
			{
				if (split.Enabled && !split.Triggered && split.Verify(data))
				{
					split.Triggered = true;
					SplitTriggered?.Invoke(this, new SplitEventArgs(split));
				}
			}
			Updated?.Invoke(this, new AutoSplitManagerUpdateEventArgs(data, evnt));
		}

		public void Reset()
		{
			foreach (var split in AutoSplitList)
				split.Triggered = false;
		}
	}

	public class SplitEventArgs : EventArgs
	{
		public AutoSplit AutoSplit { get; }

		public SplitEventArgs(AutoSplit split)
		{
			AutoSplit = split;
		}
	}

	public class AutoSplitManagerUpdateEventArgs : EventArgs
	{
		public GameData Data { get; }
		public GameEvent Event { get; }

		public AutoSplitManagerUpdateEventArgs(GameData data, GameEvent evnt)
		{
			Data = data;
			Event = evnt;
		}
	}
}
