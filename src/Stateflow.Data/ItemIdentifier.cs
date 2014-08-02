using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Data
{

	/// <summary>
	/// Uniquely identifies an item.
	/// </summary>
	public class ItemIdentifier<TIdentity>{

		public const int Current = -1;

		public TIdentity Id {
			get;
			set;
		}

		public int Revision {
			get;
			set;
		}

		public TIdentity TemplateId {
			get;
			set;
		}
	}

}
