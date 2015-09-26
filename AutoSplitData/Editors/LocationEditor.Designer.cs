namespace LiveSplit.Skyrim.AutoSplitData.Editors
{
	partial class LocationEditor
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
			this.flpDialogButtons = new System.Windows.Forms.FlowLayoutPanel();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.locationArrayControl1 = new LiveSplit.Skyrim.AutoSplitData.Editors.LocationArrayControl();
			this.tlpMain.SuspendLayout();
			this.flpDialogButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.ColumnCount = 1;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpMain.Controls.Add(this.flpDialogButtons, 0, 1);
			this.tlpMain.Controls.Add(this.locationArrayControl1, 0, 0);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.Size = new System.Drawing.Size(334, 173);
			this.tlpMain.TabIndex = 2;
			// 
			// flpDialogButtons
			// 
			this.flpDialogButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.flpDialogButtons.AutoSize = true;
			this.flpDialogButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flpDialogButtons.Controls.Add(this.btnOK);
			this.flpDialogButtons.Controls.Add(this.btnCancel);
			this.flpDialogButtons.Location = new System.Drawing.Point(86, 141);
			this.flpDialogButtons.Name = "flpDialogButtons";
			this.flpDialogButtons.Size = new System.Drawing.Size(162, 29);
			this.flpDialogButtons.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(3, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(84, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// locationArrayControl1
			// 
			this.locationArrayControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.locationArrayControl1.Location = new System.Drawing.Point(3, 3);
			this.locationArrayControl1.MinimumSize = new System.Drawing.Size(0, 135);
			this.locationArrayControl1.Name = "locationArrayControl1";
			this.locationArrayControl1.Size = new System.Drawing.Size(328, 135);
			this.locationArrayControl1.TabIndex = 0;
			// 
			// LocationEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 173);
			this.ControlBox = false;
			this.Controls.Add(this.tlpMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "LocationEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Location Editor";
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.flpDialogButtons.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.FlowLayoutPanel flpDialogButtons;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private LocationArrayControl locationArrayControl1;
	}
}