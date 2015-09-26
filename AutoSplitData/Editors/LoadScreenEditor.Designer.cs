namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	partial class LoadScreenEditor
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
			this.gbEndLocation = new System.Windows.Forms.GroupBox();
			this.locationEnd = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationValueControl();
			this.gbStartLocation = new System.Windows.Forms.GroupBox();
			this.locationStart = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationValueControl();
			this.toolTipTabButtons = new System.Windows.Forms.ToolTip(this.components);
			this.tlpMain.SuspendLayout();
			this.gbEndLocation.SuspendLayout();
			this.gbStartLocation.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.ColumnCount = 2;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpMain.Controls.Add(this.gbEndLocation, 1, 0);
			this.tlpMain.Controls.Add(this.gbStartLocation, 0, 0);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 1;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(484, 193);
			this.tlpMain.TabIndex = 2;
			// 
			// gbEndLocation
			// 
			this.gbEndLocation.AutoSize = true;
			this.gbEndLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbEndLocation.Controls.Add(this.locationEnd);
			this.gbEndLocation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbEndLocation.Location = new System.Drawing.Point(245, 3);
			this.gbEndLocation.Name = "gbEndLocation";
			this.gbEndLocation.Size = new System.Drawing.Size(236, 187);
			this.gbEndLocation.TabIndex = 1;
			this.gbEndLocation.TabStop = false;
			this.gbEndLocation.Text = "End Location";
			// 
			// locationEnd
			// 
			this.locationEnd.AutoSize = true;
			this.locationEnd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationEnd.Location = new System.Drawing.Point(3, 16);
			this.locationEnd.MinimumSize = new System.Drawing.Size(230, 168);
			this.locationEnd.Name = "locationEnd";
			this.locationEnd.Size = new System.Drawing.Size(230, 168);
			this.locationEnd.TabIndex = 0;
			this.locationEnd.ValueEquals = true;
			// 
			// gbStartLocation
			// 
			this.gbStartLocation.AutoSize = true;
			this.gbStartLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbStartLocation.Controls.Add(this.locationStart);
			this.gbStartLocation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbStartLocation.Location = new System.Drawing.Point(3, 3);
			this.gbStartLocation.Name = "gbStartLocation";
			this.gbStartLocation.Size = new System.Drawing.Size(236, 187);
			this.gbStartLocation.TabIndex = 0;
			this.gbStartLocation.TabStop = false;
			this.gbStartLocation.Text = "Start Location";
			// 
			// locationStart
			// 
			this.locationStart.AutoSize = true;
			this.locationStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationStart.Location = new System.Drawing.Point(3, 16);
			this.locationStart.MinimumSize = new System.Drawing.Size(230, 168);
			this.locationStart.Name = "locationStart";
			this.locationStart.Size = new System.Drawing.Size(230, 168);
			this.locationStart.TabIndex = 0;
			this.locationStart.ValueEquals = true;
			// 
			// LoadScreenEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.tlpMain);
			this.Name = "LoadScreenEditor";
			this.Size = new System.Drawing.Size(484, 193);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.gbEndLocation.ResumeLayout(false);
			this.gbEndLocation.PerformLayout();
			this.gbStartLocation.ResumeLayout(false);
			this.gbStartLocation.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.GroupBox gbStartLocation;
		private System.Windows.Forms.GroupBox gbEndLocation;
		private System.Windows.Forms.ToolTip toolTipTabButtons;
		private LocationValueControl locationStart;
		private LocationValueControl locationEnd;
	}
}