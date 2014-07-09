using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{
	public delegate void ValueChangedEventHandler(object sender, Object newValue);


	/// <summary>
	/// A generic implementation of a field.
	/// </summary>
	public class Field<TIdentifier> : IField<TIdentifier>, IFieldActions
	{
		private readonly IFieldDefinition<TIdentifier> _fieldDefinition;
		private readonly IRevision<TIdentifier> _revision;
		public event ValueChangedEventHandler ValueChanged;

		public Field (IRevision<TIdentifier> revision, IFieldDefinition<TIdentifier> fieldDefinition)
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

		public virtual bool IsDirty {
			get {
				return FieldDefinition.IsComputed;
			}
		}

		public virtual bool IsEditable {
			get {
				return FieldDefinition.IsEditable;
			}
		}

		public virtual string ReferenceName {
			get {
				return FieldDefinition.ReferenceName;
			}
		}

		public virtual object OriginalValue {
			get {
				return _revision.GetOriginalFieldValue (FieldDefinition);
			}
		}

		public virtual FieldStatus Status {
			get {
				return IsValid ? FieldStatus.Valid : FieldStatus.InValid;
			}
		}

		public virtual object Value {
			get {
				return _revision.GetCurrentFieldValue(_fieldDefinition);
			}
			set { 
				if (value != _revision.GetOriginalFieldValue(_fieldDefinition) && OnBeforeValueChange (value)) {
					_revision.SetFieldValue (_fieldDefinition, value);
					OnAfterValueChange ();
					if (ValueChanged != null)
						ValueChanged (this, value);
				}
			}
		}

		public virtual object DefaultValue { get{ return FieldDefinition.DefaultValue;} }

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

		public virtual string Name{ get { return FieldDefinition.Name; } }

		public virtual string Description{ get { return FieldDefinition.Description; } }

		public virtual FieldType FieldType { get { return FieldDefinition.FieldType; } }

		public virtual IFieldOptions FieldOptions { get {  return FieldDefinition.FieldOptions;} }

		public virtual int Sequence { get; set; }

		public virtual IEnumerable<IFieldValidator<TIdentifier>> Validators { get; set; }

		public virtual TIdentifier Id { get { return FieldDefinition.Id; } }

		#endregion

		public virtual bool IsValid {
			get {
				if (Validators == null || Validators.Any () == false)
					return true;

				return Validators.All (a => a.IsValid (this));
			}
		}
	}

}
