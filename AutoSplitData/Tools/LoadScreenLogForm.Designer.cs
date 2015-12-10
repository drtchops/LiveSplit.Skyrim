namespace LiveSplit.Skyrim.AutoSplitData.Tools
{
	partial class LoadScreenLogForm
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
			this.components = new System.ComponentModel.Container();
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.gbLoadScreenInfo = new System.Windows.Forms.GroupBox();
			this.tlpLoadScreenInfo = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.gbEndLocation = new System.Windows.Forms.GroupBox();
			this.gbLog = new System.Windows.Forms.GroupBox();
			this.tlpLog = new System.Windows.Forms.TableLayoutPanel();
			this.lbLoadScreens = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.linkClear = new System.Windows.Forms.LinkLabel();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.lCopyToClipboard = new System.Windows.Forms.LinkLabel();
			this.locationStart = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationControl();
			this.locationEnd = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationControl();
			this.tlpMain.SuspendLayout();
			this.gbLoadScreenInfo.SuspendLayout();
			this.tlpLoadScreenInfo.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.gbEndLocation.SuspendLayout();
			this.gbLog.SuspendLayout();
			this.tlpLog.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.ColumnCount = 1;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.gbLoadScreenInfo, 0, 1);
			this.tlpMain.Controls.Add(this.gbLog, 0, 0);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(304, 265);
			this.tlpMain.TabIndex = 0;
			// 
			// gbLoadScreenInfo
			// 
			this.gbLoadScreenInfo.AutoSize = true;
			this.gbLoadScreenInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbLoadScreenInfo.Controls.Add(this.tlpLoadScreenInfo);
			this.gbLoadScreenInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbLoadScreenInfo.Enabled = false;
			this.gbLoadScreenInfo.Location = new System.Drawing.Point(3, 116);
			this.gbLoadScreenInfo.Name = "gbLoadScreenInfo";
			this.gbLoadScreenInfo.Size = new System.Drawing.Size(298, 146);
			this.gbLoadScreenInfo.TabIndex = 0;
			this.gbLoadScreenInfo.TabStop = false;
			this.gbLoadScreenInfo.Text = "LoadScreen Info";
			// 
			// tlpLoadScreenInfo
			// 
			this.tlpLoadScreenInfo.AutoSize = true;
			this.tlpLoadScreenInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpLoadScreenInfo.ColumnCount = 2;
			this.tlpLoadScreenInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpLoadScreenInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpLoadScreenInfo.Controls.Add(this.groupBox2, 0, 0);
			this.tlpLoadScreenInfo.Controls.Add(this.gbEndLocation, 1, 0);
			this.tlpLoadScreenInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpLoadScreenInfo.Location = new System.Drawing.Point(3, 16);
			this.tlpLoadScreenInfo.Name = "tlpLoadScreenInfo";
			this.tlpLoadScreenInfo.RowCount = 1;
			this.tlpLoadScreenInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpLoadScreenInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 127F));
			this.tlpLoadScreenInfo.Size = new System.Drawing.Size(292, 127);
			this.tlpLoadScreenInfo.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.locationStart);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(140, 121);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Start Location";
			// 
			// gbEndLocation
			// 
			this.gbEndLocation.Controls.Add(this.locationEnd);
			this.gbEndLocation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbEndLocation.Location = new System.Drawing.Point(149, 3);
			this.gbEndLocation.Name = "gbEndLocation";
			this.gbEndLocation.Size = new System.Drawing.Size(140, 121);
			this.gbEndLocation.TabIndex = 0;
			this.gbEndLocation.TabStop = false;
			this.gbEndLocation.Text = "End Location";
			// 
			// gbLog
			// 
			this.gbLog.AutoSize = true;
			this.gbLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbLog.Controls.Add(this.tlpLog);
			this.gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbLog.Location = new System.Drawing.Point(3, 3);
			this.gbLog.Name = "gbLog";
			this.gbLog.Size = new System.Drawing.Size(298, 107);
			this.gbLog.TabIndex = 1;
			this.gbLog.TabStop = false;
			this.gbLog.Text = "Log";
			// 
			// tlpLog
			// 
			this.tlpLog.AutoSize = true;
			this.tlpLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpLog.ColumnCount = 1;
			this.tlpLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpLog.Controls.Add(this.lbLoadScreens, 0, 1);
			this.tlpLog.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tlpLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpLog.Location = new System.Drawing.Point(3, 16);
			this.tlpLog.Name = "tlpLog";
			this.tlpLog.RowCount = 2;
			this.tlpLog.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpLog.Size = new System.Drawing.Size(292, 88);
			this.tlpLog.TabIndex = 2;
			// 
			// lbLoadScreens
			// 
			this.lbLoadScreens.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLoadScreens.FormattingEnabled = true;
			this.lbLoadScreens.IntegralHeight = false;
			this.lbLoadScreens.Location = new System.Drawing.Point(3, 16);
			this.lbLoadScreens.Name = "lbLoadScreens";
			this.lbLoadScreens.Size = new System.Drawing.Size(286, 69);
			this.lbLoadScreens.TabIndex = 1;
			this.lbLoadScreens.SelectedIndexChanged += new System.EventHandler(this.lbLoadScreens_SelectedIndexChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.linkClear, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lCopyToClipboard, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 13);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// linkClear
			// 
			this.linkClear.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.linkClear.AutoSize = true;
			this.linkClear.Location = new System.Drawing.Point(258, 0);
			this.linkClear.Name = "linkClear";
			this.linkClear.Size = new System.Drawing.Size(31, 13);
			this.linkClear.TabIndex = 2;
			this.linkClear.TabStop = true;
			this.linkClear.Text = "Clear";
			this.linkClear.Click += new System.EventHandler(this.linkClear_Click);
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 6000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// lCopyToClipboard
			// 
			this.lCopyToClipboard.AutoSize = true;
			this.lCopyToClipboard.Location = new System.Drawing.Point(3, 0);
			this.lCopyToClipboard.Name = "lCopyToClipboard";
			this.lCopyToClipboard.Size = new System.Drawing.Size(89, 13);
			this.lCopyToClipboard.TabIndex = 3;
			this.lCopyToClipboard.TabStop = true;
			this.lCopyToClipboard.Text = "Copy to clipboard";
			this.lCopyToClipboard.Visible = false;
			this.lCopyToClipboard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lCopyToClipboard_LinkClicked);
			// 
			// locationStart
			// 
			this.locationStart.AutoSize = true;
			this.locationStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.locationStart.CellX = null;
			this.locationStart.CellY = null;
			this.locationStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationStart.Location = new System.Drawing.Point(3, 16);
			this.locationStart.Name = "locationStart";
			this.locationStart.ReadOnly = true;
			this.locationStart.Size = new System.Drawing.Size(134, 102);
			this.locationStart.TabIndex = 0;
			this.locationStart.WorldID = null;
			// 
			// locationEnd
			// 
			this.locationEnd.AutoSize = true;
			this.locationEnd.CellX = null;
			this.locationEnd.CellY = null;
			this.locationEnd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationEnd.Location = new System.Drawing.Point(3, 16);
			this.locationEnd.Name = "locationEnd";
			this.locationEnd.ReadOnly = true;
			this.locationEnd.Size = new System.Drawing.Size(134, 102);
			this.locationEnd.TabIndex = 0;
			this.locationEnd.WorldID = null;
			// 
			// LoadScreenLogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 265);
			this.Controls.Add(this.tlpMain);
			this.Name = "LoadScreenLogForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LoadScreen Log ";
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.gbLoadScreenInfo.ResumeLayout(false);
			this.gbLoadScreenInfo.PerformLayout();
			this.tlpLoadScreenInfo.ResumeLayout(false);
			this.tlpLoadScreenInfo.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.gbEndLocation.ResumeLayout(false);
			this.gbEndLocation.PerformLayout();
			this.gbLog.ResumeLayout(false);
			this.gbLog.PerformLayout();
			this.tlpLog.ResumeLayout(false);
			this.tlpLog.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.GroupBox gbLoadScreenInfo;
		private System.Windows.Forms.TableLayoutPanel tlpLoadScreenInfo;
		private System.Windows.Forms.GroupBox gbLog;
		private System.Windows.Forms.ListBox lbLoadScreens;
		private System.Windows.Forms.GroupBox groupBox2;
		private Editors.LocationControl locationStart;
		private System.Windows.Forms.GroupBox gbEndLocation;
		private Editors.LocationControl locationEnd;
		private System.Windows.Forms.TableLayoutPanel tlpLog;
		private System.Windows.Forms.LinkLabel linkClear;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.LinkLabel lCopyToClipboard;
	}
}