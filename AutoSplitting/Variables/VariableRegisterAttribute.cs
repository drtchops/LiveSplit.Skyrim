using LiveSplit.AutoSplitting.Editors;
using System;

namespace LiveSplit.AutoSplitting.Variables
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class VariableRegisterAttribute : Attribute
	{
		public Type VariableType { get; }

		public Type EditorType { get; }

		public bool Hidden { get; set; }

		public VariableRegisterAttribute(Type variableType, Type editorType = null)
		{
			if (variableType == null)
				throw new ArgumentNullException(nameof(variableType));
			else if (!variableType.IsSubclassOf(typeof(Variable)))
				throw new ArgumentException($"{nameof(variableType)} must be a sub class of Variable", nameof(variableType));

			if (editorType != null && !editorType.IsSubclassOf(typeof(VariableControl)))
				throw new ArgumentException($"{nameof(editorType)} must be a sub class of VariableControl", nameof(editorType));

			VariableType = variableType;
			EditorType = editorType;
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class VariableDescriptorAttribute : Attribute
	{
		/// <summary>
		/// Allow multiple variables of this type within an AutoSplit.
		/// </summary>
		public bool AllowMultiple { get; set; } = true;

		public string Description { get; set; }
	}
}
