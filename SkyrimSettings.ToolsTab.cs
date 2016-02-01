using LiveSplit.AutoSplitting.Tools;
using LiveSplit.ComponentUtil;
using LiveSplit.Properties;
using LiveSplit.Skyrim.AutoSplitData;
using LiveSplit.Skyrim.AutoSplitData.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LiveSplit.Skyrim
{
	partial class SkyrimSettings
	{
		HashSet<string> _addrHiddenFromRamWatch;
		IList<ApplicationContext> _runningTools;

		void InitializeToolTab()
		{
			btnAddAddr.Text = btnRemoveAddr.Text = btnMoveDownAddr.Text = btnMoveUpAddr.Text = string.Empty;
			btnAddAddr.BackgroundImage = Resources.Add;
			btnRemoveAddr.BackgroundImage = Resources.Remove;
			btnMoveUpAddr.BackgroundImage = Resources.UpArrow;
			btnMoveDownAddr.BackgroundImage = Resources.DownArrow;
			foreach (var btn in tlpListBtn.Controls.OfType<Button>())
			{
				btn.FlatAppearance.MouseOverBackColor = Color.LightGray;
				btn.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
			}

			_runningTools = new List<ApplicationContext>();
			var data = new SkyrimData();
			_addrHiddenFromRamWatch = new HashSet<string>()
			{
				data.Location.Name,
			};

			var ramWatchAddresses = new BindingList<MemoryWatcher>()
			{
				data.WorldID,
				data.CellX,
				data.CellY
			};
			ramWatchAddresses.ListChanged += RamWatchAddresses_ListChanged;
			lstAddresses.DataSource = ramWatchAddresses;
			lstAddresses.DisplayMember = "Name";
			lstAddresses.SelectedIndex = -1;
		}

		void SkyrimSettings_ToolsTab_Dispose()
		{
			foreach (var app in _runningTools.ToList())
				app.ExitThread();
		}

		void btnLaunchRamWatch_Click(object sender, EventArgs e)
		{
			btnLaunchRamWatch.Enabled = false;
			LaunchTool<RamWatch>(form => form.Invoke((MethodInvoker)delegate ()
			{
				form.Disposed += (s, ee) => _uiThread.Send(d => btnLaunchRamWatch.Enabled = true, null);
				var data = new SkyrimData();
				form.CustomFormatting.Add(nameof(data.WorldID),
					value => (value == null) ? null : ((int)value).ToString("X8"));
				form.AddAddresses((IEnumerable<MemoryWatcher>)lstAddresses.DataSource);
				form.Attach(_component.AutoSplitManager);
			}));
		}

		void btnLaunchLoadScreenLog_Click(object sender, EventArgs e)
		{
			btnLaunchLoadScreenLog.Enabled = false;
			LaunchTool<LoadScreenLogForm>(form =>
			{
				form.Disposed += (s, ee) => _uiThread.Send(d => btnLaunchLoadScreenLog.Enabled = true, null);
				form.Attach(_component.AutoSplitManager);
			});
		}

		void btnAddAddr_Click(object sender, EventArgs e)
		{
			var cmsAddAddr = new ContextMenuStrip();
			cmsAddAddr.MaximumSize = new Size(0, 450);
			var list = (BindingList<MemoryWatcher>)lstAddresses.DataSource;

			var addresses = new SkyrimData()
				.Where(w => !_addrHiddenFromRamWatch.Contains(w.Name) && !list.Any(x => x.Name == w.Name))
				.OrderBy(x => x.Name);

			foreach (var watcher in addresses)
				cmsAddAddr.Items.Add(watcher.Name, null, (s, ee) => list.Add(watcher));

			cmsAddAddr.Show(btnAddAddr, new Point(0, 0));
		}

		void LaunchTool<T>(Action<T> initializedCallback, bool allowMultipleInstances = false) where T : Form
		{
			if (!allowMultipleInstances && _runningTools.Any(t => t.MainForm is T))
				return;

			var initEvent = new ManualResetEventSlim();
			var thread = new Thread(() =>
			{
				var context = new ApplicationContext();
				try
				{
					var form = Activator.CreateInstance<T>();
					form.Shown += (s, e) =>
					{
						initEvent.Set();
						initializedCallback?.Invoke(form);
					};
					context.MainForm = form;
					lock (((ICollection)_runningTools).SyncRoot)
						_runningTools.Add(context);
					Application.Run(context);
				}
				catch (Exception e)
				{
					initEvent.Set();
					_uiThread.Post(d => MessageBox.Show($"Error: {e.GetType().Name}\nMessage:\n{e.Message}", $"{typeof(T).Name} crashed",
						MessageBoxButtons.OK, MessageBoxIcon.Error), null);
				}
				finally
				{
					context.Dispose();
					lock (((ICollection)_runningTools).SyncRoot)
						_runningTools.Remove(context);
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.IsBackground = true;
			thread.Start();
			initEvent.Wait();
		}

		void RamWatchAddresses_ListChanged(object sender, ListChangedEventArgs e)
		{
			var ramWatch = (RamWatch)_runningTools.FirstOrDefault(c => c.MainForm is RamWatch)?.MainForm;
			if (ramWatch == null)
				return;

			var list = (BindingList<MemoryWatcher>)lstAddresses.DataSource;
			ramWatch.Invoke((MethodInvoker)delegate ()
			{
				ramWatch.Addresses.Clear();
				ramWatch.AddAddresses(list);
			});
		}

		void btnRemoveAddr_Click(object sender, EventArgs e)
		{
			if (lstAddresses.SelectedIndex == -1)
				return;
			var list = (BindingList<MemoryWatcher>)lstAddresses.DataSource;
			list.RemoveAt(lstAddresses.SelectedIndex);
		}

		void btnMoveUpAddr_Click(object sender, EventArgs e)
		{
			if (lstAddresses.SelectedIndex == -1)
				return;
			var newIndex = MoveItem((BindingList<MemoryWatcher>)lstAddresses.DataSource,
				lstAddresses.SelectedIndex,
				-1);
			lstAddresses.SelectedIndex = newIndex;
		}

		void btnMoveDownAddr_Click(object sender, EventArgs e)
		{
			if (lstAddresses.SelectedIndex == -1)
				return;
			var newIndex = MoveItem((BindingList<MemoryWatcher>)lstAddresses.DataSource,
				lstAddresses.SelectedIndex,
				1);
			lstAddresses.SelectedIndex = newIndex;
		}

		static int MoveItem<T>(IList<T> list, int index, int moves)
		{
			var newIndex = index + moves;
			if (newIndex < 0 || newIndex >= list.Count)
				return index;

			var item = list[index];
			list.RemoveAt(index);
			list.Insert(newIndex, item);

			return newIndex;
		}
	}
}