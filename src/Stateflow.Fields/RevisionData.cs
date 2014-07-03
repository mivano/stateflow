using System.Collections.Generic;

namespace Stateflow.Fields
{

	internal class RevisionData<TIdentifier>{

		private readonly Dictionary<TIdentifier, object> _changes;

		public RevisionData ()
		{
			_changes = new Dictionary<TIdentifier, object> ();
		}

		public RevisionData (int revisionNumber, Dictionary<TIdentifier, object> changes)
		{
			_changes = changes;
			RevisionNumber = revisionNumber;
		}

		public Dictionary<TIdentifier, object> Changes {
			get {
				return _changes;
			}
		}

		public int RevisionNumber {
			get;
			private set;
		}
	}

}
