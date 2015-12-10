using LiveSplit.AutoSplitting.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.AutoSplitting.Editors
{
	public partial class VariableEditorPicker : Form
	{
		public Variable EditedVariable { get; private set; }

		readonly AutoSplitEnv _env;

		public VariableEditorPicker(AutoSplitEnv env, Variable variable = null)
		{
			InitializeComponent();

			_env = env;
			EditedVariable = variable?.Clone(_env);

			var dictionary = env.VariableTypes.Keys
				.Except(env.HiddenVariables)
				.Where(t => env.VariableTypes[t] != null)
				.ToDictionary(t =>
				{
					var tName = t.Name;
					if (tName.Contains("`"))
						tName = tName.Remove(tName.IndexOf("`"));
					return tName;
				});
			cbVarTypes.DataSource = new BindingSource(dictionary.OrderBy(p => p.Key), null);
			cbVarTypes.DisplayMember = "Key";
			cbVarTypes.ValueMember = "Value";
			cbVarTypes.SelectedValueChanged += cbVarTypes_SelectedValueChanged;
            cbVarTypes.SelectedItem = dictionary.First(p => p.Value == env.DefaultVariableType);
		}

		public static Variable ShowEditor(AutoSplitEnv env, Variable variable = null)
		{
			using (var form = new VariableEditorPicker(env, variable))
			{
				return form.ShowDialog() != DialogResult.Cancel
					? form.EditedVariable
					: variable;
			}
		}

		void btnOK_Click(object sender, EventArgs e)
		{
			if (cbVarTypes.SelectedIndex == -1)
				return;

			Visible = false;
			EditedVariable = VariableEditor.ShowEditor(_env, (Type)cbVarTypes.SelectedValue);
			if (EditedVariable != null)
				DialogResult = DialogResult.OK;
			else
				Show();
		}

		void cbVarTypes_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbVarTypes.SelectedIndex == -1)
				return;

			var selectedPair = (Type)cbVarTypes.SelectedValue;
			var attributes = selectedPair.GetCustomAttributes(typeof(VariableDescriptorAttribute), false)
				.Cast<VariableDescriptorAttribute>()
				.ToArray();
			var attr = attributes.Length > 0 ? attributes[0] : null;
			lDescription.Text = attr?.Description ?? "(none)";
			lDescription.Enabled = attr?.Description != null;
		}
	}
}
