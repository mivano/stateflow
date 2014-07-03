namespace Stateflow.Fields
{

	public interface IFieldValidator<TIdentifier>
	{
		bool IsValid(IField<TIdentifier> field);
	}
}
