using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields.DataStores
{

	public interface IFieldValidator<TIdentifier>
	{
		bool IsValid(IField<TIdentifier> field);
	}
}
