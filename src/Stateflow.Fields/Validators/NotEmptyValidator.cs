namespace Stateflow.Fields.Validators
{
	public class NotEmptyValidator<TIdentifier>: IFieldValidator<TIdentifier>
	{
		#region IFieldValidator implementation

		public bool IsValid(IField<TIdentifier> field)
		{
			switch (field.FieldDefinition.FieldType) {
			case FieldType.MultiLineText:
			case FieldType.SingleLineText:
			case FieldType.HtmlText:
				return field.Value != null && !string.IsNullOrEmpty ((string)field.Value);

			default:
				break;
			}
			return field.Value != null;
		}

		#endregion


	}

}

