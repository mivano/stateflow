using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	/// <summary>
	/// Describes a fields item, managing a collection of fields and their revisions.
	/// </summary>
	public interface IFieldsTemplate<TIdentifier>: IIdentifiableBy<TIdentifier>{

		FieldDefinitionCollection<TIdentifier> FieldDefinitions { get; }

		string Name {get;}
		string Description { get; }

		IFieldsItemStore<TIdentifier> Store {get;}

		IFieldsItem<TIdentifier> CreateNew();
	}


}
