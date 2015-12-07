namespace LiveSplit.AutoSplitting.Editors
{
	partial class MemoryValueEditor
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
			this.label2 = new System.Windows.Forms.Label();
			this.cbAddresses = new System.Windows.Forms.ComboBox();
			this.cbComparison = new System.Windows.Forms.ComboBox();
			this.cbValue = new System.Windows.Forms.ComboBox();
			this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
			this.chkOnChange = new System.Windows.Forms.CheckBox();
			this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
			this.chkContains = new System.Windows.Forms.CheckBox();
			this.tlpMain.SuspendLayout();
			this.flpOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.ColumnCount = 3;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.label1, 0, 0);
			this.tlpMain.Controls.Add(this.label2, 0, 1);
			this.tlpMain.Controls.Add(this.cbAddresses, 1, 0);
			this.tlpMain.Controls.Add(this.cbComparison, 1, 1);
			this.tlpMain.Controls.Add(this.cbValue, 2, 1);
			this.tlpMain.Controls.Add(this.flpOptions, 0, 2);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 3;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpMain.Size = new System.Drawing.Size(334, 122);
			this.tlpMain.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Address:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Value:";
			// 
			// cbAddresses
			// 
			this.cbAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tlpMain.SetColumnSpan(this.cbAddresses, 2);
			this.cbAddresses.DisplayMember = "Name";
			this.cbAddresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAddresses.FormattingEnabled = true;
			this.cbAddresses.IntegralHeight = false;
			this.cbAddresses.Location = new System.Drawing.Point(57, 4);
			this.cbAddresses.Name = "cbAddresses";
			this.cbAddresses.Size = new System.Drawing.Size(274, 21);
			this.cbAddresses.TabIndex = 0;
			this.cbAddresses.ValueMember = "Name";
			// 
			// cbComparison
			// 
			this.cbComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComparison.FormattingEnabled = true;
			this.cbComparison.Items.AddRange(new object[] {
            "GreaterOrEqualTo"});
			this.cbComparison.Location = new System.Drawing.Point(57, 33);
			this.cbComparison.Name = "cbComparison";
			this.cbComparison.Size = new System.Drawing.Size(115, 21);
			this.cbComparison.TabIndex = 1;
			// 
			// cbValue
			// 
			this.cbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbValue.FormattingEnabled = true;
			this.cbValue.Location = new System.Drawing.Point(178, 33);
			this.cbValue.Name = "cbValue";
			this.cbValue.Size = new System.Drawing.Size(153, 21);
			this.cbValue.TabIndex = 2;
			this.cbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbValue_KeyPress);
			// 
			// flpOptions
			// 
			this.flpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.flpOptions.AutoSize = true;
			this.tlpMain.SetColumnSpan(this.flpOptions, 3);
			this.flpOptions.Controls.Add(this.chkOnChange);
			this.flpOptions.Controls.Add(this.chkIgnoreCase);
			this.flpOptions.Controls.Add(this.chkContains);
			this.flpOptions.Location = new System.Drawing.Point(3, 78);
			this.flpOptions.Name = "flpOptions";
			this.flpOptions.Size = new System.Drawing.Size(328, 23);
			this.flpOptions.TabIndex = 3;
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
			// chkIgnoreCase
			// 
			this.chkIgnoreCase.AutoSize = true;
			this.chkIgnoreCase.Location = new System.Drawing.Point(88, 3);
			this.chkIgnoreCase.Name = "chkIgnoreCase";
			this.chkIgnoreCase.Size = new System.Drawing.Size(82, 17);
			this.chkIgnoreCase.TabIndex = 1;
			this.chkIgnoreCase.Text = "Ignore case";
			this.chkIgnoreCase.UseVisualStyleBackColor = true;
			this.chkIgnoreCase.Visible = false;
			// 
			// chkContains
			// 
			this.chkContains.AutoSize = true;
			this.chkContains.Location = new System.Drawing.Point(176, 3);
			this.chkContains.Name = "chkContains";
			this.chkContains.Size = new System.Drawing.Size(67, 17);
			this.chkContains.TabIndex = 2;
			this.chkContains.Text = "Contains";
			this.chkContains.UseVisualStyleBackColor = true;
			this.chkContains.Visible = false;
			// 
			// MemoryValueEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.tlpMain);
			this.Name = "MemoryValueEditor";
			this.Size = new System.Drawing.Size(334, 122);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.flpOptions.ResumeLayout(false);
			this.flpOptions.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		protected System.Windows.Forms.ComboBox cbAddresses;
		protected System.Windows.Forms.ComboBox cbComparison;
		protected System.Windows.Forms.ComboBox cbValue;
		private System.Windows.Forms.CheckBox chkOnChange;
		private System.Windows.Forms.FlowLayoutPanel flpOptions;
		private System.Windows.Forms.CheckBox chkIgnoreCase;
		private System.Windows.Forms.CheckBox chkContains;
	}
}