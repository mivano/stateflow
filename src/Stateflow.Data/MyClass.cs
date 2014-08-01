using System;
using System.Collections.Generic;

namespace Stateflow.Data
{
	/// <summary>
	/// Definies a single field.
	/// </summary>
	public class FieldDefinition<TIdentity>: IIdentifiableBy<TIdentity>
	{
		#region IIdentifiableBy implementation

		public TIdentity Id {
			get;
			set;
		}

		#endregion

		public string Name {
			get;
			set;
		}

		public IDictionary<string, object> MetaData{ get; set;}

		public FieldType FieldType {
			get;
			set;
		}


	}

	/// <summary>
	/// Describes the available field types
	/// </summary>
	public enum FieldType{
		Unknown = 0,
		SingleLineText,
		MultiLineText,
		Amount,
		Decimal,
		Number,
		HtmlText,
		Boolean,
		SingleSelect,
		MultiSelect,
		Date,
		DateTime,
		Time,
		File,
		Label,
		Color,
		EmailAddress,
		Password,
		Url,
		List,
		Reference,
		MultiReference
	}

	/// <summary>
	/// Contains the data value for a field.
	/// </summary>
	public class FieldValue<TIdentity>{

		public TIdentity FieldDefinition {
			get;
			set;
		}

		public FieldData<TIdentity> Data {
			get;
			set;
		}

	}

	/// <summary>
	/// The actual field data itself.
	/// </summary>
	public class FieldData<TIdentity>{

		public object Value {
			get;
			set;
		}

		public string DisplayValue {
			get;
			set;
		}

		public IList<Reference<TIdentity>> References {
			get;
			set;
		}


	}

	public class Reference<TIdentity>{

		public string DisplayValue {
			get;
			set;
		}

		public ItemIdentifier<TIdentity> Identifier {
			get;
			set;
		}

	}

	/// <summary>
	/// An item that contains data fields.
	/// </summary>
	public class Item<TIdentity>
	{

		public ItemIdentifier<TIdentity> Id {
			get;
			set;
		}


		public string Name {
			get;
			set;
		}

		public List<FieldValue<TIdentity>> FieldValues {
			get;
			set;
		}

		public bool IsDeleted {
			get;
			set;
		}

	}

	/// <summary>
	/// Uniquely identifies an item.
	/// </summary>
	public class ItemIdentifier<TIdentity>{

		public TIdentity Id {
			get;
			set;
		}

		public int Revision {
			get;
			set;
		}

		public TIdentity TemplateId {
			get;
			set;
		}
	}

	/// <summary>
	/// Describes the standard available fields for a specific template.
	/// </summary>
	public class Template<TIdentity>: IIdentifiableBy<TIdentity>
	{
		#region IIdentifiableBy implementation

		public TIdentity Id {
			get;
			set;
		}

		#endregion

		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Defines the available fields for this template.
		/// </summary>
		/// <value>The field definitions.</value>
		public IList<TemplateFieldDefinition<TIdentity>> FieldDefinitions {
			get;
			set;
		}
	}

	/// <summary>
	/// Describes the field needed for a template.
	/// </summary>
	public class TemplateFieldDefinition<TIdentity>{

		public TIdentity FieldDefinition {
			get;
			set;
		}

		public bool IsRequired {
			get;
			set;
		}
	}

}

