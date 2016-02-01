namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	partial class LocationValueEditor
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
			this.toolTipTabButtons = new System.Windows.Forms.ToolTip(this.components);
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.cbAddresses = new System.Windows.Forms.ComboBox();
			this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
			this.chkOnChange = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.locationValueControl1 = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationValueControl();
			this.tlpMain.SuspendLayout();
			this.flpOptions.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.ColumnCount = 3;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.label1, 0, 0);
			this.tlpMain.Controls.Add(this.cbAddresses, 1, 0);
			this.tlpMain.Controls.Add(this.flpOptions, 0, 2);
			this.tlpMain.Controls.Add(this.groupBox1, 0, 1);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 3;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(334, 249);
			this.tlpMain.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Address:";
			// 
			// cbAddresses
			// 
			this.cbAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tlpMain.SetColumnSpan(this.cbAddresses, 2);
			this.cbAddresses.DisplayMember = "Name";
			this.cbAddresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAddresses.Enabled = false;
			this.cbAddresses.FormattingEnabled = true;
			this.cbAddresses.IntegralHeight = false;
			this.cbAddresses.Items.AddRange(new object[] {
            "Location"});
			this.cbAddresses.Location = new System.Drawing.Point(57, 3);
			this.cbAddresses.Name = "cbAddresses";
			this.cbAddresses.Size = new System.Drawing.Size(274, 21);
			this.cbAddresses.TabIndex = 0;
			this.cbAddresses.ValueMember = "Name";
			// 
			// flpOptions
			// 
			this.flpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.flpOptions.AutoSize = true;
			this.tlpMain.SetColumnSpan(this.flpOptions, 3);
			this.flpOptions.Controls.Add(this.chkOnChange);
			this.flpOptions.Location = new System.Drawing.Point(3, 223);
			this.flpOptions.Name = "flpOptions";
			this.flpOptions.Size = new System.Drawing.Size(328, 23);
			this.flpOptions.TabIndex = 2;
			// 
			// chkOnChange
			// 
			this.chkOnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.chkOnChange.AutoSize = true;
			this.chkOnChange.Location = new System.Drawing.Point(3, 3);
			this.chkOnChange.Name = "chkOnChange";
			this.chkOnChange.Size = new System.Drawing.Size(79, 17);
			this.chkOnChange.TabIndex = 0;
			this.chkOnChange.Text = "On change";
			this.chkOnChange.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.SetColumnSpan(this.groupBox1, 3);
			this.groupBox1.Controls.Add(this.locationValueControl1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(328, 187);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Value";
			// 
			// locationValueControl1
			// 
			this.locationValueControl1.AutoSize = true;
			this.locationValueControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationValueControl1.Location = new System.Drawing.Point(3, 16);
			this.locationValueControl1.Name = "locationValueControl1";
			this.locationValueControl1.Size = new System.Drawing.Size(322, 168);
			this.locationValueControl1.TabIndex = 0;
			this.locationValueControl1.ValueEquals = true;
			// 
			// LocationValueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.tlpMain);
			this.Name = "LocationValueEditor";
			this.Size = new System.Drawing.Size(334, 249);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.flpOptions.ResumeLayout(false);
			this.flpOptions.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.Label label1;
		protected System.Windows.Forms.ComboBox cbAddresses;
		private System.Windows.Forms.FlowLayoutPanel flpOptions;
		private System.Windows.Forms.CheckBox chkOnChange;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolTip toolTipTabButtons;
		private LocationValueControl locationValueControl1;
	}
}