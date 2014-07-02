using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields.DataStores
{

	public interface IFieldsItem<TIdentifier>: IRevision<TIdentifier>, IIdentifiableBy<TIdentifier>
	{
		FieldCollection<TIdentifier> Fields { get; }

		bool IsDirty{get;}
		bool IsNew{get;}

		int RevisionNumber {get;}

		RevisionCollection<TIdentifier> Revisions { get;}
		object GetFieldValue(TIdentifier id, int revision);
		FieldData<TIdentifier> FieldData {get;}

		void Save();
		void Load();
		bool Validate();
	}
	
}
