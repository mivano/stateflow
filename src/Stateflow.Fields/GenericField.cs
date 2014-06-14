using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	/// <summary>
	/// A generic implementation of a field.
	/// </summary>
	public class GenericField<TIdentifier> : IField<TIdentifier>, IFieldActions
	{
		private object _value;
		private readonly IFieldDefinition<TIdentifier> _fieldDefinition;
		private readonly IRevision<TIdentifier> _revision;

		public GenericField (IRevision<TIdentifier> revision, IFieldDefinition<TIdentifier> fieldDefinition)
		{

			if (fieldDefinition == null)
				throw new ArgumentNullException ("fieldDefinition", "No field definition specified.");

			if (revision == null)
				throw new ArgumentNullException ("revision", "No revision data specified");

			_revision = revision;

			_fieldDefinition = fieldDefinition;
		}

		#region IField implementation

		public virtual IFieldDefinition<TIdentifier> FieldDefinition {
			get {
				return _fieldDefinition;
			}
		}

		public virtual IEnumerable AllowedValues {
			get {
				return FieldDefinition.AllowedValues;
			}

		}

	

		public bool IsDirty {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsEditable {
			get {
				throw new NotImplementedException ();
			}
		}

		public string ReferenceName {
			get {
				return FieldDefinition.ReferenceName;
			}
		}

		public object OriginalValue {
			get {
				return _revision.GetOriginalFieldValue (FieldDefinition);
			}
		}

		public FieldStatus Status {
			get {
				return IsValid ? FieldStatus.Valid : FieldStatus.InValid;
			}
		}

		public object Value {
			get {
				return _revision.GetCurrentFieldValue(_fieldDefinition);
			}
			set { 
				if (value != _value && OnBeforeValueChange (value)) {
					_revision.SetFieldValue (_fieldDefinition, value);
					OnAfterValueChange ();
				}
			}
		}

		public object DefaultValue { get{ return FieldDefinition.DefaultValue;} }

		/// <summary>
		/// Raised before the value is changed. This allows a subclasses to inspect and optionally reject by returning false.
		/// </summary>
		/// <param name="newValue">New value.</param>
		public virtual bool OnBeforeValueChange(object newValue)
		{
			return true;
		}

		/// <summary>
		/// Raised after the value changed.
		/// </summary>
		public virtual void OnAfterValueChange()
		{
		
		}

		public string Name{ get { return FieldDefinition.Name; } }

		public string Description{ get { return FieldDefinition.Description; } }

		public FieldType FieldType { get { return FieldDefinition.FieldType; } }

		public IFieldOptions FieldOptions { get {  return FieldDefinition.FieldOptions;} }

		public int Sequence { get; set; }

		public IEnumerable<IFieldValidator<TIdentifier>> Validators { get; set; }

		public TIdentifier Id { get { return FieldDefinition.Id; } }

		#endregion

		public bool IsValid {
			get {
				if (Validators == null || Validators.Any () == false)
					return true;

				return Validators.All (a => a.IsValid (this));
			}
		}
	}

}
