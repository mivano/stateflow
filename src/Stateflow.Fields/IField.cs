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
		object Value { get; set; }

		/// <summary>
		/// The default value used when creating this field.
		/// </summary>
		/// <value>The default value.</value>
		object DefaultValue {get;set;}

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

		IEnumerable<IFieldValidator<TIdentifier>> Validators{ get; set; }

		int Sequence { get; set; }
	}

}

