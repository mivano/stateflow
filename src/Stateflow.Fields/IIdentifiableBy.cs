using System;

namespace Stateflow.Fields.DataStores
{

	public interface IIdentifiableBy<TIdentifier>
	{
		        TIdentifier Id { get; set; }
	}
}
