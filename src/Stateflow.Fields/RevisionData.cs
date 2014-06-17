using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	internal class RevisionData<TIdentifier>{

		private readonly Dictionary<TIdentifier, object> _changes;

		public RevisionData ()
		{
			_changes = new Dictionary<TIdentifier, object> ();
		}

		public Dictionary<TIdentifier, object> Changes {
			get {
				return _changes;
			}
		}

		public int RevisionNumber {
			get;
			set;
		}
	}

}
