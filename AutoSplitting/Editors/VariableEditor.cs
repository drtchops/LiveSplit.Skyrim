using LiveSplit.AutoSplitting.Variables;
using System;
using System.Windows.Forms;

namespace LiveSplit.AutoSplitting.Editors
{
	public partial class VariableEditor : Form
	{
		public Variable EditedVariable { get; protected set; }

		AutoSplitEnv _env;
		VariableControl _editor;

		public VariableEditor(AutoSplitEnv env, Type varType, Variable source)
		{
			InitializeComponent();

			_env = env;
			EditedVariable = source?.Clone(env);

			var typeName = varType.Name.IndexOf("`") != -1
				? varType.Name.Remove(varType.Name.IndexOf("`"))
				: varType.Name;
			Text = typeName + " Editor";

			_editor = (VariableControl)Activator.CreateInstance(_env.GetEditor(varType), env, source);
			_editor.Dock = DockStyle.Fill;
			_editor.Margin = new Padding(_editor.Margin.Left, _editor.Margin.Top, _editor.Margin.Right, 0);
			_editor.TabIndex = 0;
			tlpMain.Controls.Add(_editor);
			tlpMain.SetRow(_editor, 0);
			tlpMain.SetColumn(_editor, 0);
		}

		void btnOK_Click(object sender, EventArgs e)
		{
			if (_editor.OnClose())
			{
				Close();
				EditedVariable = _editor.Value;
				DialogResult = DialogResult.OK;
			}
		}

		public static Variable ShowEditor(AutoSplitEnv env, Type type, Variable source = null)
		{
			using (var form = new VariableEditor(env, type, source))
			{
				if (form.ShowDialog() != DialogResult.Cancel)
					return form.EditedVariable;
			}
			return source;
		}
		public static Variable ShowEditor(AutoSplitEnv env, Variable source) => ShowEditor(env, source.GetType(), source);
	}

	public class VariableControl : UserControl
	{
		public virtual Variable Value { get; protected set; }

		public VariableControl(AutoSplitEnv env, Variable source)
		{
			Value = source?.Clone(env);
		}

		[Obsolete("Designer only", true)]
		public VariableControl() { }

		public virtual bool OnClose() => true;
	}
}
