using System;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	public class NotNullValidator<TIdentifier>: IFieldValidator<TIdentifier>
	{
		#region IFieldValidator implementation

		public bool IsValid(IField<TIdentifier> field)
		{
			return field.Value != null;
		}

		#endregion

	}

}
