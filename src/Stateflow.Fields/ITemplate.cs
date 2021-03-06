using System;
using Stateflow.Fields.DataStores;

namespace Stateflow.Fields
{

	/// <summary>
	/// Describes a fields item, managing a collection of fields and their revisions.
	/// </summary>
	public interface ITemplate<TIdentifier>: IIdentifiableBy<TIdentifier>{

		FieldDefinitionCollection<TIdentifier> FieldDefinitions { get; }

		string Name { get; set;}
		string Description { get; set; }
		Version Version { get; set; }

		IDataStore<TIdentifier> Store {get;}

		IFieldsItem<TIdentifier> CreateNew();

	    void Save();

	}


}
