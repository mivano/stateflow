using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	/// <summary>
	/// A fields item contains field definitions and their values and is based on a FieldsItemType
	/// </summary>
	public class FieldsItem<TIdentifier>: IFieldsItem<TIdentifier>
	{

		private readonly IFieldsTemplate<TIdentifier> _fieldsTemplate;
		private FieldCollection<TIdentifier> _fields;
		private RevisionCollection<TIdentifier> _revisions;
		private FieldData<TIdentifier> _fieldData;
		private IFieldsItemStore<TIdentifier> _store;

		public FieldsItem (IFieldsTemplate<TIdentifier> fieldsTemplate)
		{
			if (fieldsTemplate == null)
				throw new ArgumentNullException ("fieldsTemplate");

			_fieldsTemplate = fieldsTemplate;
			Id = default(TIdentifier);
			_fieldData = new FieldData<TIdentifier> (this);
			_store = fieldsTemplate.Store;
		}

		public TIdentifier Id { get; set;		}

		#region IRevision implementation

		public virtual object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			return GetFieldValue (fieldDefinition.Id, -1);
		}

		public virtual object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			return GetFieldValue (fieldDefinition.Id, -2);
		}

		public virtual void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value)
		{
			SetFieldValueInternal (fieldDefinition, value);
		}

		private void SetFieldValueInternal(IFieldDefinition<TIdentifier> fieldDefinition, object value){
		
			// get current value
			var currentValue = _fieldData.GetFieldValue (fieldDefinition.Id, -1);

			// Only change when actually different
			if (!object.Equals(currentValue, value))
			{
				if (!fieldDefinition.IsEditable) {
					throw new ValidationException (string.Format("The field '{0}' is readonly.", fieldDefinition.Name));
				}

				_fieldData.SetFieldValue (fieldDefinition.Id, value);
			}


		}

		public object GetFieldValue(TIdentifier id, int revision){
			return _fieldData.GetFieldValue (id, revision);
		}


		public IFieldsTemplate<TIdentifier> FieldsItemType {
			get {
				return _fieldsTemplate;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public bool IsDraft {
			get {
				throw new NotImplementedException ();
			}
		}

		public int Number {
			get {
				return -1;
			}
		}

		// Save the item, this will create a new revision. 
		public void Save()
		{
			if (IsDirty) {

				// Create a new revision
				_fieldData.CreateNewRevision ();

				// Store the data, this should create a new identifier
				_store.SaveFieldsItems (new [] { this });

				// Refresh the revision list
				Revisions.PrepareRevisions ();

			}

		}

		public void Load()
		{
			throw new NotImplementedException ();
		}

		public bool Validate()
		{
			throw new NotImplementedException ();
		}


		#endregion

		public FieldCollection<TIdentifier> Fields {

			get {
				if (_fields == null) {
					_fields = new FieldCollection<TIdentifier> (this, FieldsItemType.FieldDefinitions);
				}
				return _fields;
			}

		}

		public FieldData<TIdentifier> FieldData {
			get {
				return _fieldData;
			}
		}

		public IFieldsTemplate<TIdentifier> FieldsTemplate {
			get {
				return _fieldsTemplate;
			}
		}

		public bool IsDirty {
			get {
				return _fieldData.IsDirty ();
			}
		}

		public bool IsNew {
			get {
				return Id.Equals( default(TIdentifier));
			}
		}

		public int RevisionNumber {
			get {
				throw new NotImplementedException ();
			}
		}

		public RevisionCollection<TIdentifier> Revisions {
			get
			{
				if (_revisions == null)
				{
					_revisions = new RevisionCollection<TIdentifier>(this);
				}
				return _revisions;
			}
		}
	}


}
