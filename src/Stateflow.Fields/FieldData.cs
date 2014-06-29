using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	/// <summary>
	/// Used for storing the actual data of the fields.
	/// </summary>
	internal class FieldData<TIdentifier>
	{
		private readonly IFieldsItem<TIdentifier> _fieldsItem;
		private Dictionary<TIdentifier, object> _values;
		private Dictionary<TIdentifier, object> _oldValues;
		private Dictionary<int, RevisionData<TIdentifier>> _revisions;
		private Dictionary<TIdentifier, object> _changes;

		public FieldData (IFieldsItem<TIdentifier> fieldsItem)
		{
			if (fieldsItem == null)
				throw new ArgumentNullException ("fieldsItem");

			_fieldsItem=fieldsItem;
			_values = new Dictionary<TIdentifier, object> ();
			_changes = new Dictionary<TIdentifier, object> ();
			_oldValues = new Dictionary<TIdentifier, object> ();
			_revisions = new Dictionary<int, RevisionData<TIdentifier>> ();
		}

		internal object GetFieldValue(TIdentifier id, int revision){
			Object value = null;
			switch (revision) {
			case -2:  // Previous value
				{
					if (_oldValues.TryGetValue (id, out value))
						return value;
					else
						return null;

				}
			case -1: // Current value
				{
					if (_values.TryGetValue (id, out value))
						return value;
					else
						return _fieldsItem.Fields [id].DefaultValue;

				}
			default: // From revision
				{

					return null;
				}
				break;
			}

		}

		internal bool SetFieldValue(TIdentifier id, object value)
		{
			// update the old values
			Object oldValue = null;
			if (_values.TryGetValue (id, out oldValue))
			{
				_oldValues [id] = oldValue;
			}

			_values [id] = value;

			// Mark the field as being changed
			_changes[id] = value;

			return true;
		}

		public bool IsDirty()
		{
			return _changes != null && _changes.Any ();
		}
	}

}