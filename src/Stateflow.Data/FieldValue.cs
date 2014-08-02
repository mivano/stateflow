using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// Contains the data value for a field.
	/// </summary>
	public class FieldValue<TIdentity>{

		/// <summary>
		/// Gets or sets the field definition identifier.
		/// </summary>
		/// <value>The field definition identifier.</value>
		public TIdentity FieldDefinitionId {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the name of the field definition.
		/// </summary>
		/// <value>The name of the field definition.</value>
		public string FieldDefinitionName {
			get;
			set;
		}

		public FieldData<TIdentity> Data {
			get;
			set;
		}

	}

}
