using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// Contains the data value for a field.
	/// </summary>
	public class FieldValue<TIdentity>{

		public TIdentity FieldDefinition {
			get;
			set;
		}

		public FieldData<TIdentity> Data {
			get;
			set;
		}

	}

}
