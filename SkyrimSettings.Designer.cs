namespace LiveSplit.Skyrim
{
    partial class SkyrimSettings
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
            this.gbStartSplits = new System.Windows.Forms.GroupBox();
            this.tlpStartSplits = new System.Windows.Forms.TableLayoutPanel();
            this.chkHelgen = new System.Windows.Forms.CheckBox();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.gbEndSplits = new System.Windows.Forms.GroupBox();
            this.tlpEndSplits = new System.Windows.Forms.TableLayoutPanel();
            this.chkAlduinDefeated = new System.Windows.Forms.CheckBox();
            this.chkTheEyeofMagnus = new System.Windows.Forms.CheckBox();
            this.chkGloryOfTheDead = new System.Windows.Forms.CheckBox();
            this.chkDarknessReturns = new System.Windows.Forms.CheckBox();
            this.chkHailSithis = new System.Windows.Forms.CheckBox();
            this.gbMiscellaneous = new System.Windows.Forms.GroupBox();
            this.tlpMiscellaneous = new System.Windows.Forms.TableLayoutPanel();
            this.chkDisplayWithoutLoads = new System.Windows.Forms.CheckBox();
            this.chkPauseInEscapeMenu = new System.Windows.Forms.CheckBox();
            this.tlpMain.SuspendLayout();
            this.gbStartSplits.SuspendLayout();
            this.tlpStartSplits.SuspendLayout();
            this.gbEndSplits.SuspendLayout();
            this.tlpEndSplits.SuspendLayout();
            this.gbMiscellaneous.SuspendLayout();
            this.tlpMiscellaneous.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.gbStartSplits, 0, 0);
            this.tlpMain.Controls.Add(this.gbEndSplits, 0, 1);
            this.tlpMain.Controls.Add(this.gbMiscellaneous, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(476, 365);
            this.tlpMain.TabIndex = 0;
            // 
            // gbStartSplits
            // 
            this.gbStartSplits.AutoSize = true;
            this.gbStartSplits.Controls.Add(this.tlpStartSplits);
            this.gbStartSplits.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbStartSplits.Location = new System.Drawing.Point(3, 3);
            this.gbStartSplits.Name = "gbStartSplits";
            this.gbStartSplits.Size = new System.Drawing.Size(470, 65);
            this.gbStartSplits.TabIndex = 5;
            this.gbStartSplits.TabStop = false;
            this.gbStartSplits.Text = "Start Auto-splits";
            // 
            // tlpStartSplits
            // 
            this.tlpStartSplits.AutoSize = true;
            this.tlpStartSplits.BackColor = System.Drawing.Color.Transparent;
            this.tlpStartSplits.ColumnCount = 1;
            this.tlpStartSplits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStartSplits.Controls.Add(this.chkHelgen, 0, 1);
            this.tlpStartSplits.Controls.Add(this.chkAutoStart, 0, 0);
            this.tlpStartSplits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStartSplits.Location = new System.Drawing.Point(3, 16);
            this.tlpStartSplits.Name = "tlpStartSplits";
            this.tlpStartSplits.RowCount = 2;
            this.tlpStartSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpStartSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpStartSplits.Size = new System.Drawing.Size(464, 46);
            this.tlpStartSplits.TabIndex = 4;
            // 
            // chkHelgen
            // 
            this.chkHelgen.AutoSize = true;
            this.chkHelgen.Location = new System.Drawing.Point(3, 26);
            this.chkHelgen.Name = "chkHelgen";
            this.chkHelgen.Size = new System.Drawing.Size(60, 17);
            this.chkHelgen.TabIndex = 7;
            this.chkHelgen.Text = "Helgen";
            this.chkHelgen.UseVisualStyleBackColor = true;
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Checked = true;
            this.chkAutoStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoStart.Location = new System.Drawing.Point(3, 3);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(87, 17);
            this.chkAutoStart.TabIndex = 4;
            this.chkAutoStart.Text = "Start / Reset";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // gbEndSplits
            // 
            this.gbEndSplits.AutoSize = true;
            this.gbEndSplits.Controls.Add(this.tlpEndSplits);
            this.gbEndSplits.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbEndSplits.Location = new System.Drawing.Point(3, 74);
            this.gbEndSplits.Name = "gbEndSplits";
            this.gbEndSplits.Size = new System.Drawing.Size(470, 134);
            this.gbEndSplits.TabIndex = 7;
            this.gbEndSplits.TabStop = false;
            this.gbEndSplits.Text = "End Auto-splits";
            // 
            // tlpEndSplits
            // 
            this.tlpEndSplits.AutoSize = true;
            this.tlpEndSplits.BackColor = System.Drawing.Color.Transparent;
            this.tlpEndSplits.ColumnCount = 1;
            this.tlpEndSplits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEndSplits.Controls.Add(this.chkAlduinDefeated, 0, 0);
            this.tlpEndSplits.Controls.Add(this.chkTheEyeofMagnus, 0, 1);
            this.tlpEndSplits.Controls.Add(this.chkGloryOfTheDead, 0, 2);
            this.tlpEndSplits.Controls.Add(this.chkDarknessReturns, 0, 4);
            this.tlpEndSplits.Controls.Add(this.chkHailSithis, 0, 3);
            this.tlpEndSplits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEndSplits.Location = new System.Drawing.Point(3, 16);
            this.tlpEndSplits.Name = "tlpEndSplits";
            this.tlpEndSplits.RowCount = 5;
            this.tlpEndSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEndSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEndSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEndSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEndSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpEndSplits.Size = new System.Drawing.Size(464, 115);
            this.tlpEndSplits.TabIndex = 4;
            // 
            // chkAlduinDefeated
            // 
            this.chkAlduinDefeated.AutoSize = true;
            this.chkAlduinDefeated.Checked = true;
            this.chkAlduinDefeated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlduinDefeated.Location = new System.Drawing.Point(3, 3);
            this.chkAlduinDefeated.Name = "chkAlduinDefeated";
            this.chkAlduinDefeated.Size = new System.Drawing.Size(160, 17);
            this.chkAlduinDefeated.TabIndex = 5;
            this.chkAlduinDefeated.Text = "Alduin\'s Defeat (Main Quest)";
            this.chkAlduinDefeated.UseVisualStyleBackColor = true;
            // 
            // chkTheEyeofMagnus
            // 
            this.chkTheEyeofMagnus.AutoSize = true;
            this.chkTheEyeofMagnus.Location = new System.Drawing.Point(3, 26);
            this.chkTheEyeofMagnus.Name = "chkTheEyeofMagnus";
            this.chkTheEyeofMagnus.Size = new System.Drawing.Size(404, 17);
            this.chkTheEyeofMagnus.TabIndex = 10;
            this.chkTheEyeofMagnus.Text = "[EXPERIMENTAL] The Eye of Magnus quest completion (College of Winterhold)";
            this.chkTheEyeofMagnus.UseVisualStyleBackColor = true;
            // 
            // chkGloryOfTheDead
            // 
            this.chkGloryOfTheDead.AutoSize = true;
            this.chkGloryOfTheDead.Location = new System.Drawing.Point(3, 49);
            this.chkGloryOfTheDead.Name = "chkGloryOfTheDead";
            this.chkGloryOfTheDead.Size = new System.Drawing.Size(355, 17);
            this.chkGloryOfTheDead.TabIndex = 8;
            this.chkGloryOfTheDead.Text = "[EXPERIMENTAL] Glory of The Dead quest completion (Companions)";
            this.chkGloryOfTheDead.UseVisualStyleBackColor = true;
            // 
            // chkDarknessReturns
            // 
            this.chkDarknessReturns.AutoSize = true;
            this.chkDarknessReturns.Location = new System.Drawing.Point(3, 95);
            this.chkDarknessReturns.Name = "chkDarknessReturns";
            this.chkDarknessReturns.Size = new System.Drawing.Size(360, 17);
            this.chkDarknessReturns.TabIndex = 9;
            this.chkDarknessReturns.Text = "[EXPERIMENTAL] Darkness Returns quest completion (Thieves Guild)";
            this.chkDarknessReturns.UseVisualStyleBackColor = true;
            // 
            // chkHailSithis
            // 
            this.chkHailSithis.AutoSize = true;
            this.chkHailSithis.Location = new System.Drawing.Point(3, 72);
            this.chkHailSithis.Name = "chkHailSithis";
            this.chkHailSithis.Size = new System.Drawing.Size(340, 17);
            this.chkHailSithis.TabIndex = 7;
            this.chkHailSithis.Text = "[EXPERIMENTAL] Hail Sithis quest completion (Dark Brotherhood)";
            this.chkHailSithis.UseVisualStyleBackColor = true;
            // 
            // gbMiscellaneous
            // 
            this.gbMiscellaneous.AutoSize = true;
            this.gbMiscellaneous.Controls.Add(this.tlpMiscellaneous);
            this.gbMiscellaneous.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMiscellaneous.Location = new System.Drawing.Point(3, 214);
            this.gbMiscellaneous.Name = "gbMiscellaneous";
            this.gbMiscellaneous.Size = new System.Drawing.Size(470, 65);
            this.gbMiscellaneous.TabIndex = 9;
            this.gbMiscellaneous.TabStop = false;
            this.gbMiscellaneous.Text = "Miscellaneous";
            // 
            // tlpMiscellaneous
            // 
            this.tlpMiscellaneous.AutoSize = true;
            this.tlpMiscellaneous.ColumnCount = 1;
            this.tlpMiscellaneous.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMiscellaneous.Controls.Add(this.chkDisplayWithoutLoads, 0, 0);
            this.tlpMiscellaneous.Controls.Add(this.chkPauseInEscapeMenu, 0, 1);
            this.tlpMiscellaneous.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMiscellaneous.Location = new System.Drawing.Point(3, 16);
            this.tlpMiscellaneous.Name = "tlpMiscellaneous";
            this.tlpMiscellaneous.RowCount = 2;
            this.tlpMiscellaneous.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMiscellaneous.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMiscellaneous.Size = new System.Drawing.Size(464, 46);
            this.tlpMiscellaneous.TabIndex = 0;
            // 
            // chkDisplayWithoutLoads
            // 
            this.chkDisplayWithoutLoads.AutoSize = true;
            this.chkDisplayWithoutLoads.Checked = true;
            this.chkDisplayWithoutLoads.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayWithoutLoads.Location = new System.Drawing.Point(3, 3);
            this.chkDisplayWithoutLoads.Name = "chkDisplayWithoutLoads";
            this.chkDisplayWithoutLoads.Size = new System.Drawing.Size(165, 17);
            this.chkDisplayWithoutLoads.TabIndex = 0;
            this.chkDisplayWithoutLoads.Text = "Show alternate timing method";
            this.chkDisplayWithoutLoads.UseVisualStyleBackColor = true;
            // 
            // chkPauseInEscapeMenu
            // 
            this.chkPauseInEscapeMenu.AutoSize = true;
            this.chkPauseInEscapeMenu.Location = new System.Drawing.Point(3, 26);
            this.chkPauseInEscapeMenu.Name = "chkPauseInEscapeMenu";
            this.chkPauseInEscapeMenu.Size = new System.Drawing.Size(195, 17);
            this.chkPauseInEscapeMenu.TabIndex = 0;
            this.chkPauseInEscapeMenu.Text = "Pause the timer in the escape menu";
            this.chkPauseInEscapeMenu.UseVisualStyleBackColor = true;
            // 
            // SkyrimSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "SkyrimSettings";
            this.Size = new System.Drawing.Size(476, 443);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.gbStartSplits.ResumeLayout(false);
            this.gbStartSplits.PerformLayout();
            this.tlpStartSplits.ResumeLayout(false);
            this.tlpStartSplits.PerformLayout();
            this.gbEndSplits.ResumeLayout(false);
            this.gbEndSplits.PerformLayout();
            this.tlpEndSplits.ResumeLayout(false);
            this.tlpEndSplits.PerformLayout();
            this.gbMiscellaneous.ResumeLayout(false);
            this.gbMiscellaneous.PerformLayout();
            this.tlpMiscellaneous.ResumeLayout(false);
            this.tlpMiscellaneous.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox gbStartSplits;
        private System.Windows.Forms.GroupBox gbEndSplits;
        private System.Windows.Forms.TableLayoutPanel tlpEndSplits;
        private System.Windows.Forms.CheckBox chkAlduinDefeated;
        private System.Windows.Forms.TableLayoutPanel tlpStartSplits;
        private System.Windows.Forms.CheckBox chkHelgen;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.CheckBox chkHailSithis;
        private System.Windows.Forms.CheckBox chkGloryOfTheDead;
        private System.Windows.Forms.CheckBox chkDisplayWithoutLoads;
        private System.Windows.Forms.CheckBox chkDarknessReturns;
        private System.Windows.Forms.CheckBox chkTheEyeofMagnus;
        private System.Windows.Forms.GroupBox gbMiscellaneous;
        private System.Windows.Forms.TableLayoutPanel tlpMiscellaneous;
        private System.Windows.Forms.CheckBox chkPauseInEscapeMenu;
    }
}
