using System.Collections.Generic;
using System;

namespace Stateflow.Workflow.Conditions
{

	public class DelegateCondition<TIdentifier> : ICondition<TIdentifier>
	{
		readonly Func<IWorkflow<TIdentifier>, bool> _isAllowed;

		public DelegateCondition(Func<IWorkflow<TIdentifier>, bool> isAllowed)
		{
			if (isAllowed == null) throw new ArgumentNullException("isAllowed");
			_isAllowed = isAllowed;
		}

		#region ICondition implementation

		public bool IsAllowed(IWorkflow<TIdentifier> workflow)
		{
			if (workflow == null) throw new ArgumentNullException("workflow");
			return _isAllowed(workflow);
		}

		#endregion

	}
}