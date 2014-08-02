using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// A reference to another item.
	/// </summary>
	public class Reference<TIdentity>{

		public string DisplayValue {
			get;
			set;
		}

		public ItemIdentifier<TIdentity> Identifier {
			get;
			set;
		}

	}

}
