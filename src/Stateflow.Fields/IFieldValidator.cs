using System;
using System.Collections;
using System.Collections.Generic;

namespace Stateflow.Fields
{

	public interface IFieldValidator<TIdentifier>
	{
		bool IsValid(IField<TIdentifier> field);
	}
}
