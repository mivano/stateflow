namespace Stateflow.Fields
{

	public interface IFieldsRevision<TIdentifier>{

		FieldCollection<TIdentifier> Fields { get; }

	}

}
