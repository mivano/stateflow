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

		private readonly IFieldsItemType<TIdentifier> _fieldsItemType;
		private FieldCollection<TIdentifier> _fields;
		private RevisionCollection<TIdentifier> _revisions;
		private Dictionary<TIdentifier, object> _values;

		public FieldsItem (IFieldsItemType<TIdentifier> fieldsItemType)
		{
			if (fieldsItemType == null)
				throw new ArgumentNullException ("fieldsItemType");

			_fieldsItemType = fieldsItemType;
			Id = default(TIdentifier);
			_values = new Dictionary<TIdentifier, object> ();
		}

		public TIdentifier Id { get; set;		}

		#region IRevision implementation

		public object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			return _values [fieldDefinition.Id];
		}

		public object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			return _values [fieldDefinition.Id];
		}

		public void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value)
		{
			_values [fieldDefinition.Id] = value;
		}

		public IFieldsItemType<TIdentifier> FieldsItemType {
			get {
				return _fieldsItemType;
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

		#endregion

		public FieldCollection<TIdentifier> Fields {

			get {
				if (_fields == null) {
					_fields = new FieldCollection<TIdentifier> (this, FieldsItemType.FieldDefinitions);
				}
				return _fields;
			}

		}

		public bool IsDirty {
			get {
				throw new NotImplementedException ();
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
