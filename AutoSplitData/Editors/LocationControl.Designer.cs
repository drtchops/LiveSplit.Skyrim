namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	partial class LocationControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.gbCellY = new System.Windows.Forms.GroupBox();
			this.tlpCellY = new System.Windows.Forms.TableLayoutPanel();
			this.numCellY = new System.Windows.Forms.NumericUpDown();
			this.chkCellYAny = new System.Windows.Forms.CheckBox();
			this.gbCellX = new System.Windows.Forms.GroupBox();
			this.tlpCellX = new System.Windows.Forms.TableLayoutPanel();
			this.numCellX = new System.Windows.Forms.NumericUpDown();
			this.chkCellXAny = new System.Windows.Forms.CheckBox();
			this.gbWorld = new System.Windows.Forms.GroupBox();
			this.tlpWorld = new System.Windows.Forms.TableLayoutPanel();
			this.numWorldID = new System.Windows.Forms.NumericUpDown();
			this.chkWorldAny = new System.Windows.Forms.CheckBox();
			this.btnWorldIDList = new System.Windows.Forms.Button();
			this.tlpMain.SuspendLayout();
			this.gbCellY.SuspendLayout();
			this.tlpCellY.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCellY)).BeginInit();
			this.gbCellX.SuspendLayout();
			this.tlpCellX.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCellX)).BeginInit();
			this.gbWorld.SuspendLayout();
			this.tlpWorld.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numWorldID)).BeginInit();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.ColumnCount = 2;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpMain.Controls.Add(this.gbCellY, 1, 1);
			this.tlpMain.Controls.Add(this.gbCellX, 0, 1);
			this.tlpMain.Controls.Add(this.gbWorld, 0, 0);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(234, 105);
			this.tlpMain.TabIndex = 3;
			// 
			// gbCellY
			// 
			this.gbCellY.AutoSize = true;
			this.gbCellY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbCellY.Controls.Add(this.tlpCellY);
			this.gbCellY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbCellY.Location = new System.Drawing.Point(120, 57);
			this.gbCellY.Name = "gbCellY";
			this.gbCellY.Size = new System.Drawing.Size(111, 45);
			this.gbCellY.TabIndex = 2;
			this.gbCellY.TabStop = false;
			this.gbCellY.Text = "Cell Y";
			// 
			// tlpCellY
			// 
			this.tlpCellY.AutoSize = true;
			this.tlpCellY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpCellY.ColumnCount = 2;
			this.tlpCellY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCellY.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCellY.Controls.Add(this.numCellY, 0, 0);
			this.tlpCellY.Controls.Add(this.chkCellYAny, 1, 0);
			this.tlpCellY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpCellY.Location = new System.Drawing.Point(3, 16);
			this.tlpCellY.Name = "tlpCellY";
			this.tlpCellY.RowCount = 1;
			this.tlpCellY.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpCellY.Size = new System.Drawing.Size(105, 26);
			this.tlpCellY.TabIndex = 0;
			// 
			// numCellY
			// 
			this.numCellY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numCellY.Enabled = false;
			this.numCellY.Location = new System.Drawing.Point(3, 3);
			this.numCellY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.numCellY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
			this.numCellY.Name = "numCellY";
			this.numCellY.Size = new System.Drawing.Size(49, 20);
			this.numCellY.TabIndex = 0;
			// 
			// chkCellYAny
			// 
			this.chkCellYAny.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkCellYAny.AutoSize = true;
			this.chkCellYAny.Checked = true;
			this.chkCellYAny.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCellYAny.Location = new System.Drawing.Point(58, 4);
			this.chkCellYAny.Name = "chkCellYAny";
			this.chkCellYAny.Size = new System.Drawing.Size(44, 17);
			this.chkCellYAny.TabIndex = 1;
			this.chkCellYAny.Text = "Any";
			this.chkCellYAny.UseVisualStyleBackColor = true;
			this.chkCellYAny.CheckedChanged += new System.EventHandler(this.chkCellYAny_CheckedChanged);
			// 
			// gbCellX
			// 
			this.gbCellX.AutoSize = true;
			this.gbCellX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbCellX.Controls.Add(this.tlpCellX);
			this.gbCellX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbCellX.Location = new System.Drawing.Point(3, 57);
			this.gbCellX.Name = "gbCellX";
			this.gbCellX.Size = new System.Drawing.Size(111, 45);
			this.gbCellX.TabIndex = 1;
			this.gbCellX.TabStop = false;
			this.gbCellX.Text = "Cell X";
			// 
			// tlpCellX
			// 
			this.tlpCellX.AutoSize = true;
			this.tlpCellX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpCellX.ColumnCount = 2;
			this.tlpCellX.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCellX.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCellX.Controls.Add(this.numCellX, 0, 0);
			this.tlpCellX.Controls.Add(this.chkCellXAny, 1, 0);
			this.tlpCellX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpCellX.Location = new System.Drawing.Point(3, 16);
			this.tlpCellX.Name = "tlpCellX";
			this.tlpCellX.RowCount = 1;
			this.tlpCellX.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpCellX.Size = new System.Drawing.Size(105, 26);
			this.tlpCellX.TabIndex = 0;
			// 
			// numCellX
			// 
			this.numCellX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numCellX.Enabled = false;
			this.numCellX.Location = new System.Drawing.Point(3, 3);
			this.numCellX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.numCellX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
			this.numCellX.Name = "numCellX";
			this.numCellX.Size = new System.Drawing.Size(49, 20);
			this.numCellX.TabIndex = 0;
			// 
			// chkCellXAny
			// 
			this.chkCellXAny.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkCellXAny.AutoSize = true;
			this.chkCellXAny.Checked = true;
			this.chkCellXAny.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCellXAny.Location = new System.Drawing.Point(58, 4);
			this.chkCellXAny.Name = "chkCellXAny";
			this.chkCellXAny.Size = new System.Drawing.Size(44, 17);
			this.chkCellXAny.TabIndex = 1;
			this.chkCellXAny.Text = "Any";
			this.chkCellXAny.UseVisualStyleBackColor = true;
			this.chkCellXAny.CheckedChanged += new System.EventHandler(this.chkCellXAny_CheckedChanged);
			// 
			// gbWorld
			// 
			this.gbWorld.AutoSize = true;
			this.gbWorld.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.SetColumnSpan(this.gbWorld, 2);
			this.gbWorld.Controls.Add(this.tlpWorld);
			this.gbWorld.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbWorld.Location = new System.Drawing.Point(3, 3);
			this.gbWorld.Name = "gbWorld";
			this.gbWorld.Size = new System.Drawing.Size(228, 48);
			this.gbWorld.TabIndex = 0;
			this.gbWorld.TabStop = false;
			this.gbWorld.Text = "World";
			// 
			// tlpWorld
			// 
			this.tlpWorld.AutoSize = true;
			this.tlpWorld.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpWorld.ColumnCount = 3;
			this.tlpWorld.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpWorld.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpWorld.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpWorld.Controls.Add(this.numWorldID, 0, 0);
			this.tlpWorld.Controls.Add(this.chkWorldAny, 1, 0);
			this.tlpWorld.Controls.Add(this.btnWorldIDList, 2, 0);
			this.tlpWorld.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpWorld.Location = new System.Drawing.Point(3, 16);
			this.tlpWorld.Name = "tlpWorld";
			this.tlpWorld.RowCount = 1;
			this.tlpWorld.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpWorld.Size = new System.Drawing.Size(222, 29);
			this.tlpWorld.TabIndex = 0;
			// 
			// numWorldID
			// 
			this.numWorldID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numWorldID.Enabled = false;
			this.numWorldID.Hexadecimal = true;
			this.numWorldID.Location = new System.Drawing.Point(3, 4);
			this.numWorldID.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.numWorldID.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
			this.numWorldID.Name = "numWorldID";
			this.numWorldID.Size = new System.Drawing.Size(70, 20);
			this.numWorldID.TabIndex = 0;
			// 
			// chkWorldAny
			// 
			this.chkWorldAny.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkWorldAny.AutoSize = true;
			this.chkWorldAny.Checked = true;
			this.chkWorldAny.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkWorldAny.Location = new System.Drawing.Point(79, 6);
			this.chkWorldAny.Name = "chkWorldAny";
			this.chkWorldAny.Size = new System.Drawing.Size(44, 17);
			this.chkWorldAny.TabIndex = 1;
			this.chkWorldAny.Text = "Any";
			this.chkWorldAny.UseVisualStyleBackColor = true;
			this.chkWorldAny.CheckedChanged += new System.EventHandler(this.chkWorldAny_CheckedChanged);
			// 
			// btnWorldIDList
			// 
			this.btnWorldIDList.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnWorldIDList.AutoSize = true;
			this.btnWorldIDList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnWorldIDList.FlatAppearance.BorderSize = 0;
			this.btnWorldIDList.Location = new System.Drawing.Point(129, 3);
			this.btnWorldIDList.Name = "btnWorldIDList";
			this.btnWorldIDList.Size = new System.Drawing.Size(33, 23);
			this.btnWorldIDList.TabIndex = 2;
			this.btnWorldIDList.Text = "List";
			// 
			// LocationControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tlpMain);
			this.Name = "LocationControl";
			this.Size = new System.Drawing.Size(234, 105);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.gbCellY.ResumeLayout(false);
			this.gbCellY.PerformLayout();
			this.tlpCellY.ResumeLayout(false);
			this.tlpCellY.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCellY)).EndInit();
			this.gbCellX.ResumeLayout(false);
			this.gbCellX.PerformLayout();
			this.tlpCellX.ResumeLayout(false);
			this.tlpCellX.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numCellX)).EndInit();
			this.gbWorld.ResumeLayout(false);
			this.gbWorld.PerformLayout();
			this.tlpWorld.ResumeLayout(false);
			this.tlpWorld.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numWorldID)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.GroupBox gbCellY;
		private System.Windows.Forms.TableLayoutPanel tlpCellY;
		private System.Windows.Forms.NumericUpDown numCellY;
		private System.Windows.Forms.CheckBox chkCellYAny;
		private System.Windows.Forms.GroupBox gbCellX;
		private System.Windows.Forms.TableLayoutPanel tlpCellX;
		private System.Windows.Forms.NumericUpDown numCellX;
		private System.Windows.Forms.CheckBox chkCellXAny;
		private System.Windows.Forms.GroupBox gbWorld;
		private System.Windows.Forms.TableLayoutPanel tlpWorld;
		private System.Windows.Forms.NumericUpDown numWorldID;
		private System.Windows.Forms.CheckBox chkWorldAny;
		private System.Windows.Forms.Button btnWorldIDList;
	}
}
