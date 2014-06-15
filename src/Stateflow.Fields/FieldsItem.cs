using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	/// <summary>
	/// A fields item contains field definitions and their values and is based on a FieldsItemType
	/// </summary>
	public class FieldsItem<TIdentifier>: IFieldsItem<TIdentifier>{

		private readonly IFieldsItemType<TIdentifier> _fieldsItemType;

		public FieldsItem (IFieldsItemType<TIdentifier> fieldsItemType)
		{
			if (fieldsItemType == null)
				throw new ArgumentNullException ("fieldsItemType");

			_fieldsItemType = fieldsItemType;
			
		}

		#region IRevision implementation
		public object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			throw new NotImplementedException ();
		}
		public object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition)
		{
			throw new NotImplementedException ();
		}
		public void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value)
		{
			throw new NotImplementedException ();
		}

		public IFieldsItemType<TIdentifier> FieldsItemType {
			get {
				return _fieldsItemType;
			}
		}
		public bool IsReadOnly {
			get {
				throw new NotImplementedException ();
			}
		}
		public bool IsDraft {
			get {
				throw new NotImplementedException ();
			}
		}
		public int Number {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion

		public FieldCollection<TIdentifier> Fields {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsDirty {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsNew {
			get {
				throw new NotImplementedException ();
			}
		}

		public int RevisionNumber {
			get {
				throw new NotImplementedException ();
			}
		}

		public object Revisions {
			get {
				throw new NotImplementedException ();
			}
		}
	}
	
}
