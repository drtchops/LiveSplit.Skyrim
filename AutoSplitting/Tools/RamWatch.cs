using LiveSplit.ComponentUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LiveSplit.AutoSplitting.Tools
{
	public partial class RamWatch : Form
	{
		public BindingList<KeyValuePair<string, object>> Addresses;
		public Dictionary<object, Func<object, string>> CustomFormatting { get; private set; }

		AutoSplitManager _manager;
		SynchronizationContext _uiThread;

		public RamWatch()
		{
			InitializeComponent();

			MaximumSize = new Size(Screen.AllScreens.Sum(s => s.WorkingArea.Width), Screen.AllScreens.Max(s => s.WorkingArea.Height));
			MinimumSize = new Size(Size.Width, Size.Height);
			Disposed += RamWatch_Disposed;

			_uiThread = SynchronizationContext.Current;
			CustomFormatting = new Dictionary<object, Func<object, string>>();
			Addresses = new BindingList<KeyValuePair<string, object>>();

			dataGridView.Columns[0].DataPropertyName = "Key";
			dataGridView.Columns[1].DataPropertyName = "Value";
			dataGridView.DataSource = Addresses;
        }

		public void Attach(AutoSplitManager manager)
		{
			Detach();
			_manager = manager;
			_manager.Updated += Manager_Updated;
		}

		public void Detach()
		{
			if (_manager != null)
			{
				_manager.Updated -= Manager_Updated;
				_manager = null;
			}
		}

		void Manager_Updated(object sender, AutoSplitManagerUpdateEventArgs e)
		{
			if (e.Event != GameEvent.Always)
				return;

			_uiThread.Post(d => UpdateAllAddresses(e.Data), null);
		}

		public void AddAddresses(IEnumerable<MemoryWatcher> watchers)
		{
			foreach (var w in watchers)
				Addresses.Add(new KeyValuePair<string, object>(w.Name, w.Current));
		}

		public void UpdateAddress(MemoryWatcher watcher)
		{
			var pair = Addresses.FirstOrDefault(p => p.Key == watcher.Name);
			if (pair.Key != null)
			{
				var index = Addresses.IndexOf(pair);
				Addresses[index] = new KeyValuePair<string, object>(watcher.Name, watcher.Current);
			}
		}

		public void UpdateAllAddresses(GameData data)
		{
			if (Disposing || IsDisposed)
				return;

			foreach (var watcher in data)
				UpdateAddress(watcher);
		}

		void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				var name = Addresses[e.RowIndex].Key;
				if (CustomFormatting.ContainsKey(name))
				{
					e.Value = CustomFormatting[name].Invoke(e.Value);
					e.FormattingApplied = e.Value != null;
				}
			}
		}

		void RamWatch_Disposed(object sender, EventArgs e)
		{
			Detach();
		}
	}
}
