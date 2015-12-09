using LiveSplit.AutoSplitting.Variables;
using LiveSplit.ComponentUtil;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.AutoSplitting.Editors
{
	public partial class MemoryValueEditor : VariableControl
	{
		readonly AutoSplitEnv _env;
		Type _innerType;

		public MemoryValueEditor(AutoSplitEnv env, MemoryValue source = null) : base(env, source)
		{
			InitializeComponent();

			_env = env;

			cbAddresses.DataSource = _env.Addresses.Where(w => IsSupportedMemoryWatcher(w)).OrderBy(w => w.Name).ToList();
			cbAddresses.SelectionChangeCommitted += (s, e) => ChangeAddress((MemoryWatcher)cbAddresses.SelectedItem);

			if (source != null)
			{
				var addr = _env.Addresses.FirstOrDefault(m => m.Name == source.MemoryWatcherName);
				if (addr != null)
					cbAddresses.SelectedItem = addr;
				else
					MessageBox.Show($"The address \"{source.MemoryWatcherName}\" does not exist.\nPlease choose another one.",
						"Address not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			ChangeAddress((MemoryWatcher)cbAddresses.SelectedItem);

			if (source != null)
			{
				if (cbValue.DropDownStyle == ComboBoxStyle.DropDownList)
					cbValue.SelectedItem = source.Value;
				else
				{
					var value = source.Value as IFormattable;
					cbValue.Text = value.ToString(null, CultureInfo.InvariantCulture);
				}
				cbComparison.SelectedItem = source.Comparison;
				chkOnChange.Checked = source.OnValueChanged;
				if (source is StringValue)
				{
					var stringValue = (StringValue)source;
					chkIgnoreCase.Checked = stringValue.IgnoreCase;
					chkContains.Checked = stringValue.IsContained;
				}
			}
		}

		void ChangeAddress(MemoryWatcher watcher)
		{
			if (watcher is StringWatcher)
				_innerType = typeof(string);
			else
			{
				_innerType = watcher.GetType().GetProperties()
					.First(p => p.Name == "Current" && p.PropertyType != typeof(object)).GetValue(watcher)
					.GetType();
			}

			SetComparisons();
			SetPossibleValues();
			SetOptions();
		}

		void SetComparisons()
		{
			Comparison[] comparisons;

			if (_innerType == typeof(bool) || _innerType == typeof(string))
			{
				comparisons = new Comparison[]
				{
					Comparison.Equals,
					Comparison.Unequals
				};
			}
			else
				comparisons = (Comparison[])Enum.GetValues(typeof(Comparison));

			cbComparison.DataSource = comparisons;
		}

		void SetPossibleValues()
		{
			object[] possibleValues = null;
			var editable = true;

			if (_innerType == typeof(bool))
			{
				editable = false;
				possibleValues = new object[] { true, false };
			}

			cbValue.ResetText();
			cbValue.DataSource = possibleValues;
			cbValue.DropDownStyle = editable ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
		}

		void SetOptions()
		{
			chkIgnoreCase.Visible = chkContains.Visible = _innerType == typeof(string);
		}

		public MemoryValue GetResult()
		{
			var memoryValueType = _innerType == typeof(string)
				? typeof(StringValue)
				: typeof(MemoryValue<>).MakeGenericType(_innerType);

			var variable = (MemoryValue)Activator.CreateInstance(memoryValueType,
				((MemoryWatcher)cbAddresses.SelectedItem).Name,
				(Comparison)cbComparison.SelectedItem,
				GetValue(),
				chkOnChange.Checked);

			if (variable is StringValue)
			{
				var stringValue = (StringValue)variable;
				stringValue.IgnoreCase = chkIgnoreCase.Checked;
				stringValue.IsContained = chkContains.Checked;
			}

			return variable;
		}

		object GetValue()
		{
			try
			{
				return TypeDescriptor.GetConverter(_innerType).ConvertFromString(null, CultureInfo.InvariantCulture, cbValue.Text);
			}
			catch { return null; }
		}

		public override bool OnClose()
		{
			if (GetValue() != null)
			{
				Value = GetResult();
				return true;
			}

			MessageBox.Show("Invalid properties.", "MemoryValue Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return false;
		}

		void cbComparison_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbComparison.SelectedIndex == -1)
				return;

			var selectedItem = (Comparison)cbComparison.SelectedItem;
			chkOnChange.Enabled = selectedItem != Comparison.IncreasedBy && selectedItem != Comparison.DecreasedBy;
		}

		void cbValue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && IsNumericType(_innerType) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
			{
				if (IsDecimalType(_innerType))
				{
					if (e.KeyChar != '.')
						e.Handled = true;
				}
				else
					e.Handled = true;
			}
		}

		public static bool IsSupportedMemoryWatcher(MemoryWatcher watcher)
		{
			var t = watcher.GetType();

			if (t.IsGenericType)
			{
				var genericT = t.GetGenericTypeDefinition();
				var genericArg = t.GetGenericArguments()[0];

				if (genericT == typeof(MemoryWatcher<>) || genericT == typeof(FakeMemoryWatcher<>))
					return genericArg.IsPrimitive;
			}

			if (watcher is StringWatcher)
				return true;

			return false;
		}

		static bool IsNumericType(Type type)
		{
			if (IsDecimalType(type))
				return true;

			switch (Type.GetTypeCode(type))
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
					return true;
				default:
					return false;
			}
		}

		static bool IsDecimalType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return true;
				default:
					return false;
			}
		}
	}
}
