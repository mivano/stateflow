using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{
	public class FieldDefinitionCollection<TIdentifier>: Dictionary<TIdentifier, IFieldDefinition<TIdentifier>>
	{
		IFieldsItemStore<TIdentifier> _store;
		IFieldsItemType<TIdentifier> _type;

		public FieldDefinitionCollection (IFieldsItemStore<TIdentifier> store, IFieldsItemType<TIdentifier> type)
		{
			_store = store;
			_type = type;


		}

		public void Add(IFieldDefinition<TIdentifier> field)
		{
			if (field == null)
				throw new ArgumentNullException ("field");

			base.Add (field.Id, field);
		}

		public IFieldDefinition<TIdentifier> this [string name] {
			get {
				if (name == null) {
					throw new ArgumentNullException ("name");
				}
				return this.FirstOrDefault(a=>a.Value.Name.Equals(name));
			}
		}

	}


}
