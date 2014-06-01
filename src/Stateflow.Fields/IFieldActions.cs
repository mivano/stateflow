using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields
{

	public interface IFieldActions{

		/// <summary>
		/// Determines whether this field is valid based on the validators specified.
		/// </summary>
		/// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
		bool IsValid();
	}
}
