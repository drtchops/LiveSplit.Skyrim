namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	partial class LocationValueControl
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
			this.components = new System.ComponentModel.Container();
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.cbComparison = new System.Windows.Forms.ComboBox();
			this.locationArrayControl1 = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationArrayControl();
			this.flpTabButtons = new System.Windows.Forms.FlowLayoutPanel();
			this.btnRemoveTab = new System.Windows.Forms.Button();
			this.btnAddTab = new System.Windows.Forms.Button();
			this.toolTipTabButtons = new System.Windows.Forms.ToolTip(this.components);
			this.tlpMain.SuspendLayout();
			this.flpTabButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.ColumnCount = 2;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMain.Controls.Add(this.cbComparison, 0, 0);
			this.tlpMain.Controls.Add(this.locationArrayControl1, 0, 1);
			this.tlpMain.Controls.Add(this.flpTabButtons, 1, 0);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.MinimumSize = new System.Drawing.Size(230, 168);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(230, 168);
			this.tlpMain.TabIndex = 1;
			// 
			// cbComparison
			// 
			this.cbComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComparison.FormattingEnabled = true;
			this.cbComparison.Location = new System.Drawing.Point(3, 3);
			this.cbComparison.Name = "cbComparison";
			this.cbComparison.Size = new System.Drawing.Size(166, 21);
			this.cbComparison.TabIndex = 0;
			// 
			// locationArrayControl1
			// 
			this.tlpMain.SetColumnSpan(this.locationArrayControl1, 2);
			this.locationArrayControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationArrayControl1.Location = new System.Drawing.Point(3, 30);
			this.locationArrayControl1.MinimumSize = new System.Drawing.Size(0, 135);
			this.locationArrayControl1.Name = "locationArrayControl1";
			this.locationArrayControl1.Size = new System.Drawing.Size(224, 135);
			this.locationArrayControl1.TabIndex = 2;
			// 
			// flpTabButtons
			// 
			this.flpTabButtons.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.flpTabButtons.AutoSize = true;
			this.flpTabButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flpTabButtons.Controls.Add(this.btnRemoveTab);
			this.flpTabButtons.Controls.Add(this.btnAddTab);
			this.flpTabButtons.Location = new System.Drawing.Point(172, 2);
			this.flpTabButtons.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.flpTabButtons.Name = "flpTabButtons";
			this.flpTabButtons.Size = new System.Drawing.Size(55, 23);
			this.flpTabButtons.TabIndex = 1;
			this.flpTabButtons.WrapContents = false;
			// 
			// btnRemoveTab
			// 
			this.btnRemoveTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnRemoveTab.FlatAppearance.BorderSize = 0;
			this.btnRemoveTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnRemoveTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnRemoveTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemoveTab.Location = new System.Drawing.Point(3, 0);
			this.btnRemoveTab.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnRemoveTab.Name = "btnRemoveTab";
			this.btnRemoveTab.Size = new System.Drawing.Size(23, 23);
			this.btnRemoveTab.TabIndex = 0;
			this.btnRemoveTab.Text = "-";
			this.btnRemoveTab.UseVisualStyleBackColor = true;
			this.btnRemoveTab.Click += new System.EventHandler(this.btnRemoveTab_Click);
			// 
			// btnAddTab
			// 
			this.btnAddTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnAddTab.FlatAppearance.BorderSize = 0;
			this.btnAddTab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnAddTab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnAddTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddTab.Location = new System.Drawing.Point(29, 0);
			this.btnAddTab.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnAddTab.Name = "btnAddTab";
			this.btnAddTab.Size = new System.Drawing.Size(23, 23);
			this.btnAddTab.TabIndex = 1;
			this.btnAddTab.Text = "+";
			this.btnAddTab.UseVisualStyleBackColor = true;
			// 
			// LocationValueControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tlpMain);
			this.Name = "LocationValueControl";
			this.Size = new System.Drawing.Size(230, 168);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.flpTabButtons.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		protected System.Windows.Forms.ComboBox cbComparison;
		private LocationArrayControl locationArrayControl1;
		private System.Windows.Forms.FlowLayoutPanel flpTabButtons;
		private System.Windows.Forms.Button btnRemoveTab;
		private System.Windows.Forms.Button btnAddTab;
		private System.Windows.Forms.ToolTip toolTipTabButtons;
	}
}
