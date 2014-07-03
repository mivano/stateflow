namespace Stateflow.Fields
{
	public interface IRevision<TIdentifier>{

		ITemplate<TIdentifier> Template { get; }

		object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition);
		object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition);

		void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value);

		bool IsReadOnly { get; }
		int Number { get; }

		FieldCollection<TIdentifier> Fields { get; }
	}

}
