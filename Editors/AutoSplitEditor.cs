using LiveSplit.AutoSplitting.Variables;
using LiveSplit.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Action = LiveSplit.AutoSplitting.Variables.Action;

namespace LiveSplit.AutoSplitting.Editors
{
	public partial class AutoSplitEditor : Form
	{
		public AutoSplit EditedAutoSplit { get; private set; }

		readonly AutoSplitEnv _env;

		public AutoSplitEditor(AutoSplitEnv env, AutoSplit source = null)
		{
			InitializeComponent();

			MaximumSize = new Size(Size.Width, Screen.AllScreens.Max(s => s.WorkingArea.Height));
			btnAdd.Text = btnRemove.Text = string.Empty;
			btnAdd.BackgroundImage = Resources.Add;
			btnRemove.BackgroundImage = Resources.Remove;
			foreach (var btn in tlpListBtn.Controls.OfType<Button>())
			{
				btn.FlatAppearance.MouseOverBackColor = Color.LightGray;
				btn.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
			}
			lstVariables.Height = 0; //hack to fix the control not filling its parent properly

			_env = env;
			EditedAutoSplit = source?.Clone(_env) ?? new AutoSplit();

			txtName.DataBindings.Add("Text", EditedAutoSplit, "Name");
			RefreshEvents();
			cbEvent.DataBindings.Add("SelectedItem", EditedAutoSplit, "Event");
			lstVariables.Format += (s, e) => e.Value = $"{((Variable)e.ListItem).ToString(_env)}";
			lstVariables.DataSource = new BindingList<Variable>(EditedAutoSplit.Variables);
			lstVariables.SelectedIndex = -1;
#if DEBUG
			label2.Visible = cbEvent.Visible = true;
#endif
		}

		void RefreshEvents()
		{
			cbEvent.DataSource = GetAvailableEvents(EditedAutoSplit);
		}

		IEnumerable<GameEvent> GetAvailableEvents(AutoSplit split)
		{
			var events = split.Variables.GetRestrictedEvents() != null
				? _env.Events.Intersect(split.Variables.GetRestrictedEvents()).ToArray()
				: _env.Events;
			return events.OrderBy(e => e).ToArray();
		}

		bool IsValidAutoSplit() => !string.IsNullOrEmpty(txtName.Text) && lstVariables.Items.Count > 0;

		public static AutoSplit ShowEditor(AutoSplitEnv env, AutoSplit source = null)
		{
			using (var form = new AutoSplitEditor(env, source))
			{
				return form.ShowDialog() != DialogResult.Cancel
					? form.EditedAutoSplit
					: source;
			}
		}

		void btnAdd_Click(object sender, EventArgs e)
		{
			var env = _env.Clone();
			env.Events = EditedAutoSplit.Variables.GetRestrictedEvents() ?? _env.Events;
			var newVariable = VariableEditorPicker.ShowEditor(env);
			if (newVariable != null)
			{
				var list = (BindingList<Variable>)lstVariables.DataSource;
				var attributes = newVariable.GetType().GetCustomAttributes(typeof(VariableDescriptorAttribute), true);
                var varDescriptor = attributes.Length > 0 ? (VariableDescriptorAttribute)attributes[0] : null;
				if (varDescriptor == null || varDescriptor.AllowMultiple || !list.Any(v => v.GetType() == newVariable.GetType()))
				{
					if (EditedAutoSplit.Variables.AreCompatible(newVariable))
					{
						var selectedIndex = lstVariables.SelectedIndex;
						list.Add(newVariable);
						if (selectedIndex == -1)
							lstVariables.SelectedIndex = -1;
						RefreshEvents();
					}
					else
						MessageBox.Show($"The new condition could not be added because one or more existing conditions are incompatible with it.",
							"Condition could not be added",
							MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
					MessageBox.Show($"There can't be multiple {newVariable.GetType().Name} conditions in an autosplit.",
						"Condition could not be added",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		void btnRemove_Click(object sender, EventArgs e)
		{
			if (lstVariables.SelectedIndex == -1)
				return;
			var list = (BindingList<Variable>)lstVariables.DataSource;
			list.RemoveAt(lstVariables.SelectedIndex);
			RefreshEvents();
		}

		void btnOK_Click(object sender, EventArgs e)
		{
			if (!IsValidAutoSplit())
			{
				MessageBox.Show("Invalid Autosplit properties.", "AutoSplit Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		void lstVariables_DoubleClick(object sender, EventArgs e)
		{
			if (lstVariables.SelectedIndex == -1)
				return;

			var splitIndex = lstVariables.SelectedIndex;
			var selectedVar = (Variable)lstVariables.SelectedItem;

			if (_env.GetEditor(selectedVar.GetType()) != null)
			{
				var varList = ((BindingList<Variable>)lstVariables.DataSource);
				var events = (selectedVar is Action) == false
					? varList.GetRestrictedEvents()
					: varList.Where(v => (v is Action) == false).GetRestrictedEvents();
				var env = _env.Clone();
				env.Events = events ?? _env.Events;

				var newVariable = VariableEditor.ShowEditor(env, selectedVar);
				if (varList.Where(v => v != selectedVar).AreCompatible(newVariable))
				{
					varList[splitIndex] = newVariable;
					RefreshEvents();
				}
				else
					MessageBox.Show($"The condition could not be edited because one or more existing conditions are incompatible with it. Changes were reverted.",
						"Condition could not be edited",
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
				MessageBox.Show("No editor found for this condition type.", "AutoSplit Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}
}
