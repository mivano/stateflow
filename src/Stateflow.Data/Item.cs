using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// An item that contains data fields.
	/// </summary>
	public class Item<TIdentity>
	{

		public ItemIdentifier<TIdentity> Id {
			get;
			set;
		}

		public TIdentity TemplateId {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public List<FieldValue<TIdentity>> FieldValues {
			get;
			set;
		}

		public bool IsDeleted {
			get;
			set;
		}

	}

}
