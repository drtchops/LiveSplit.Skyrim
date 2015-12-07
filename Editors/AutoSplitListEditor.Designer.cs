using System;
using LiveSplit.AutoSplitting;
using LiveSplit.Skyrim.AutoSplitData;

namespace LiveSplit.AutoSplitting.Editors
{
	partial class AutoSplitListEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvList = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Variables = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.tlpNewSplit = new System.Windows.Forms.TableLayoutPanel();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnAddFromPreset = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOK = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
			this.tlpMain.SuspendLayout();
			this.tlpNewSplit.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvList
			// 
			this.dgvList.AllowUserToResizeColumns = false;
			this.dgvList.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Variables});
			this.dgvList.Dock = System.Windows.Forms.DockStyle.Top;
			this.dgvList.Location = new System.Drawing.Point(3, 3);
			this.dgvList.MultiSelect = false;
			this.dgvList.Name = "dgvList";
			this.dgvList.RowHeadersVisible = false;
			this.dgvList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvList.Size = new System.Drawing.Size(437, 320);
			this.dgvList.TabIndex = 1;
			this.dgvList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseDoubleClick);
			this.dgvList.SelectionChanged += new System.EventHandler(this.dgvList_SelectionChanged);
			// 
			// Column1
			// 
			this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Column1.DataPropertyName = "Enabled";
			this.Column1.FalseValue = "false";
			this.Column1.HeaderText = "";
			this.Column1.MinimumWidth = 10;
			this.Column1.Name = "Column1";
			this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Column1.TrueValue = "true";
			this.Column1.Width = 22;
			// 
			// Column2
			// 
			this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.DataPropertyName = "Name";
			this.Column2.HeaderText = "Name";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// Variables
			// 
			this.Variables.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Variables.HeaderText = "Conditions";
			this.Variables.Name = "Variables";
			this.Variables.ReadOnly = true;
			this.Variables.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Variables.Width = 81;
			// 
			// tlpMain
			// 
			this.tlpMain.ColumnCount = 2;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.Controls.Add(this.dgvList, 0, 0);
			this.tlpMain.Controls.Add(this.tlpNewSplit, 1, 0);
			this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(584, 361);
			this.tlpMain.TabIndex = 1;
			// 
			// tlpNewSplit
			// 
			this.tlpNewSplit.AutoSize = true;
			this.tlpNewSplit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpNewSplit.ColumnCount = 1;
			this.tlpNewSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpNewSplit.Controls.Add(this.btnCreate, 0, 0);
			this.tlpNewSplit.Controls.Add(this.btnAddFromPreset, 0, 1);
			this.tlpNewSplit.Controls.Add(this.btnExport, 0, 10);
			this.tlpNewSplit.Controls.Add(this.btnImport, 0, 11);
			this.tlpNewSplit.Controls.Add(this.btnClear, 0, 8);
			this.tlpNewSplit.Controls.Add(this.btnMoveUp, 0, 4);
			this.tlpNewSplit.Controls.Add(this.btnMoveDown, 0, 5);
			this.tlpNewSplit.Controls.Add(this.btnEdit, 0, 3);
			this.tlpNewSplit.Controls.Add(this.btnRemove, 0, 6);
			this.tlpNewSplit.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpNewSplit.Location = new System.Drawing.Point(446, 3);
			this.tlpNewSplit.Name = "tlpNewSplit";
			this.tlpNewSplit.RowCount = 12;
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNewSplit.Size = new System.Drawing.Size(135, 320);
			this.tlpNewSplit.TabIndex = 0;
			// 
			// btnCreate
			// 
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreate.Location = new System.Drawing.Point(3, 3);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(129, 23);
			this.btnCreate.TabIndex = 0;
			this.btnCreate.Text = "New autosplit...";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnAddFromPreset
			// 
			this.btnAddFromPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFromPreset.Location = new System.Drawing.Point(3, 32);
			this.btnAddFromPreset.Name = "btnAddFromPreset";
			this.btnAddFromPreset.Size = new System.Drawing.Size(129, 23);
			this.btnAddFromPreset.TabIndex = 1;
			this.btnAddFromPreset.Text = "Copy from a preset";
			this.btnAddFromPreset.UseVisualStyleBackColor = true;
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExport.Location = new System.Drawing.Point(3, 266);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(129, 23);
			this.btnExport.TabIndex = 7;
			this.btnExport.Text = "Export...";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImport.Location = new System.Drawing.Point(3, 295);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(129, 23);
			this.btnImport.TabIndex = 8;
			this.btnImport.Text = "Import...";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Location = new System.Drawing.Point(3, 217);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(129, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUp.Enabled = false;
			this.btnMoveUp.Location = new System.Drawing.Point(3, 110);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(129, 23);
			this.btnMoveUp.TabIndex = 3;
			this.btnMoveUp.Text = "Move up";
			this.btnMoveUp.UseVisualStyleBackColor = true;
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDown.Enabled = false;
			this.btnMoveDown.Location = new System.Drawing.Point(3, 139);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(129, 23);
			this.btnMoveDown.TabIndex = 4;
			this.btnMoveDown.Text = "Move down";
			this.btnMoveDown.UseVisualStyleBackColor = true;
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Enabled = false;
			this.btnEdit.Location = new System.Drawing.Point(3, 81);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(129, 23);
			this.btnEdit.TabIndex = 2;
			this.btnEdit.Text = "Edit...";
			this.btnEdit.UseVisualStyleBackColor = true;
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Enabled = false;
			this.btnRemove.Location = new System.Drawing.Point(3, 168);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(129, 23);
			this.btnRemove.TabIndex = 5;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tlpMain.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.btnOK, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 329);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 29);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(211, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(292, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// AutoSplitListEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.ControlBox = false;
			this.Controls.Add(this.tlpMain);
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "AutoSplitListEditor";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AutoSplitList Editor";
			((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.tlpNewSplit.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		internal static void ShowEditor(SkyrimData skyrimData, GameEvent[] gameEvent, object autoSplitList)
		{
			throw new NotImplementedException();
		}

		#endregion

		private System.Windows.Forms.DataGridView dgvList;
		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.TableLayoutPanel tlpNewSplit;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnAddFromPreset;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Variables;
	}
}