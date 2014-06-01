using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{
	public interface IField<TIdentifier>: ICorrelateBy<TIdentifier>
	{
		/// <summary>
		/// The actual value of the field.
		/// </summary>
		/// <value>The value.</value>
		object Value {
			get;
			set;
		}

		/// <summary>
		/// Name of the field.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; set; }

		/// <summary>
		/// A description of this field.
		/// </summary>
		/// <value>The description.</value>
		string Description { get; set; }


		/// <summary>
		/// The type of field to use.
		/// </summary>
		/// <value>The type of the field.</value>
		FieldType FieldType{ get; set; }

		IFieldOptions FieldOptions { get; set; }

		int Sequence { get; set;}
	}

	public enum FieldType
	{
		Amount,
		Decimal,
		Number,
		SingleLineText,
		MultiLineText,
		RichText,
		Boolean,
		SingleSelect,
		MultiSelect,
		Date,
		DateTime,
		Time,
		File,
		FieldCollection
	}

	public interface IFieldOptions
	{
		
	}

	public class FieldCollection<TIdentifier> : Dictionary <TIdentifier, IField<TIdentifier>>, IField<TIdentifier>
	{
		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add a single field to the dictionary.
		/// </summary>
		/// <param name="field">Field.</param>
		public void Add(IField<TIdentifier> field){
			if (field == null)
				throw new ArgumentNullException ("field");

			base.Add (field.Id, field);
		}

		/// <summary>
		/// Adds a generic field using the specified name, value and fieldType.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <param name="fieldType">Field type.</param>
		public void Add(string name, object value, FieldType fieldType){
			Add(new GenericField<TIdentifier>{ 
				Name = name, 
				Value = value,
				FieldType=fieldType}
			);
		}

		/// <summary>
		/// Adds a new collection to the field collection.
		/// </summary>
		/// <returns>The collection.</returns>
		/// <param name="name">Name.</param>
		public FieldCollection<TIdentifier> AddCollection(string name){
			var c = new FieldCollection<TIdentifier> ();
			c.Name = name;
			Add (c);
			return c;
		}

		#region IField implementation

		public object Value {
			get {
				return this.Values;
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public string Name {
			get;
			set;
		}

		public string Description {
			get;
			set;
		}

		public FieldType FieldType {
			get {
				return FieldType.FieldCollection;
			}
			set {
				throw new ArgumentException("Cannot set the field type for a list.");
			}
		}

		public IFieldOptions FieldOptions {
			get;
			set;
		}

		public int Sequence {
			get;
			set;
		}

		#endregion

		#region ICorrelateBy implementation

		public TIdentifier Id {
			get;
			set;
		}

		#endregion
	}

	public class GenericField<TIdentifier> : IField<TIdentifier>
	{
		#region IField implementation

		public object Value { get; set; }

		public string Name{ get; set; }

		public string Description{ get; set; }

		public FieldType FieldType { get; set; }

		public IFieldOptions FieldOptions { get; set; }

		public int Sequence { get; set; }

		#endregion

		#region ICorrelateBy implementation

		public TIdentifier Id { get; set; }

		#endregion


	}
}

