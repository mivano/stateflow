using System;

namespace Stateflow.Fields
{

	public interface ICorrelateBy<TIdentifier>
	{
		        TIdentifier Id { get; set; }
	}
}
