using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields.DataStores
{

	public interface IDataStore<TIdentifier>
	{

		IRepository< TIdentifier,ITemplate<TIdentifier>> Templates{ get; }

		IRepository< TIdentifier,IFieldsItem<TIdentifier>> Items{ get; }

	}

}
