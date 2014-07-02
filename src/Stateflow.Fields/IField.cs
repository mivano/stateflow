using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields.DataStores
{
	public interface IField<TIdentifier>
	{
		 event ValueChangedEventHandler ValueChanged;

		/// <summary>
		/// Gets or sets the field definition.
		/// </summary>
		/// <value>The field definition.</value>
		IFieldDefinition<TIdentifier> FieldDefinition {get;}

		IEnumerable AllowedValues {get;}
	
		bool IsValid {get;}
		bool IsDirty { get; }
		bool IsEditable{ get; }

		 TIdentifier Id { get; }


		string Name { get; }
		string ReferenceName { get; }

		Object OriginalValue { get; }

		Object Value { get; set; }

		Object DefaultValue { get; }

		FieldStatus Status { get; }

		int Sequence { get; set; }
	}


}

