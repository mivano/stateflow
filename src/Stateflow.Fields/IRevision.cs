using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{
	public interface IRevision<TIdentifier>{

		IFieldsTemplate<TIdentifier> FieldsTemplate { get; }

		object GetCurrentFieldValue(IFieldDefinition<TIdentifier> fieldDefinition);
		object GetOriginalFieldValue(IFieldDefinition<TIdentifier> fieldDefinition);

		void SetFieldValue(IFieldDefinition<TIdentifier> fieldDefinition, object value);

		bool IsReadOnly { get; }
		bool IsDraft { get; }

		int Number { get; }
	}

}
