using System.Collections.Generic;
using System;
using System.Linq;

namespace Stateflow.Workflow.Conditions
{

	/// <summary>
	/// Combines multiple conditions together. All conditions needs to be true to allow the transition to pass.
	/// </summary>
	public class CombinedConditions<TIdentifier>: ICondition<TIdentifier>{

		readonly IEnumerable<ICondition<TIdentifier>> _conditions;

		public CombinedConditions(IEnumerable<ICondition<TIdentifier>> conditions)
		{
			if (conditions == null) throw new ArgumentNullException("conditions");
			_conditions = conditions;
		}

		#region ICondition implementation

		public bool IsAllowed(IWorkflow<TIdentifier> workflow)
		{
			return _conditions.Any (c => c.IsAllowed (workflow));
		}

		#endregion


	}


}