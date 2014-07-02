using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields.DataStores
{

	public interface IFieldsRevision<TIdentifier>{

		FieldCollection<TIdentifier> Fields { get; }

	}

}
