using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{
	/// <summary>
	/// Contains a collection of field definitions and values for a certain revision number.
	/// </summary>
	public class FieldsRevision<TIdentifier>: IFieldsRevision<TIdentifier>, IRevision<TIdentifier>
	{
		private readonly FieldDefinitionCollection<TIdentifier> _fieldDefinitions;

		private readonly int _revisionNumber;

		private Dictionary<TIdentifier, object> _values;

		private FieldCollection<TIdentifier> _fields;
		private readonly IFieldsItem<TIdentifier> _fieldsItem;

		public FieldsRevision (FieldDefinitionCollection<TIdentifier> fieldDefinitions, int revisionNumber)
		{
			_revisionNumber = revisionNumber;
			_fieldDefinitions = fieldDefinitions;

			_values = new Dictionary<TIdentifier, object> ();
		}

	

		public FieldsRevision (IFieldsItem<TIdentifier> fieldsItem, int revisionNumber)
		{
			if (fieldsItem == null)
				throw new ArgumentNullException ("fieldsItem");

			_fieldsItem = fieldsItem;

			_revisionNumber = revisionNumber;
			_fieldDefinitions = fieldsItem.FieldsItemType.FieldDefinitions;

			_values = new Dictionary<TIdentifier, object> ();
		}

		#region IRevision implementation

		public object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			if (_fieldsItem != null)
				return _fieldsItem.GetFieldValue (fieldDefinition.Id, _revisionNumber);
			return _values [fieldDefinition.Id];
		}

		public object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			if (_fieldsItem != null)
				return _fieldsItem.GetFieldValue (fieldDefinition.Id, _revisionNumber-1);

			return _values [fieldDefinition.Id];
		}

		public void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value)
		{
			throw new InvalidOperationException("Unable to change the value for a revision item.");
		}

		public IFieldsItemType<TIdentifier> FieldsItemType {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsReadOnly {
			get {
				return true;
			}
		}

		public bool IsDraft {
			get {
				throw new NotImplementedException ();
			}
		}

		public int Number {
			get {
				return _revisionNumber;
			}
		}


		public FieldCollection<TIdentifier> Fields {
			get {
				if (_fields == null) {
					_fields = new FieldCollection<TIdentifier> (this, _fieldDefinitions);
				}
				return _fields;
			}
		}


		#endregion

		public object this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				return Fields[name].Value;
			}
		}
	}
}
