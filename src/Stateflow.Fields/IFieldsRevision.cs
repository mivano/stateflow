using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	public interface IFieldsRevision<TIdentifier>{

		FieldCollection<TIdentifier> Fields { get; }

	}

}
