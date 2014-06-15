using System;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	public class MinimumItemsInCollectionValidator<TIdentifier> : IFieldValidator<TIdentifier>
	{
		int _minimumAmount;

		public MinimumItemsInCollectionValidator (int minimumAmount)
		{
			_minimumAmount = minimumAmount;
			
		}

		#region IFieldValidator implementation

		public bool IsValid(IField<TIdentifier> field)
		{
			if (field.FieldDefinition.FieldType != FieldType.FieldCollection)
				throw new InvalidProgramException ("The minimum items in collection validator only works on fields of the type FieldCollection.");

			var v = field.Value as Dictionary<TIdentifier, IField<TIdentifier>>.ValueCollection;

			if (v == null)
				throw new InvalidProgramException ("The minimum items in collection validator only works on fields of the type FieldCollection.");

			return v.Count () <= _minimumAmount;

		}

		#endregion

	}
}
