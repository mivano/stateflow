using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{
	public class FieldDefinitionCollection<TIdentifier>: Dictionary<TIdentifier, IFieldDefinition<TIdentifier>>
	{
		public void Add(IFieldDefinition<TIdentifier> field){
			if (field == null)
				throw new ArgumentNullException ("field");

			base.Add (field.Id, field);
		}


	}

}
