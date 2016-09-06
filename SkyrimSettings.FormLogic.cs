using LiveSplit.AutoSplitting;
using LiveSplit.AutoSplitting.Editors;
using LiveSplit.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.Skyrim
{
	partial class SkyrimSettings
	{
		ContextMenuStrip _cmsOtherBtn;
		bool _autoCloseRunEditor = false;

		void InitializeFormLogic()
		{
			chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoReset.DataBindings.Add("Checked", this, "AutoReset", false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoUpdatePresets.DataBindings.Add("Checked", this, "AutoUpdatePresets", false, DataSourceUpdateMode.OnPropertyChanged);
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

			_cmsOtherBtn = new ContextMenuStrip();
			_cmsOtherBtn.Items.Add("Update presets list", null, (s,e) => UpdatePresets());
			_cmsOtherBtn.Items.Add("Generate segments from preset", null, cmsOtherBtn_GenerateSegments);
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
				lBearCartPB.Text = string.Format("Personal Best:\n Game Time: {0}, Real Time: {1}",
					BearCartPB.GameTime.Value.ToString(@"mm\:ss\.fff"), BearCartPB.RealTime.Value.ToString(@"mm\:ss\.fff"));
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

			disableNbrSplitCheck = true;
			for (int i = 0; i < AutoSplitList.Count; i++)
			{
				var split = AutoSplitList[i];
				chklbSplits.Items.Add(split);
				chklbSplits.SetItemChecked(i, split.Enabled);
			}
			disableNbrSplitCheck = false;
			CheckNbrAutoSplits();
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

		void btnOther_Click(object sender, EventArgs e)
		{
			_cmsOtherBtn.Show(Cursor.Position);
		}

		void cmsOtherBtn_GenerateSegments(object sender, EventArgs e)
		{
			if (_state.CurrentPhase == TimerPhase.Running)
			{
				MessageBox.Show(this, "This cannot be done while the timer is running.",
					"Generate segments", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var autosplitNames = Presets.First(p => p.Name == Preset)
				.Select(split => split.Name)
				.ToList();

			if (autosplitNames.Count == 0)
			{
				MessageBox.Show(this, "Your autosplit list is empty.", "Generate segments",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			int i = -1;

			if (_state.Run.Count == autosplitNames.Count)
			{
				// find the first index that doesn't match
				i = 0;
				while (i < _state.Run.Count)
				{
					if (_state.Run[i].Name != autosplitNames[i])
						break;
					i++;
				}
			}

			if (i == _state.Run.Count)
			{
				MessageBox.Show(this, "Your segments already match your autosplits.",
					"Generate segments", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			else if (i == _state.Run.Count - 1) // everything matched except the last one
			{
				_state.Run[i].Name = autosplitNames[i];
			}
			else
			{
				var result = MessageBox.Show(this, "All your existing segments except the last one will be deleted.\n\nContinue?",
					"Generate segments", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result != DialogResult.Yes)
					return;

				// remove everything except last split
				while (_state.Run.Count > 1)
					_state.Run.RemoveAt(0);

				_state.Run[0].Name = autosplitNames[autosplitNames.Count - 1];

				for (int j = 0; j < autosplitNames.Count - 1; j++)
				{
					var newSegment = new Segment(autosplitNames[j]);

					_state.Run.ImportBestSegment(j);
					var maxIndex = _state.Run.AttemptHistory.Select(x => x.Index).DefaultIfEmpty(0).Max();
					for (var x = _state.Run.GetMinSegmentHistoryIndex(); x <= maxIndex; x++)
						newSegment.SegmentHistory.Add(x, default(Time));

					_state.Run.Insert(j, newSegment);
				}

				_state.Run.FixSplits();
			}

			_state.Run.HasChanged = true;
			_state.Form.Invalidate();
			CheckNbrAutoSplits();

			// force close the splits editor because editing the segments messes it up
			var editor = _state.Form.OwnedForms.FirstOrDefault(f => f.Text == "Splits Editor");
			if (!_autoCloseRunEditor && editor != null)
			{
				ParentForm.FormClosed += (s, ea) =>
				{
					if (ParentForm.DialogResult == DialogResult.OK)
						editor.AcceptButton.PerformClick();
					else
						editor.Close();

					_autoCloseRunEditor = false;
				};

				_autoCloseRunEditor = true;
			}

			MessageBox.Show(this, "Your segments have been updated.\nYou might want to reset the Best Segment time of your last split.",
				"Generate segments", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
				lWarningNbrAutoSplit.Text = $"Segment count: {_state.Run.Count}    Enabled autosplit count: {enabledSplitsCount}";
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