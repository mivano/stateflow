using System;

namespace Stateflow.Fields
{
	public class NotEmptyValidator<TIdentifier>: IFieldValidator<TIdentifier>
	{
		#region IFieldValidator implementation

		public bool IsValid(IField<TIdentifier> field)
		{
			switch (field.FieldType) {
			case FieldType.MultiLineText:
			case FieldType.SingleLineText:
			case FieldType.RichText:
				return field.Value != null && !string.IsNullOrEmpty ((string)field.Value);

			default:
				break;
			}
			return field.Value != null;
		}

		#endregion


	}

}

