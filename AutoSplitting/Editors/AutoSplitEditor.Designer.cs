namespace LiveSplit.AutoSplitting.Editors
{
	partial class AutoSplitEditor
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
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbEvent = new System.Windows.Forms.ComboBox();
			this.flpConfirmButtons = new System.Windows.Forms.FlowLayoutPanel();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbVariables = new System.Windows.Forms.GroupBox();
			this.tlpConditions = new System.Windows.Forms.TableLayoutPanel();
			this.tlpListBtn = new System.Windows.Forms.TableLayoutPanel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.lstVariables = new System.Windows.Forms.ListBox();
			this.tlpMain.SuspendLayout();
			this.flpConfirmButtons.SuspendLayout();
			this.gbVariables.SuspendLayout();
			this.tlpConditions.SuspendLayout();
			this.tlpListBtn.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.ColumnCount = 2;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.label1, 0, 0);
			this.tlpMain.Controls.Add(this.txtName, 1, 0);
			this.tlpMain.Controls.Add(this.label2, 0, 1);
			this.tlpMain.Controls.Add(this.cbEvent, 1, 1);
			this.tlpMain.Controls.Add(this.flpConfirmButtons, 0, 3);
			this.tlpMain.Controls.Add(this.gbVariables, 0, 2);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.tlpMain.RowCount = 4;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(334, 206);
			this.tlpMain.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(50, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(278, 20);
			this.txtName.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Event:";
			this.label2.Visible = false;
			// 
			// cbEvent
			// 
			this.cbEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbEvent.Enabled = false;
			this.cbEvent.FormattingEnabled = true;
			this.cbEvent.Location = new System.Drawing.Point(50, 29);
			this.cbEvent.Name = "cbEvent";
			this.cbEvent.Size = new System.Drawing.Size(278, 21);
			this.cbEvent.TabIndex = 1;
			this.cbEvent.TabStop = false;
			this.cbEvent.Visible = false;
			// 
			// flpConfirmButtons
			// 
			this.flpConfirmButtons.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.flpConfirmButtons.AutoSize = true;
			this.flpConfirmButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.SetColumnSpan(this.flpConfirmButtons, 2);
			this.flpConfirmButtons.Controls.Add(this.btnOK);
			this.flpConfirmButtons.Controls.Add(this.btnCancel);
			this.flpConfirmButtons.Location = new System.Drawing.Point(86, 174);
			this.flpConfirmButtons.Name = "flpConfirmButtons";
			this.flpConfirmButtons.Size = new System.Drawing.Size(162, 29);
			this.flpConfirmButtons.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(3, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(84, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// gbVariables
			// 
			this.tlpMain.SetColumnSpan(this.gbVariables, 2);
			this.gbVariables.Controls.Add(this.tlpConditions);
			this.gbVariables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbVariables.Location = new System.Drawing.Point(6, 56);
			this.gbVariables.Name = "gbVariables";
			this.gbVariables.Size = new System.Drawing.Size(322, 112);
			this.gbVariables.TabIndex = 2;
			this.gbVariables.TabStop = false;
			this.gbVariables.Text = "Conditions";
			// 
			// tlpConditions
			// 
			this.tlpConditions.ColumnCount = 2;
			this.tlpConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpConditions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpConditions.Controls.Add(this.tlpListBtn, 1, 0);
			this.tlpConditions.Controls.Add(this.lstVariables, 0, 0);
			this.tlpConditions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpConditions.Location = new System.Drawing.Point(3, 16);
			this.tlpConditions.Name = "tlpConditions";
			this.tlpConditions.RowCount = 1;
			this.tlpConditions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpConditions.Size = new System.Drawing.Size(316, 93);
			this.tlpConditions.TabIndex = 0;
			// 
			// tlpListBtn
			// 
			this.tlpListBtn.AutoScroll = true;
			this.tlpListBtn.AutoSize = true;
			this.tlpListBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpListBtn.ColumnCount = 1;
			this.tlpListBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpListBtn.Controls.Add(this.btnAdd, 0, 0);
			this.tlpListBtn.Controls.Add(this.btnRemove, 0, 1);
			this.tlpListBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpListBtn.Location = new System.Drawing.Point(282, 0);
			this.tlpListBtn.Margin = new System.Windows.Forms.Padding(0);
			this.tlpListBtn.Name = "tlpListBtn";
			this.tlpListBtn.RowCount = 2;
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpListBtn.Size = new System.Drawing.Size(34, 62);
			this.tlpListBtn.TabIndex = 1;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnAdd.FlatAppearance.BorderSize = 0;
			this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAdd.Location = new System.Drawing.Point(3, 0);
			this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(28, 28);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnRemove.FlatAppearance.BorderSize = 0;
			this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemove.Location = new System.Drawing.Point(3, 31);
			this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(28, 28);
			this.btnRemove.TabIndex = 1;
			this.btnRemove.Text = "-";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// lstVariables
			// 
			this.lstVariables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstVariables.FormattingEnabled = true;
			this.lstVariables.IntegralHeight = false;
			this.lstVariables.Location = new System.Drawing.Point(3, 3);
			this.lstVariables.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.lstVariables.Name = "lstVariables";
			this.lstVariables.Size = new System.Drawing.Size(279, 87);
			this.lstVariables.TabIndex = 0;
			this.lstVariables.DoubleClick += new System.EventHandler(this.lstVariables_DoubleClick);
			// 
			// AutoSplitEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(334, 206);
			this.ControlBox = false;
			this.Controls.Add(this.tlpMain);
			this.MinimumSize = new System.Drawing.Size(350, 222);
			this.Name = "AutoSplitEditor";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AutoSplit Editor";
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.flpConfirmButtons.ResumeLayout(false);
			this.gbVariables.ResumeLayout(false);
			this.tlpConditions.ResumeLayout(false);
			this.tlpConditions.PerformLayout();
			this.tlpListBtn.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbEvent;
		private System.Windows.Forms.FlowLayoutPanel flpConfirmButtons;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gbVariables;
		private System.Windows.Forms.TableLayoutPanel tlpConditions;
		private System.Windows.Forms.TableLayoutPanel tlpListBtn;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.ListBox lstVariables;
	}
}