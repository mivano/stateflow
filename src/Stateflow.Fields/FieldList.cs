using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	/// <summary>
	/// A field that maintains a list of other fields.
	/// </summary>
	public class FieldList<TIdentifier>: Field<TIdentifier>{
	
		public FieldList (IRevision<TIdentifier> revision, IFieldDefinition<TIdentifier> fieldDefinition):base(revision, fieldDefinition)
		{
			
		}

		public override IFieldDefinition<TIdentifier> FieldDefinition {
			get {
				return base.FieldDefinition as FieldListDefinition<TIdentifier>;
			}
		}

		public override FieldType FieldType {
			get {
				return FieldType.List;
			}
		}

	}

}
