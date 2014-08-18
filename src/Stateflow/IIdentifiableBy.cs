namespace Stateflow
{

	public interface IIdentifiableBy<TIdentifier>
	{
		        TIdentifier Id { get; set; }
	}
}
