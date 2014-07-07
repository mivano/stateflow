using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

    /// <summary>
    /// Used for storing the actual data of the fields.
    /// </summary>
    public class FieldData<TIdentifier>
    {
        private readonly IFieldsItem<TIdentifier> _fieldsItem;
        private Dictionary<TIdentifier, object> _values;
        private Dictionary<TIdentifier, object> _oldValues;
        private Dictionary<int, RevisionData<TIdentifier>> _revisions;
        private Dictionary<TIdentifier, object> _changes;

        /// <summary>
        /// Create a new data container for the field item.
        /// </summary>
        /// <param name="fieldsItem"></param>
        public FieldData(IFieldsItem<TIdentifier> fieldsItem)
        {
            if (fieldsItem == null)
                throw new ArgumentNullException("fieldsItem");

            _fieldsItem = fieldsItem;
            _values = new Dictionary<TIdentifier, object>();
            _changes = new Dictionary<TIdentifier, object>();
            _oldValues = new Dictionary<TIdentifier, object>();
            _revisions = new Dictionary<int, RevisionData<TIdentifier>>();
        }

		public FieldData (IFieldsItem<TIdentifier> fieldsItem, Dictionary<TIdentifier, object> values,  Dictionary<int, RevisionData<TIdentifier>> revisions)
		{
			if (fieldsItem == null)
				throw new ArgumentNullException("fieldsItem");

			_fieldsItem = fieldsItem;
			_values = values;
			_changes = new Dictionary<TIdentifier, object>();
			_oldValues = new Dictionary<TIdentifier, object>();
			_revisions = revisions;
		}

        internal object GetFieldValue(TIdentifier id, int revision)
        {
            Object value = null;
            switch (revision)
            {
                case -2:  // Previous value
                    {
                        if (_oldValues.TryGetValue(id, out value))
                            return value;
                        else
                            return null;

                    }
                case -1: // Current value
                    {
                        if (_values.TryGetValue(id, out value))
                            return value;
                        else
                            return _fieldsItem.Fields[id].DefaultValue;

                    }
			default: // From revision
				{
					if (!_revisions.ContainsKey (revision))
						return null;

					if (_revisions [revision].Changes.TryGetValue (id, out value))
						return value;

					return null;
				}
            }

        }

        internal bool SetFieldValue(TIdentifier id, object value)
        {
            // update the old values
            Object oldValue = null;
            if (_values.TryGetValue(id, out oldValue))
            {
                _oldValues[id] = oldValue;
            }

            _values[id] = value;

            // Mark the field as being changed
            _changes[id] = value;

            return true;
        }

        public bool IsDirty()
        {
            return _changes != null && _changes.Any();
        }

        public int Versions
        {
            get
            {
                return _revisions.Count;
            }
        }

        // Stores the changed values into a new revision
        public int CreateNewRevision()
        {
            if (!IsDirty())
                return Versions;

            var revisionData = new RevisionData<TIdentifier>(Versions + 1, _changes);

            _revisions.Add(revisionData.RevisionNumber, revisionData);

            // Clear current stored changes
            _changes.Clear();

            return Versions;

        }
    }

}
