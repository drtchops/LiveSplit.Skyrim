using LiveSplit.AutoSplitting;
using LiveSplit.AutoSplitting.Editors;
using LiveSplit.Skyrim.AutoSplitData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.Skyrim
{
	partial class SkyrimSettings
	{
		void InitializeFormLogic()
		{
			chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoReset.DataBindings.Add("Checked", this, "AutoReset", false, DataSourceUpdateMode.OnPropertyChanged);
			chkBearCartPBNotification.DataBindings.Add("Checked", this, "BearCartPBNotification", false, DataSourceUpdateMode.OnPropertyChanged);
			chkPlayBearCartSound.DataBindings.Add("Checked", this, "PlayBearCartSound", false, DataSourceUpdateMode.OnPropertyChanged);
			txtBearCartSoundPath.DataBindings.Add("Text", this, "BearCartSoundPath", false, DataSourceUpdateMode.OnPropertyChanged);
			chkPlayBearCartSoundOnlyOnPB.DataBindings.Add("Checked", this, "PlayBearCartSoundOnlyOnPB", false, DataSourceUpdateMode.OnPropertyChanged);
			if (_component.MediaPlayer != null)
				tbGeneralVolume.DataBindings.Add("Value", _component.MediaPlayer, "GeneralVolume", false, DataSourceUpdateMode.OnPropertyChanged);

			Load += Settings_OnLoad;
			Disposed += SkyrimSettings_Disposed;

			chklbSplits.DisplayMember = "Name";
			chklbSplits.ItemCheck += (s, e) =>
			{
				var split = AutoSplitList[e.Index];
				split.Enabled = e.NewValue == CheckState.Checked;

				if (!disableNbrSplitCheck) //checking all the checkboxes is very slow otherwise
					CheckNbrAutoSplits();
			};

			cbPreset.DataSource = Presets;
			cbPreset.DisplayMember = "Name";
			cbPreset.SelectedItem = Presets.FirstOrDefault(p => p.Name == Preset) ?? CustomAutosplits;
			cbPreset.SelectionChangeCommitted += CbPreset_SelectionChangeCommitted;
		}

		void Settings_OnLoad(object sender, EventArgs e)
		{
			if (_component.MediaPlayer == null)
			{
				gbBearCartSound.Enabled = false;
				gbBearCartSound.Text = "Sound (NAudio.dll is missing)";
			}

			if (BearCartPB.RealTime != null && BearCartPB.RealTime != new TimeSpan(0))
			{
				lBearCartPB.Text = string.Format("Personal Best:\n Game Time: {0}, Real Time: {1}", BearCartPB.GameTime.Value.ToString(@"mm\:ss\.fff"), BearCartPB.RealTime.Value.ToString(@"mm\:ss\.fff"));
				lBearCartPB.Visible = true;
			}
			else
				lBearCartPB.Visible = false;

			tabControl1.TabPages.Remove(tabBearCart);
			if (!IsBearCartSecret)
				tabControl1.TabPages.Add(tabBearCart);

			CheckNbrAutoSplits();
		}

		void RefreshSplitsListControl()
		{
			var isCustomPreset = ((AutoSplitList)cbPreset.SelectedItem).Name == CustomAutosplits.Name;
			llCheckAll.Enabled = btnCustomize.Enabled = chklbSplits.Enabled = isCustomPreset;

			chklbSplits.Items.Clear();

			for (int i = 0; i < AutoSplitList.Count; i++)
			{
				var split = AutoSplitList[i];
				chklbSplits.Items.Add(split);
				chklbSplits.SetItemChecked(i, split.Enabled);
			}
		}

		void CbPreset_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (cbPreset.SelectedIndex == -1)
				return;

			UsePreset((AutoSplitList)cbPreset.SelectedItem);
		}

		void btnCustomize_Click(object sender, EventArgs e)
		{
			var selectedPreset = Presets.First(p => p.Name == Preset);
			AutoSplitListEditor.ShowEditor(_autoSplitEnv, selectedPreset);
			UsePreset(selectedPreset);
			RefreshSplitsListControl();
		}

		void btnUpdatePresets_Click(object sender, EventArgs e)
		{
			if (!CheckForComponentUpdate())
			{
				btnUpdatePresets.Enabled = false;

				if (DownloadPresetsFile(false) && LoadPresets(false))
					MessageBox.Show("Presets successfully updated.", "Presets updated",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				UsePreset(Presets.FirstOrDefault(l => l.Name == Preset) ?? CustomAutosplits);

				btnUpdatePresets.Enabled = true;
			}
			else
				MessageBox.Show("An update is available for the autosplitter.\nPresets can only be updated with the latest autosplitter version available.",
					"Presets update cancelled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		void llCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (chklbSplits.CheckedItems.Count != chklbSplits.Items.Count)
				SetAllSplitsState(true);
			else
				SetAllSplitsState(false);
		}

		void SetAllSplitsState(bool state)
		{
			disableNbrSplitCheck = true;

			for (int i = 0; i < chklbSplits.Items.Count; i++)
				chklbSplits.SetItemChecked(i, state);

			disableNbrSplitCheck = false;
			CheckNbrAutoSplits();
		}

		void CheckNbrAutoSplits()
		{
			var enabledSplitsCount = AutoSplitList.Count(s => s.Enabled);
			if (enabledSplitsCount != 0 && enabledSplitsCount != _state.Run.Count)
			{
				lWarningNbrAutoSplit.Text = string.Format("Enabled autosplit count: {0}    Segment count: {1}", enabledSplitsCount, _state.Run.Count);
				lWarningNbrAutoSplit.Visible = true;
			}
			else
				lWarningNbrAutoSplit.Visible = false;
		}

		string BrowseForPath(string path)
		{
			var fileDialog = new OpenFileDialog()
			{
				FileName = path ?? "",
				Filter = "Media Files|*.avi;*.mp3;*.wav;*.mid;*.midi;*.mpeg;*.mpg;*.mp4;*.m4a;*.aac;*.m4v;*.mov;*.wmv;|All Files (*.*)|*.*"
			};
			var result = fileDialog.ShowDialog();
			if (result == DialogResult.OK)
				path = fileDialog.FileName;
			return path;
		}

		void chkBearCartSoundTest_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(BearCartSoundPath) || !File.Exists(BearCartSoundPath))
			{
				_component.MediaPlayer?.PlaySound(_component.BearCartDefaultSoundPath);
			}
			else if (File.Exists(BearCartSoundPath))
			{
				_component.MediaPlayer?.PlaySound(BearCartSoundPath);
			}
		}

		void btnBrowseBearCartSound_Click(object sender, EventArgs e)
		{
			txtBearCartSoundPath.Text = BearCartSoundPath = BrowseForPath(BearCartSoundPath);
		}

		void chkPlayBearCartSound_CheckedChanged(object sender, EventArgs e)
		{
			var enable = chkPlayBearCartSound.Checked;

			btnBrowseBearCartSound.Enabled = enable;
			lSound.Enabled = enable;
			txtBearCartSoundPath.Enabled = enable;
			btnBearCartSoundTest.Enabled = enable;
			chkPlayBearCartSoundOnlyOnPB.Enabled = enable;
			lVolume.Enabled = enable;
			tbGeneralVolume.Enabled = enable;

			if (!enable)
				_component.MediaPlayer?.Stop();
		}

		void SkyrimSettings_Disposed(object sender, EventArgs e)
		{
			SkyrimSettings_ToolsTab_Dispose();
		}
	}
}