using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// The actual field data itself.
	/// </summary>
	public class FieldData<TIdentity>{

		/// <summary>
		/// The field type to store.
		/// </summary>
		/// <value>The type of the field.</value>
		public FieldType FieldType {
			get;
			set;
		}

		/// <summary>
		/// The actual value.
		/// </summary>
		/// <value>The value.</value>
		public object Value {
			get;
			set;
		}

		public string DisplayValue {
			get;
			set;
		}

		/// <summary>
		/// When the field type is a reference, this property contains the links to the items.
		/// </summary>
		/// <value>The references.</value>
		public IList<Reference<TIdentity>> References {
			get;
			set;
		}

	}

}
