namespace Stateflow.Data
{

	public interface IIdentifiableBy<TIdentifier>
	{
		        TIdentifier Id { get; set; }
	}
}
