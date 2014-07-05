using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Fields.DataStores;

namespace Stateflow.Fields
{
	public class FieldDefinitionCollection<TIdentifier>: Dictionary<TIdentifier, IFieldDefinition<TIdentifier>>
	{
		IDataStore<TIdentifier> _store;
		ITemplate<TIdentifier> _type;

		public FieldDefinitionCollection ()
		{
			
		}

		public FieldDefinitionCollection (IDataStore<TIdentifier> store, ITemplate<TIdentifier> type)
		{
			_store = store;
			_type = type;


		}

		public void Add(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			if (fieldDefinition == null)
				throw new ArgumentNullException ("fieldDefinition");
                   
			base.Add (fieldDefinition.Id, fieldDefinition);
		}

	    public void AddRange(IEnumerable<IFieldDefinition<TIdentifier>> fieldDefinitions)
	    {
	        foreach (var fieldDefinition in fieldDefinitions)
	        {
	           this.Add(fieldDefinition); 
	        }
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
