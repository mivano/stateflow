using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{
	/// <summary>
	/// Defines a field definition containing multiple instances of fields.
	/// </summary>
	public class FieldListDefinition<TIdentifier>: FieldDefinition<TIdentifier>
	{

		private readonly ITemplate<int> _itemTemplate;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Fields.FieldListDefinition`1"/> class and pass in the template used for creating the new items.
		/// </summary>
		/// <param name="itemTemplate">Item template.</param>
		public FieldListDefinition (ITemplate<int> itemTemplate) : base ()
		{
			if (itemTemplate == null)
				throw new ArgumentNullException ("itemTemplate");
			
			_itemTemplate = itemTemplate;
			FieldType = FieldType.List;

		}

		public FieldListDefinition (ITemplate<int> itemTemplate, TIdentifier id, string name) : base (FieldType.List,id, name){
			if (itemTemplate == null)
				throw new ArgumentNullException ("itemTemplate");

			_itemTemplate = itemTemplate;

		}



	}

}
