using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.AutoSplitting.Editors
{
	public partial class AutoSplitListEditor : Form
	{
		public AutoSplitList EditedList { get; private set; }

		readonly AutoSplitEnv _env;
		ContextMenuStrip _cmsSplitRow;
		ContextMenuStrip _cmsAddFromPreset;

		public AutoSplitListEditor(AutoSplitEnv env, AutoSplitList list = null)
		{
			InitializeComponent();

			FormClosing += (s, e) =>
			{
				if (e.CloseReason != CloseReason.None)
					DialogResult = DialogResult.OK;
			};

			_env = env;
			EditedList = list?.Clone(env) ?? new AutoSplitList();

			dgvList.AutoGenerateColumns = false;
			dgvList.DataBindingComplete += DgvList_DataBindingComplete;
			dgvList.CellMouseDown += DgvList_CellMouseDown;
			dgvList.DataSource = new BindingList<AutoSplit>(EditedList);

			_cmsSplitRow = new ContextMenuStrip();
			_cmsSplitRow.Items.Add("Edit...", null, (s, e) => EditSelectedRow());
			_cmsSplitRow.Items.Add("-");
			_cmsSplitRow.Items.Add("Move up", null, (s, e) => DgvList_SelectRow(DgvList_MoveRow(dgvList.SelectedRows[0], -1)));
			_cmsSplitRow.Items.Add("Move down", null, (s, e) => DgvList_SelectRow(DgvList_MoveRow(dgvList.SelectedRows[0], 1)));
			_cmsSplitRow.Items.Add("-");
			_cmsSplitRow.Items.Add("Remove", null, (s, e) => DgvList_RemoveSelectedRow());

			btnEdit.Click += (s, e) => EditSelectedRow();
			btnMoveUp.Click += (s, e) => DgvList_SelectRow(DgvList_MoveRow(dgvList.SelectedRows[0], -1));
			btnMoveDown.Click += (s, e) => DgvList_SelectRow(DgvList_MoveRow(dgvList.SelectedRows[0], 1));
			btnRemove.Click += (s, e) => DgvList_RemoveSelectedRow();

			_cmsAddFromPreset = new ContextMenuStrip();
			PopulateAddFromPresetCM(_env.Presets);
			btnAddFromPreset.Click += BtnAddFromPreset_Click;
		}

		void PopulateAddFromPresetCM(IEnumerable<AutoSplitList> presets)
		{
			_cmsAddFromPreset.Items.Clear();

			foreach (var preset in presets)
			{
				var presetMenu = new ToolStripMenuItem(preset.Name);
				presetMenu.DropDownItems.Add("Add all", null, (s, e) =>
				{
					var bindingList = (BindingList<AutoSplit>)dgvList.DataSource;
					var result = bindingList.Count > 0
						? MessageBox.Show("Are you sure you want to add all autosplits from this preset to your list?", $"Confirm copy from \"{preset.Name}\" preset",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question)
						: DialogResult.Yes;
					if (result == DialogResult.Yes)
					{
						foreach (var split in preset)
							bindingList.Add(split.Clone(_env));
						dgvList.FirstDisplayedScrollingRowIndex = bindingList.Count - 1;
					}
				});
				presetMenu.DropDownItems.Add("-");

				foreach (var split in preset)
				{
					presetMenu.DropDownItems.Add(split.Name, null, (s, e) =>
					{
						var bindingList = (BindingList<AutoSplit>)dgvList.DataSource;
						bindingList.Add(split.Clone(_env));
						dgvList.FirstDisplayedScrollingRowIndex = bindingList.Count - 1;
					});
				}
				_cmsAddFromPreset.Items.Add(presetMenu);
			}
		}

		void EditSelectedRow()
		{
			var selectedRow = dgvList.SelectedRows[0];
			var selectedSplit = (AutoSplit)selectedRow.DataBoundItem;
			EditedList[selectedRow.Index] = AutoSplitEditor.ShowEditor(_env, selectedSplit);
			DgvList_ResetBindings(selectedRow.Index);
		}

		void BtnAddFromPreset_Click(object sender, EventArgs e)
		{
			_cmsAddFromPreset.Show(btnAddFromPreset, new Point(0, 0));
		}

		int DgvList_MoveRow(DataGridViewRow row, int rows)
		{
			var selectedSplit = (AutoSplit)row.DataBoundItem;
			var splitList = (BindingList<AutoSplit>)dgvList.DataSource;
			var rowIndex = row.Index;
			var newIndex = rowIndex + rows;

			if (newIndex < 0 || newIndex >= splitList.Count)
				return -1;

			int scrollPosition = dgvList.FirstDisplayedScrollingRowIndex;

			splitList.RemoveAt(rowIndex);
			splitList.Insert(newIndex, selectedSplit);

			dgvList.FirstDisplayedScrollingRowIndex = scrollPosition;

			if (!DgvList_IsRowVisible(newIndex))
				dgvList.FirstDisplayedScrollingRowIndex = newIndex;

			return newIndex;
		}

		bool DgvList_IsRowVisible(int index)
		{
			var firstVisibleRowIndex = dgvList.FirstDisplayedScrollingRowIndex;
            var lastVisibleRowIndex = firstVisibleRowIndex + dgvList.DisplayedRowCount(true) - 1;

			return index >= firstVisibleRowIndex && index <= lastVisibleRowIndex;
        }

		void DgvList_RemoveSelectedRow()
		{
			if (dgvList.SelectedRows.Count == 0)
				return;

			int scrollPosition = dgvList.FirstDisplayedScrollingRowIndex;

			var splitsList = (BindingList<AutoSplit>)dgvList.DataSource;
			splitsList.RemoveAt(dgvList.SelectedRows[0].Index);

			if (splitsList.Count > 0)
				dgvList.FirstDisplayedScrollingRowIndex = scrollPosition;
		}

		void DgvList_SelectRow(int index)
		{
			if (index < 0 || index >= dgvList.Rows.Count)
				return;
			dgvList.Rows[index].Selected = true;
		}

		void DgvList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			//fix checkbox staying in edit mode until second click
			if (dgvList.IsCurrentCellInEditMode && dgvList.CurrentCellAddress.X == 0)
				dgvList.CurrentCell = null;

			if (e.Button != MouseButtons.Right || e.RowIndex == -1 || dgvList.IsCurrentCellInEditMode)
				return;

			dgvList.Rows[e.RowIndex].Selected = true;
			_cmsSplitRow.Show(Cursor.Position);
		}

		void DgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			var splits = (BindingList<AutoSplit>)dgvList.DataSource;
			var varsCol = dgvList.Columns["Variables"];
			foreach (DataGridViewRow row in dgvList.Rows)
				row.Cells[varsCol.Index].Value = splits[row.Index].Variables.Count;

			var height = dgvList.ColumnHeadersHeight;
			foreach (DataGridViewRow row in dgvList.Rows)
				height += row.Height;
			dgvList.Height = height;
		}

		void DgvList_ResetBindings(int itemIndex = -1)
		{
			var scrollPosition = dgvList.FirstDisplayedScrollingRowIndex;

			var bindingList = (BindingList<AutoSplit>)dgvList.DataSource;
			if (itemIndex < 0)
				bindingList.ResetBindings();
			else
				bindingList.ResetItem(itemIndex);

			if (scrollPosition >= 0)
				dgvList.FirstDisplayedScrollingRowIndex = scrollPosition;
		}

		public static AutoSplitList ShowEditor(AutoSplitEnv env, AutoSplitList list = null)
		{
			using (var form = new AutoSplitListEditor(env, list))
			{
				if (form.ShowDialog() != DialogResult.Cancel)
				{
					if (list == null)
						return form.EditedList;
					list.Clear();
					list.AddRange(form.EditedList);
				}

				return list;
			}
		}

		void btnCreate_Click(object sender, EventArgs e)
		{
			var newSplit = AutoSplitEditor.ShowEditor(_env);
			if (newSplit != null)
			{
				var list = (BindingList<AutoSplit>)dgvList.DataSource;
				list.Add(newSplit);
			}
		}

		void btnExport_Click(object sender, EventArgs e)
		{
			var path = ShowSaveFileDialog();
			if (string.IsNullOrEmpty(path))
				return;

			var doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8",  null));
			var root = doc.AppendChild(doc.CreateElement("AutoSplitLists"));
			root.AppendChild(EditedList.ToXml(doc));
			doc.Save(path);
		}

		static string ShowSaveFileDialog(string path = "AutosplitList")
		{
			using (var fileDialog = new SaveFileDialog()
			{
				CheckPathExists = true,
				AddExtension = true,
				FileName = path,
				Filter = "Xml Files|*.xml;|All Files (*.*)|*.*"
			})
			{
				var result = fileDialog.ShowDialog();
				if (result == DialogResult.OK)
					path = fileDialog.FileName;
				return path;
			}
		}

		void btnImport_Click(object sender, EventArgs e)
		{
			var path = ShowOpenFileDialog();
			if (string.IsNullOrEmpty(path))
				return;

			var doc = new XmlDocument();
			doc.Load(path);
			var root = doc.DocumentElement;
			XmlElement splitsListElem = null;

			if (System.IO.Path.GetExtension(path).ToLower() == ".lss")
			{
				splitsListElem = root["AutoSplitterSettings"]?["AutoSplitList"];
			}
			else
			{
				if (root.HasChildNodes)
					splitsListElem = (XmlElement)root.FirstChild;
			}

			if (splitsListElem == null || !AutoSplitList.IsValidXml(splitsListElem))
			{
				MessageBox.Show("Invalid autosplit list file.", "Import failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			AutoSplitList importedList = null;
			try
			{
				importedList = AutoSplitList.FromXml(splitsListElem, _env);
			}
			catch { MessageBox.Show("Import error.", "Import failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }

			if (importedList != null)
			{
				var dialogResult = DialogResult.None;

				if (EditedList.Count > 0)
					dialogResult = MessageBox.Show("Keep current list and append imported autosplits?\nChoose No to replace the list.", "Import autosplits", MessageBoxButtons.YesNoCancel);

				if (dialogResult != DialogResult.Cancel)
				{
					if (dialogResult == DialogResult.No)
						EditedList.Clear();
					EditedList.AddRange(importedList);
					DgvList_ResetBindings();
				}
			}
		}

		static string ShowOpenFileDialog(string path = null)
		{
			using (var fileDialog = new OpenFileDialog()
			{
				CheckFileExists = true,
				FileName = path ?? "",
				Filter = "Splits Files|*.xml;*.lss|All Files (*.*)|*.*"
			})
			{
				var result = fileDialog.ShowDialog();
				if (result == DialogResult.OK)
					path = fileDialog.FileName;
				return path;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to remove all autosplits from your list?", "Confirm Clear",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				var list = (BindingList<AutoSplit>)dgvList.DataSource;
				list.Clear();
			}
		}

		void dgvList_SelectionChanged(object sender, EventArgs e)
		{
			btnEdit.Enabled = btnRemove.Enabled = btnMoveDown.Enabled = btnMoveUp.Enabled = dgvList.SelectedRows.Count > 0;
		}

		void dgvList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (dgvList.SelectedRows.Count == 0 || dgvList.IsCurrentCellInEditMode)
				return;

			EditSelectedRow();
		}
	}
}
