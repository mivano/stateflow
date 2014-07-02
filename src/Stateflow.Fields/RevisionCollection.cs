using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields.DataStores
{

	public class RevisionCollection<TIdentifier> : Dictionary<int, IRevision<TIdentifier>>
	{
		private readonly IFieldsItem<TIdentifier> _item;

		public RevisionCollection (IFieldsItem<TIdentifier> item)
		{
			if (item == null)
				throw new ArgumentNullException ("item");

			_item = item;

			PrepareRevisions ();
		}

		// Prepopulate the list of revisions.
		public void PrepareRevisions(){
		
			if (_item.IsNew) {
				// No revisions yet as it is a new item
				return;
			}

			this.Clear ();

			for (int i = 1; i <= _item.FieldData.Versions; i++) {
				this.Add(i, new Revision<TIdentifier>(_item, i));
			}

		}

	
	}


}
