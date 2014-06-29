using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{
	public class FieldDefinitionCollection<TIdentifier>: Dictionary<TIdentifier, IFieldDefinition<TIdentifier>>
	{
		IFieldsItemStore<TIdentifier> _store;
		IFieldsTemplate<TIdentifier> _type;

		public FieldDefinitionCollection ()
		{
			
		}

		public FieldDefinitionCollection (IFieldsItemStore<TIdentifier> store, IFieldsTemplate<TIdentifier> type)
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
				var fd = this.FirstOrDefault(a=>a.Value.Name.Equals(name));
				if (!fd.Equals(default(KeyValuePair<TIdentifier, IFieldDefinition<TIdentifier> >)))
					return fd.Value;

					return null;
			}
		}


	}


}
