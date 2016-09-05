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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabAnyPercent = new System.Windows.Forms.TabPage();
			this.tlpAutoSplits = new System.Windows.Forms.TableLayoutPanel();
			this.tlpAutosplitPreset = new System.Windows.Forms.TableLayoutPanel();
			this.tlpPresetsList = new System.Windows.Forms.TableLayoutPanel();
			this.btnOther = new System.Windows.Forms.Button();
			this.btnCustomize = new System.Windows.Forms.Button();
			this.cbPreset = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chklbSplits = new System.Windows.Forms.CheckedListBox();
			this.tlpAutosplits1stRow = new System.Windows.Forms.TableLayoutPanel();
			this.llCheckAll = new System.Windows.Forms.LinkLabel();
			this.lWarningNbrAutoSplit = new System.Windows.Forms.Label();
			this.tabTools = new System.Windows.Forms.TabPage();
			this.tlpTools = new System.Windows.Forms.TableLayoutPanel();
			this.gbToolbox = new System.Windows.Forms.GroupBox();
			this.flpToolbox = new System.Windows.Forms.FlowLayoutPanel();
			this.btnLaunchRamWatch = new System.Windows.Forms.Button();
			this.btnLaunchLoadScreenLog = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tlpToolSettings = new System.Windows.Forms.TableLayoutPanel();
			this.gbRamWatch = new System.Windows.Forms.GroupBox();
			this.tlpRamWatchAddr = new System.Windows.Forms.TableLayoutPanel();
			this.tlpListBtn = new System.Windows.Forms.TableLayoutPanel();
			this.btnMoveDownAddr = new System.Windows.Forms.Button();
			this.btnMoveUpAddr = new System.Windows.Forms.Button();
			this.btnAddAddr = new System.Windows.Forms.Button();
			this.btnRemoveAddr = new System.Windows.Forms.Button();
			this.lstAddresses = new System.Windows.Forms.ListBox();
			this.tabBearCart = new System.Windows.Forms.TabPage();
			this.tlpBearCart = new System.Windows.Forms.TableLayoutPanel();
			this.chkBearCartPBNotification = new System.Windows.Forms.CheckBox();
			this.gbBearCartSound = new System.Windows.Forms.GroupBox();
			this.tlpSound = new System.Windows.Forms.TableLayoutPanel();
			this.lVolume = new System.Windows.Forms.Label();
			this.tbGeneralVolume = new System.Windows.Forms.TrackBar();
			this.chkPlayBearCartSound = new System.Windows.Forms.CheckBox();
			this.btnBrowseBearCartSound = new System.Windows.Forms.Button();
			this.lSound = new System.Windows.Forms.Label();
			this.txtBearCartSoundPath = new System.Windows.Forms.TextBox();
			this.chkPlayBearCartSoundOnlyOnPB = new System.Windows.Forms.CheckBox();
			this.btnBearCartSoundTest = new System.Windows.Forms.Button();
			this.lBearCartPB = new System.Windows.Forms.Label();
			this.gbGeneral = new System.Windows.Forms.GroupBox();
			this.flpGeneral = new System.Windows.Forms.FlowLayoutPanel();
			this.chkAutoStart = new System.Windows.Forms.CheckBox();
			this.chkAutoReset = new System.Windows.Forms.CheckBox();
			this.chkAutoUpdatePresets = new System.Windows.Forms.CheckBox();
			this.tlpMain.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabAnyPercent.SuspendLayout();
			this.tlpAutoSplits.SuspendLayout();
			this.tlpAutosplitPreset.SuspendLayout();
			this.tlpPresetsList.SuspendLayout();
			this.tlpAutosplits1stRow.SuspendLayout();
			this.tabTools.SuspendLayout();
			this.tlpTools.SuspendLayout();
			this.gbToolbox.SuspendLayout();
			this.flpToolbox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tlpToolSettings.SuspendLayout();
			this.gbRamWatch.SuspendLayout();
			this.tlpRamWatchAddr.SuspendLayout();
			this.tlpListBtn.SuspendLayout();
			this.tabBearCart.SuspendLayout();
			this.tlpBearCart.SuspendLayout();
			this.gbBearCartSound.SuspendLayout();
			this.tlpSound.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbGeneralVolume)).BeginInit();
			this.gbGeneral.SuspendLayout();
			this.flpGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.AutoSize = true;
			this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpMain.ColumnCount = 1;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.tabControl1, 0, 1);
			this.tlpMain.Controls.Add(this.gbGeneral, 0, 0);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(7, 7);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Size = new System.Drawing.Size(470, 496);
			this.tlpMain.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabAnyPercent);
			this.tabControl1.Controls.Add(this.tabTools);
			this.tabControl1.Controls.Add(this.tabBearCart);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 97);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(464, 396);
			this.tabControl1.TabIndex = 1;
			// 
			// tabAnyPercent
			// 
			this.tabAnyPercent.BackColor = System.Drawing.SystemColors.Control;
			this.tabAnyPercent.Controls.Add(this.tlpAutoSplits);
			this.tabAnyPercent.Location = new System.Drawing.Point(4, 22);
			this.tabAnyPercent.Name = "tabAnyPercent";
			this.tabAnyPercent.Padding = new System.Windows.Forms.Padding(3);
			this.tabAnyPercent.Size = new System.Drawing.Size(456, 370);
			this.tabAnyPercent.TabIndex = 0;
			this.tabAnyPercent.Text = "Autosplits";
			// 
			// tlpAutoSplits
			// 
			this.tlpAutoSplits.ColumnCount = 1;
			this.tlpAutoSplits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpAutoSplits.Controls.Add(this.tlpAutosplitPreset, 0, 2);
			this.tlpAutoSplits.Controls.Add(this.chklbSplits, 0, 1);
			this.tlpAutoSplits.Controls.Add(this.tlpAutosplits1stRow, 0, 0);
			this.tlpAutoSplits.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpAutoSplits.Location = new System.Drawing.Point(3, 3);
			this.tlpAutoSplits.Name = "tlpAutoSplits";
			this.tlpAutoSplits.RowCount = 3;
			this.tlpAutoSplits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpAutoSplits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpAutoSplits.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpAutoSplits.Size = new System.Drawing.Size(450, 364);
			this.tlpAutoSplits.TabIndex = 0;
			// 
			// tlpAutosplitPreset
			// 
			this.tlpAutosplitPreset.AutoSize = true;
			this.tlpAutosplitPreset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpAutosplitPreset.ColumnCount = 3;
			this.tlpAutosplitPreset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpAutosplitPreset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpAutosplitPreset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpAutosplitPreset.Controls.Add(this.tlpPresetsList, 0, 0);
			this.tlpAutosplitPreset.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpAutosplitPreset.Location = new System.Drawing.Point(3, 332);
			this.tlpAutosplitPreset.Name = "tlpAutosplitPreset";
			this.tlpAutosplitPreset.RowCount = 1;
			this.tlpAutosplitPreset.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpAutosplitPreset.Size = new System.Drawing.Size(444, 29);
			this.tlpAutosplitPreset.TabIndex = 2;
			// 
			// tlpPresetsList
			// 
			this.tlpPresetsList.AutoSize = true;
			this.tlpPresetsList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpPresetsList.ColumnCount = 4;
			this.tlpPresetsList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpPresetsList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpPresetsList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpPresetsList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpPresetsList.Controls.Add(this.btnOther, 3, 0);
			this.tlpPresetsList.Controls.Add(this.btnCustomize, 2, 0);
			this.tlpPresetsList.Controls.Add(this.cbPreset, 1, 0);
			this.tlpPresetsList.Controls.Add(this.label1, 0, 0);
			this.tlpPresetsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpPresetsList.Location = new System.Drawing.Point(0, 0);
			this.tlpPresetsList.Margin = new System.Windows.Forms.Padding(0);
			this.tlpPresetsList.Name = "tlpPresetsList";
			this.tlpPresetsList.RowCount = 1;
			this.tlpPresetsList.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpPresetsList.Size = new System.Drawing.Size(444, 29);
			this.tlpPresetsList.TabIndex = 2;
			// 
			// btnOther
			// 
			this.btnOther.AutoSize = true;
			this.btnOther.Location = new System.Drawing.Point(367, 3);
			this.btnOther.Name = "btnOther";
			this.btnOther.Size = new System.Drawing.Size(74, 23);
			this.btnOther.TabIndex = 2;
			this.btnOther.Text = "Other...";
			this.btnOther.UseVisualStyleBackColor = true;
			this.btnOther.Click += new System.EventHandler(this.btnOther_Click);
			// 
			// btnCustomize
			// 
			this.btnCustomize.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnCustomize.AutoSize = true;
			this.btnCustomize.Location = new System.Drawing.Point(287, 3);
			this.btnCustomize.Name = "btnCustomize";
			this.btnCustomize.Size = new System.Drawing.Size(74, 23);
			this.btnCustomize.TabIndex = 1;
			this.btnCustomize.Text = "Customize...";
			this.btnCustomize.UseVisualStyleBackColor = true;
			this.btnCustomize.Click += new System.EventHandler(this.btnCustomize_Click);
			// 
			// cbPreset
			// 
			this.cbPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPreset.DisplayMember = "Name";
			this.cbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPreset.FormattingEnabled = true;
			this.cbPreset.Items.AddRange(new object[] {
            "Custom"});
			this.cbPreset.Location = new System.Drawing.Point(46, 4);
			this.cbPreset.Name = "cbPreset";
			this.cbPreset.Size = new System.Drawing.Size(235, 21);
			this.cbPreset.TabIndex = 0;
			this.cbPreset.ValueMember = "Name";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Preset:";
			// 
			// chklbSplits
			// 
			this.chklbSplits.CheckOnClick = true;
			this.chklbSplits.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chklbSplits.FormattingEnabled = true;
			this.chklbSplits.IntegralHeight = false;
			this.chklbSplits.Location = new System.Drawing.Point(3, 23);
			this.chklbSplits.MultiColumn = true;
			this.chklbSplits.Name = "chklbSplits";
			this.chklbSplits.Size = new System.Drawing.Size(444, 303);
			this.chklbSplits.TabIndex = 1;
			// 
			// tlpAutosplits1stRow
			// 
			this.tlpAutosplits1stRow.AutoSize = true;
			this.tlpAutosplits1stRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpAutosplits1stRow.ColumnCount = 2;
			this.tlpAutosplits1stRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpAutosplits1stRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpAutosplits1stRow.Controls.Add(this.llCheckAll, 1, 0);
			this.tlpAutosplits1stRow.Controls.Add(this.lWarningNbrAutoSplit, 0, 0);
			this.tlpAutosplits1stRow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpAutosplits1stRow.Location = new System.Drawing.Point(0, 0);
			this.tlpAutosplits1stRow.Margin = new System.Windows.Forms.Padding(0);
			this.tlpAutosplits1stRow.Name = "tlpAutosplits1stRow";
			this.tlpAutosplits1stRow.RowCount = 1;
			this.tlpAutosplits1stRow.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpAutosplits1stRow.Size = new System.Drawing.Size(450, 20);
			this.tlpAutosplits1stRow.TabIndex = 0;
			// 
			// llCheckAll
			// 
			this.llCheckAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.llCheckAll.AutoSize = true;
			this.llCheckAll.Location = new System.Drawing.Point(330, 3);
			this.llCheckAll.Name = "llCheckAll";
			this.llCheckAll.Size = new System.Drawing.Size(117, 13);
			this.llCheckAll.TabIndex = 0;
			this.llCheckAll.TabStop = true;
			this.llCheckAll.Text = "Activate/Deactivate All";
			this.llCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llCheckAll_LinkClicked);
			// 
			// lWarningNbrAutoSplit
			// 
			this.lWarningNbrAutoSplit.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lWarningNbrAutoSplit.AutoSize = true;
			this.lWarningNbrAutoSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lWarningNbrAutoSplit.ForeColor = System.Drawing.Color.Red;
			this.lWarningNbrAutoSplit.Location = new System.Drawing.Point(3, 3);
			this.lWarningNbrAutoSplit.Name = "lWarningNbrAutoSplit";
			this.lWarningNbrAutoSplit.Size = new System.Drawing.Size(238, 13);
			this.lWarningNbrAutoSplit.TabIndex = 23;
			this.lWarningNbrAutoSplit.Text = "Enabled autosplit count: 44    Segment count: 55";
			this.lWarningNbrAutoSplit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabTools
			// 
			this.tabTools.AutoScroll = true;
			this.tabTools.BackColor = System.Drawing.SystemColors.Control;
			this.tabTools.Controls.Add(this.tlpTools);
			this.tabTools.Location = new System.Drawing.Point(4, 22);
			this.tabTools.Name = "tabTools";
			this.tabTools.Padding = new System.Windows.Forms.Padding(3);
			this.tabTools.Size = new System.Drawing.Size(456, 370);
			this.tabTools.TabIndex = 3;
			this.tabTools.Text = "Tools";
			// 
			// tlpTools
			// 
			this.tlpTools.AutoSize = true;
			this.tlpTools.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpTools.ColumnCount = 1;
			this.tlpTools.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpTools.Controls.Add(this.gbToolbox, 0, 0);
			this.tlpTools.Controls.Add(this.groupBox1, 0, 1);
			this.tlpTools.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpTools.Location = new System.Drawing.Point(3, 3);
			this.tlpTools.Name = "tlpTools";
			this.tlpTools.RowCount = 2;
			this.tlpTools.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpTools.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpTools.Size = new System.Drawing.Size(450, 218);
			this.tlpTools.TabIndex = 0;
			// 
			// gbToolbox
			// 
			this.gbToolbox.AutoSize = true;
			this.gbToolbox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbToolbox.Controls.Add(this.flpToolbox);
			this.gbToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbToolbox.Location = new System.Drawing.Point(3, 3);
			this.gbToolbox.Name = "gbToolbox";
			this.gbToolbox.Size = new System.Drawing.Size(444, 48);
			this.gbToolbox.TabIndex = 0;
			this.gbToolbox.TabStop = false;
			this.gbToolbox.Text = "Toolbox";
			// 
			// flpToolbox
			// 
			this.flpToolbox.AutoSize = true;
			this.flpToolbox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flpToolbox.Controls.Add(this.btnLaunchRamWatch);
			this.flpToolbox.Controls.Add(this.btnLaunchLoadScreenLog);
			this.flpToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpToolbox.Location = new System.Drawing.Point(3, 16);
			this.flpToolbox.Name = "flpToolbox";
			this.flpToolbox.Size = new System.Drawing.Size(438, 29);
			this.flpToolbox.TabIndex = 0;
			// 
			// btnLaunchRamWatch
			// 
			this.btnLaunchRamWatch.AutoSize = true;
			this.btnLaunchRamWatch.Location = new System.Drawing.Point(3, 3);
			this.btnLaunchRamWatch.Name = "btnLaunchRamWatch";
			this.btnLaunchRamWatch.Size = new System.Drawing.Size(76, 23);
			this.btnLaunchRamWatch.TabIndex = 0;
			this.btnLaunchRamWatch.Text = "RAM Watch";
			this.btnLaunchRamWatch.UseVisualStyleBackColor = true;
			this.btnLaunchRamWatch.Click += new System.EventHandler(this.btnLaunchRamWatch_Click);
			// 
			// btnLaunchLoadScreenLog
			// 
			this.btnLaunchLoadScreenLog.AutoSize = true;
			this.btnLaunchLoadScreenLog.Location = new System.Drawing.Point(85, 3);
			this.btnLaunchLoadScreenLog.Name = "btnLaunchLoadScreenLog";
			this.btnLaunchLoadScreenLog.Size = new System.Drawing.Size(96, 23);
			this.btnLaunchLoadScreenLog.TabIndex = 1;
			this.btnLaunchLoadScreenLog.Text = "LoadScreen Log";
			this.btnLaunchLoadScreenLog.UseVisualStyleBackColor = true;
			this.btnLaunchLoadScreenLog.Click += new System.EventHandler(this.btnLaunchLoadScreenLog_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add(this.tlpToolSettings);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 57);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(444, 158);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tools settings";
			// 
			// tlpToolSettings
			// 
			this.tlpToolSettings.AutoSize = true;
			this.tlpToolSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpToolSettings.ColumnCount = 1;
			this.tlpToolSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpToolSettings.Controls.Add(this.gbRamWatch, 0, 0);
			this.tlpToolSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpToolSettings.Location = new System.Drawing.Point(3, 16);
			this.tlpToolSettings.Name = "tlpToolSettings";
			this.tlpToolSettings.RowCount = 2;
			this.tlpToolSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpToolSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpToolSettings.Size = new System.Drawing.Size(438, 139);
			this.tlpToolSettings.TabIndex = 0;
			// 
			// gbRamWatch
			// 
			this.gbRamWatch.AutoSize = true;
			this.gbRamWatch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbRamWatch.Controls.Add(this.tlpRamWatchAddr);
			this.gbRamWatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbRamWatch.Location = new System.Drawing.Point(3, 3);
			this.gbRamWatch.Name = "gbRamWatch";
			this.gbRamWatch.Size = new System.Drawing.Size(432, 133);
			this.gbRamWatch.TabIndex = 0;
			this.gbRamWatch.TabStop = false;
			this.gbRamWatch.Text = "RAM Watch";
			// 
			// tlpRamWatchAddr
			// 
			this.tlpRamWatchAddr.AutoSize = true;
			this.tlpRamWatchAddr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpRamWatchAddr.ColumnCount = 2;
			this.tlpRamWatchAddr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpRamWatchAddr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpRamWatchAddr.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpRamWatchAddr.Controls.Add(this.tlpListBtn, 0, 0);
			this.tlpRamWatchAddr.Controls.Add(this.lstAddresses, 1, 0);
			this.tlpRamWatchAddr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpRamWatchAddr.Location = new System.Drawing.Point(3, 16);
			this.tlpRamWatchAddr.Name = "tlpRamWatchAddr";
			this.tlpRamWatchAddr.RowCount = 1;
			this.tlpRamWatchAddr.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpRamWatchAddr.Size = new System.Drawing.Size(426, 114);
			this.tlpRamWatchAddr.TabIndex = 6;
			// 
			// tlpListBtn
			// 
			this.tlpListBtn.AutoScroll = true;
			this.tlpListBtn.AutoSize = true;
			this.tlpListBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpListBtn.ColumnCount = 1;
			this.tlpListBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpListBtn.Controls.Add(this.btnMoveDownAddr, 0, 4);
			this.tlpListBtn.Controls.Add(this.btnMoveUpAddr, 0, 3);
			this.tlpListBtn.Controls.Add(this.btnAddAddr, 0, 0);
			this.tlpListBtn.Controls.Add(this.btnRemoveAddr, 0, 1);
			this.tlpListBtn.Location = new System.Drawing.Point(0, 0);
			this.tlpListBtn.Margin = new System.Windows.Forms.Padding(0);
			this.tlpListBtn.Name = "tlpListBtn";
			this.tlpListBtn.RowCount = 5;
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpListBtn.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpListBtn.Size = new System.Drawing.Size(30, 111);
			this.tlpListBtn.TabIndex = 1;
			// 
			// btnMoveDownAddr
			// 
			this.btnMoveDownAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDownAddr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnMoveDownAddr.FlatAppearance.BorderSize = 0;
			this.btnMoveDownAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMoveDownAddr.Location = new System.Drawing.Point(3, 84);
			this.btnMoveDownAddr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnMoveDownAddr.Name = "btnMoveDownAddr";
			this.btnMoveDownAddr.Size = new System.Drawing.Size(24, 24);
			this.btnMoveDownAddr.TabIndex = 3;
			this.btnMoveDownAddr.Text = "v";
			this.btnMoveDownAddr.UseVisualStyleBackColor = true;
			this.btnMoveDownAddr.Click += new System.EventHandler(this.btnMoveDownAddr_Click);
			// 
			// btnMoveUpAddr
			// 
			this.btnMoveUpAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUpAddr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnMoveUpAddr.FlatAppearance.BorderSize = 0;
			this.btnMoveUpAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMoveUpAddr.Location = new System.Drawing.Point(3, 57);
			this.btnMoveUpAddr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnMoveUpAddr.Name = "btnMoveUpAddr";
			this.btnMoveUpAddr.Size = new System.Drawing.Size(24, 24);
			this.btnMoveUpAddr.TabIndex = 2;
			this.btnMoveUpAddr.Text = "^";
			this.btnMoveUpAddr.UseVisualStyleBackColor = true;
			this.btnMoveUpAddr.Click += new System.EventHandler(this.btnMoveUpAddr_Click);
			// 
			// btnAddAddr
			// 
			this.btnAddAddr.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAddAddr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnAddAddr.FlatAppearance.BorderSize = 0;
			this.btnAddAddr.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
			this.btnAddAddr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
			this.btnAddAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddAddr.Location = new System.Drawing.Point(3, 0);
			this.btnAddAddr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnAddAddr.Name = "btnAddAddr";
			this.btnAddAddr.Size = new System.Drawing.Size(24, 24);
			this.btnAddAddr.TabIndex = 0;
			this.btnAddAddr.Text = "+";
			this.btnAddAddr.UseVisualStyleBackColor = true;
			this.btnAddAddr.Click += new System.EventHandler(this.btnAddAddr_Click);
			// 
			// btnRemoveAddr
			// 
			this.btnRemoveAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveAddr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnRemoveAddr.FlatAppearance.BorderSize = 0;
			this.btnRemoveAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemoveAddr.Location = new System.Drawing.Point(3, 27);
			this.btnRemoveAddr.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.btnRemoveAddr.Name = "btnRemoveAddr";
			this.btnRemoveAddr.Size = new System.Drawing.Size(24, 24);
			this.btnRemoveAddr.TabIndex = 1;
			this.btnRemoveAddr.Text = "-";
			this.btnRemoveAddr.UseVisualStyleBackColor = true;
			this.btnRemoveAddr.Click += new System.EventHandler(this.btnRemoveAddr_Click);
			// 
			// lstAddresses
			// 
			this.lstAddresses.Dock = System.Windows.Forms.DockStyle.Top;
			this.lstAddresses.FormattingEnabled = true;
			this.lstAddresses.Location = new System.Drawing.Point(30, 3);
			this.lstAddresses.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.lstAddresses.Name = "lstAddresses";
			this.lstAddresses.Size = new System.Drawing.Size(393, 108);
			this.lstAddresses.TabIndex = 0;
			// 
			// tabBearCart
			// 
			this.tabBearCart.BackColor = System.Drawing.SystemColors.Control;
			this.tabBearCart.Controls.Add(this.tlpBearCart);
			this.tabBearCart.Location = new System.Drawing.Point(4, 22);
			this.tabBearCart.Name = "tabBearCart";
			this.tabBearCart.Padding = new System.Windows.Forms.Padding(3);
			this.tabBearCart.Size = new System.Drawing.Size(456, 370);
			this.tabBearCart.TabIndex = 2;
			this.tabBearCart.Text = "Bear Cart";
			// 
			// tlpBearCart
			// 
			this.tlpBearCart.AutoSize = true;
			this.tlpBearCart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpBearCart.ColumnCount = 1;
			this.tlpBearCart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
			this.tlpBearCart.Controls.Add(this.chkBearCartPBNotification, 0, 1);
			this.tlpBearCart.Controls.Add(this.gbBearCartSound, 0, 2);
			this.tlpBearCart.Controls.Add(this.lBearCartPB, 0, 0);
			this.tlpBearCart.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlpBearCart.Location = new System.Drawing.Point(3, 3);
			this.tlpBearCart.Name = "tlpBearCart";
			this.tlpBearCart.RowCount = 3;
			this.tlpBearCart.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpBearCart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpBearCart.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpBearCart.Size = new System.Drawing.Size(450, 196);
			this.tlpBearCart.TabIndex = 0;
			// 
			// chkBearCartPBNotification
			// 
			this.chkBearCartPBNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.chkBearCartPBNotification.AutoSize = true;
			this.chkBearCartPBNotification.Checked = true;
			this.chkBearCartPBNotification.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkBearCartPBNotification.Location = new System.Drawing.Point(3, 32);
			this.chkBearCartPBNotification.Name = "chkBearCartPBNotification";
			this.chkBearCartPBNotification.Size = new System.Drawing.Size(444, 17);
			this.chkBearCartPBNotification.TabIndex = 0;
			this.chkBearCartPBNotification.Text = "Warn about new Personal Best when resetting";
			this.chkBearCartPBNotification.UseVisualStyleBackColor = true;
			// 
			// gbBearCartSound
			// 
			this.gbBearCartSound.AutoSize = true;
			this.gbBearCartSound.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbBearCartSound.Controls.Add(this.tlpSound);
			this.gbBearCartSound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbBearCartSound.Location = new System.Drawing.Point(3, 58);
			this.gbBearCartSound.Name = "gbBearCartSound";
			this.gbBearCartSound.Size = new System.Drawing.Size(444, 135);
			this.gbBearCartSound.TabIndex = 1;
			this.gbBearCartSound.TabStop = false;
			this.gbBearCartSound.Text = "Sound";
			// 
			// tlpSound
			// 
			this.tlpSound.AutoSize = true;
			this.tlpSound.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tlpSound.ColumnCount = 3;
			this.tlpSound.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tlpSound.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpSound.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpSound.Controls.Add(this.lVolume, 0, 3);
			this.tlpSound.Controls.Add(this.tbGeneralVolume, 1, 3);
			this.tlpSound.Controls.Add(this.chkPlayBearCartSound, 0, 0);
			this.tlpSound.Controls.Add(this.btnBrowseBearCartSound, 2, 2);
			this.tlpSound.Controls.Add(this.lSound, 0, 2);
			this.tlpSound.Controls.Add(this.txtBearCartSoundPath, 1, 2);
			this.tlpSound.Controls.Add(this.chkPlayBearCartSoundOnlyOnPB, 0, 1);
			this.tlpSound.Controls.Add(this.btnBearCartSoundTest, 2, 1);
			this.tlpSound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpSound.Location = new System.Drawing.Point(3, 16);
			this.tlpSound.Name = "tlpSound";
			this.tlpSound.RowCount = 4;
			this.tlpSound.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpSound.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpSound.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpSound.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tlpSound.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpSound.Size = new System.Drawing.Size(438, 116);
			this.tlpSound.TabIndex = 0;
			// 
			// lVolume
			// 
			this.lVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lVolume.AutoSize = true;
			this.lVolume.Location = new System.Drawing.Point(3, 95);
			this.lVolume.Name = "lVolume";
			this.lVolume.Size = new System.Drawing.Size(114, 13);
			this.lVolume.TabIndex = 10;
			this.lVolume.Text = "Volume:";
			// 
			// tbGeneralVolume
			// 
			this.tlpSound.SetColumnSpan(this.tbGeneralVolume, 2);
			this.tbGeneralVolume.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbGeneralVolume.Location = new System.Drawing.Point(123, 90);
			this.tbGeneralVolume.Maximum = 100;
			this.tbGeneralVolume.Name = "tbGeneralVolume";
			this.tbGeneralVolume.Size = new System.Drawing.Size(312, 23);
			this.tbGeneralVolume.TabIndex = 5;
			this.tbGeneralVolume.TickFrequency = 10;
			this.tbGeneralVolume.Value = 100;
			// 
			// chkPlayBearCartSound
			// 
			this.chkPlayBearCartSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.chkPlayBearCartSound.AutoSize = true;
			this.chkPlayBearCartSound.Checked = true;
			this.chkPlayBearCartSound.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tlpSound.SetColumnSpan(this.chkPlayBearCartSound, 2);
			this.chkPlayBearCartSound.Location = new System.Drawing.Point(3, 6);
			this.chkPlayBearCartSound.Name = "chkPlayBearCartSound";
			this.chkPlayBearCartSound.Size = new System.Drawing.Size(354, 17);
			this.chkPlayBearCartSound.TabIndex = 0;
			this.chkPlayBearCartSound.Text = "Play sound when getting Bear Cart";
			this.chkPlayBearCartSound.UseVisualStyleBackColor = true;
			this.chkPlayBearCartSound.CheckedChanged += new System.EventHandler(this.chkPlayBearCartSound_CheckedChanged);
			// 
			// btnBrowseBearCartSound
			// 
			this.btnBrowseBearCartSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseBearCartSound.AutoSize = true;
			this.btnBrowseBearCartSound.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnBrowseBearCartSound.Location = new System.Drawing.Point(363, 61);
			this.btnBrowseBearCartSound.Name = "btnBrowseBearCartSound";
			this.btnBrowseBearCartSound.Size = new System.Drawing.Size(72, 23);
			this.btnBrowseBearCartSound.TabIndex = 4;
			this.btnBrowseBearCartSound.Text = "Browse...";
			this.btnBrowseBearCartSound.UseVisualStyleBackColor = true;
			this.btnBrowseBearCartSound.Click += new System.EventHandler(this.btnBrowseBearCartSound_Click);
			// 
			// lSound
			// 
			this.lSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lSound.AutoSize = true;
			this.lSound.Location = new System.Drawing.Point(3, 66);
			this.lSound.Name = "lSound";
			this.lSound.Size = new System.Drawing.Size(114, 13);
			this.lSound.TabIndex = 3;
			this.lSound.Text = "Sound:";
			this.lSound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtBearCartSoundPath
			// 
			this.txtBearCartSoundPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBearCartSoundPath.Location = new System.Drawing.Point(123, 62);
			this.txtBearCartSoundPath.Name = "txtBearCartSoundPath";
			this.txtBearCartSoundPath.Size = new System.Drawing.Size(234, 20);
			this.txtBearCartSoundPath.TabIndex = 3;
			// 
			// chkPlayBearCartSoundOnlyOnPB
			// 
			this.chkPlayBearCartSoundOnlyOnPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.chkPlayBearCartSoundOnlyOnPB.AutoSize = true;
			this.tlpSound.SetColumnSpan(this.chkPlayBearCartSoundOnlyOnPB, 2);
			this.chkPlayBearCartSoundOnlyOnPB.Location = new System.Drawing.Point(3, 35);
			this.chkPlayBearCartSoundOnlyOnPB.Name = "chkPlayBearCartSoundOnlyOnPB";
			this.chkPlayBearCartSoundOnlyOnPB.Size = new System.Drawing.Size(354, 17);
			this.chkPlayBearCartSoundOnlyOnPB.TabIndex = 1;
			this.chkPlayBearCartSoundOnlyOnPB.Text = "Play only on Personal Bests";
			this.chkPlayBearCartSoundOnlyOnPB.UseVisualStyleBackColor = true;
			// 
			// btnBearCartSoundTest
			// 
			this.btnBearCartSoundTest.AutoSize = true;
			this.btnBearCartSoundTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnBearCartSoundTest.Location = new System.Drawing.Point(363, 32);
			this.btnBearCartSoundTest.Name = "btnBearCartSoundTest";
			this.btnBearCartSoundTest.Size = new System.Drawing.Size(72, 23);
			this.btnBearCartSoundTest.TabIndex = 2;
			this.btnBearCartSoundTest.Text = "Sound Test";
			this.btnBearCartSoundTest.UseVisualStyleBackColor = true;
			this.btnBearCartSoundTest.Click += new System.EventHandler(this.chkBearCartSoundTest_Click);
			// 
			// lBearCartPB
			// 
			this.lBearCartPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lBearCartPB.AutoSize = true;
			this.lBearCartPB.Location = new System.Drawing.Point(3, 0);
			this.lBearCartPB.Name = "lBearCartPB";
			this.lBearCartPB.Size = new System.Drawing.Size(444, 26);
			this.lBearCartPB.TabIndex = 7;
			this.lBearCartPB.Text = "Personal Best:\r\nGame Time: 00:42.00, Real Time: 01:23.456";
			this.lBearCartPB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// gbGeneral
			// 
			this.gbGeneral.AutoSize = true;
			this.gbGeneral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.gbGeneral.Controls.Add(this.flpGeneral);
			this.gbGeneral.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbGeneral.Location = new System.Drawing.Point(3, 3);
			this.gbGeneral.Name = "gbGeneral";
			this.gbGeneral.Size = new System.Drawing.Size(464, 88);
			this.gbGeneral.TabIndex = 0;
			this.gbGeneral.TabStop = false;
			this.gbGeneral.Text = "General";
			// 
			// flpGeneral
			// 
			this.flpGeneral.AutoSize = true;
			this.flpGeneral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flpGeneral.Controls.Add(this.chkAutoStart);
			this.flpGeneral.Controls.Add(this.chkAutoReset);
			this.flpGeneral.Controls.Add(this.chkAutoUpdatePresets);
			this.flpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpGeneral.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flpGeneral.Location = new System.Drawing.Point(3, 16);
			this.flpGeneral.Name = "flpGeneral";
			this.flpGeneral.Size = new System.Drawing.Size(458, 69);
			this.flpGeneral.TabIndex = 0;
			// 
			// chkAutoStart
			// 
			this.chkAutoStart.AutoSize = true;
			this.chkAutoStart.Checked = true;
			this.chkAutoStart.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoStart.Location = new System.Drawing.Point(3, 3);
			this.chkAutoStart.Name = "chkAutoStart";
			this.chkAutoStart.Size = new System.Drawing.Size(121, 17);
			this.chkAutoStart.TabIndex = 0;
			this.chkAutoStart.Text = "Automatic timer start";
			this.chkAutoStart.UseVisualStyleBackColor = true;
			// 
			// chkAutoReset
			// 
			this.chkAutoReset.AutoSize = true;
			this.chkAutoReset.Checked = true;
			this.chkAutoReset.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoReset.Location = new System.Drawing.Point(3, 26);
			this.chkAutoReset.Name = "chkAutoReset";
			this.chkAutoReset.Size = new System.Drawing.Size(124, 17);
			this.chkAutoReset.TabIndex = 1;
			this.chkAutoReset.Text = "Automatic timer reset";
			this.chkAutoReset.UseVisualStyleBackColor = true;
			// 
			// chkAutoUpdatePresets
			// 
			this.chkAutoUpdatePresets.AutoSize = true;
			this.chkAutoUpdatePresets.Checked = true;
			this.chkAutoUpdatePresets.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoUpdatePresets.Location = new System.Drawing.Point(3, 49);
			this.chkAutoUpdatePresets.Name = "chkAutoUpdatePresets";
			this.chkAutoUpdatePresets.Size = new System.Drawing.Size(178, 17);
			this.chkAutoUpdatePresets.TabIndex = 2;
			this.chkAutoUpdatePresets.Text = "Update the presets list at launch";
			this.chkAutoUpdatePresets.UseVisualStyleBackColor = true;
			// 
			// SkyrimSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tlpMain);
			this.Name = "SkyrimSettings";
			this.Padding = new System.Windows.Forms.Padding(7);
			this.Size = new System.Drawing.Size(484, 510);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabAnyPercent.ResumeLayout(false);
			this.tlpAutoSplits.ResumeLayout(false);
			this.tlpAutoSplits.PerformLayout();
			this.tlpAutosplitPreset.ResumeLayout(false);
			this.tlpAutosplitPreset.PerformLayout();
			this.tlpPresetsList.ResumeLayout(false);
			this.tlpPresetsList.PerformLayout();
			this.tlpAutosplits1stRow.ResumeLayout(false);
			this.tlpAutosplits1stRow.PerformLayout();
			this.tabTools.ResumeLayout(false);
			this.tabTools.PerformLayout();
			this.tlpTools.ResumeLayout(false);
			this.tlpTools.PerformLayout();
			this.gbToolbox.ResumeLayout(false);
			this.gbToolbox.PerformLayout();
			this.flpToolbox.ResumeLayout(false);
			this.flpToolbox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tlpToolSettings.ResumeLayout(false);
			this.tlpToolSettings.PerformLayout();
			this.gbRamWatch.ResumeLayout(false);
			this.gbRamWatch.PerformLayout();
			this.tlpRamWatchAddr.ResumeLayout(false);
			this.tlpRamWatchAddr.PerformLayout();
			this.tlpListBtn.ResumeLayout(false);
			this.tabBearCart.ResumeLayout(false);
			this.tabBearCart.PerformLayout();
			this.tlpBearCart.ResumeLayout(false);
			this.tlpBearCart.PerformLayout();
			this.gbBearCartSound.ResumeLayout(false);
			this.gbBearCartSound.PerformLayout();
			this.tlpSound.ResumeLayout(false);
			this.tlpSound.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbGeneralVolume)).EndInit();
			this.gbGeneral.ResumeLayout(false);
			this.gbGeneral.PerformLayout();
			this.flpGeneral.ResumeLayout(false);
			this.flpGeneral.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.CheckBox chkAutoReset;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAnyPercent;
        private System.Windows.Forms.TableLayoutPanel tlpAutoSplits;
        private System.Windows.Forms.TabPage tabBearCart;
        private System.Windows.Forms.LinkLabel llCheckAll;
        private System.Windows.Forms.TableLayoutPanel tlpBearCart;
        private System.Windows.Forms.FlowLayoutPanel flpGeneral;
        private System.Windows.Forms.CheckBox chkBearCartPBNotification;
        private System.Windows.Forms.Label lWarningNbrAutoSplit;
        private System.Windows.Forms.GroupBox gbBearCartSound;
        private System.Windows.Forms.TableLayoutPanel tlpSound;
        private System.Windows.Forms.CheckBox chkPlayBearCartSound;
        private System.Windows.Forms.Button btnBrowseBearCartSound;
        private System.Windows.Forms.Label lSound;
        private System.Windows.Forms.Button btnBearCartSoundTest;
        private System.Windows.Forms.TextBox txtBearCartSoundPath;
        private System.Windows.Forms.Label lBearCartPB;
        private System.Windows.Forms.CheckBox chkPlayBearCartSoundOnlyOnPB;
        private System.Windows.Forms.Label lVolume;
        private System.Windows.Forms.TrackBar tbGeneralVolume;
		private System.Windows.Forms.TableLayoutPanel tlpAutosplitPreset;
		private System.Windows.Forms.Button btnCustomize;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbPreset;
		private System.Windows.Forms.CheckedListBox chklbSplits;
		private System.Windows.Forms.Button btnOther;
		private System.Windows.Forms.TableLayoutPanel tlpAutosplits1stRow;
		private System.Windows.Forms.TableLayoutPanel tlpPresetsList;
		private System.Windows.Forms.TabPage tabTools;
		private System.Windows.Forms.TableLayoutPanel tlpTools;
		private System.Windows.Forms.GroupBox gbRamWatch;
		private System.Windows.Forms.Button btnLaunchRamWatch;
		private System.Windows.Forms.TableLayoutPanel tlpRamWatchAddr;
		private System.Windows.Forms.TableLayoutPanel tlpListBtn;
		private System.Windows.Forms.Button btnAddAddr;
		private System.Windows.Forms.Button btnRemoveAddr;
		private System.Windows.Forms.ListBox lstAddresses;
		private System.Windows.Forms.GroupBox gbToolbox;
		private System.Windows.Forms.FlowLayoutPanel flpToolbox;
		private System.Windows.Forms.Button btnLaunchLoadScreenLog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tlpToolSettings;
		private System.Windows.Forms.Button btnMoveUpAddr;
		private System.Windows.Forms.Button btnMoveDownAddr;
		private System.Windows.Forms.CheckBox chkAutoUpdatePresets;
	}
}
