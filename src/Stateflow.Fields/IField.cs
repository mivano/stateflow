using System;
using System.Collections;

namespace Stateflow.Fields
{
	public interface IField<TIdentifier>
	{
		event ValueChangedEventHandler ValueChanged;

		/// <summary>
		/// Gets or sets the field definition this instance is based on.
		/// </summary>
		/// <value>The field definition.</value>
		IFieldDefinition<TIdentifier> FieldDefinition { get; }

		IEnumerable AllowedValues { get; }

		bool IsValid { get; }

		bool IsDirty { get; }

		bool IsEditable{ get; }

		TIdentifier Id { get; }

		/// <summary>
		/// The name of the field.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>
		/// A system name for this field.
		/// </summary>
		/// <value>The name of the reference.</value>
		string ReferenceName { get; }

		Object OriginalValue { get; }

		Object Value { get; set; }

		Object DefaultValue { get; }

		FieldStatus Status { get; }

	}


}

