using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	public interface IFieldDefinition<TIdentifier> : IIdentifiableBy<TIdentifier>{

		/// <summary>
		/// The type of field to use.
		/// </summary>
		/// <value>The type of the field.</value>
		FieldType FieldType{ get; set; }

		IEnumerable AllowedValues {get;set;}

		bool IsComputed { get; set; }

		bool IsEditable { get; set; }

		/// <summary>
		/// The default value used when creating this field.
		/// </summary>
		/// <value>The default value.</value>
		object DefaultValue {get;set;}

		/// <summary>
		/// A friendly name of the field.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; set; }

		/// <summary>
		/// A reference name for this field.
		/// </summary>
		/// <value>The name of the reference.</value>
		string ReferenceName { get; set; }

		/// <summary>
		/// A description of this field.
		/// </summary>
		/// <value>The description.</value>
		string Description { get; set; }

		/// <summary>
		/// The underlying system type.
		/// </summary>
		/// <value>The type of the system.</value>
		System.Type SystemType { get; set; }

		IFieldOptions FieldOptions { get; set; }

		IEnumerable<IFieldValidator<TIdentifier>> Validators{ get; set; }

		/// <summary>
		/// Additional metadata to be used to decorate the field definition with more information.
		/// </summary>
		/// <value>The meta data.</value>
		IDictionary<string, object> MetaData { get; set;}
	}
}
