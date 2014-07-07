using System.Collections.Generic;

namespace Stateflow.Fields
{

	/// <summary>
	/// Contains the delta for a single revision.
	/// </summary>
	public class RevisionData<TIdentifier>{

		private readonly Dictionary<TIdentifier, object> _changes;

	

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Fields.RevisionData`1"/> class by passing in the changes and the revision number.
		/// </summary>
		/// <param name="revisionNumber">Revision number.</param>
		/// <param name="changes">Changes.</param>
		public RevisionData (int revisionNumber, Dictionary<TIdentifier, object> changes)
		{
			if (changes == null)
				throw new System.ArgumentNullException ("changes");

			_changes = changes;
			RevisionNumber = revisionNumber;
			IsLoaded = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Fields.RevisionData`1"/> class which is not yet loaded for this specific revision.
		/// </summary>
		/// <param name="revisionNumber">Revision number.</param>
		public RevisionData (int revisionNumber)
		{
			RevisionNumber = revisionNumber;
			IsLoaded = false;
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

		public bool IsLoaded {
			get;
			private set;
		}
	}

}
