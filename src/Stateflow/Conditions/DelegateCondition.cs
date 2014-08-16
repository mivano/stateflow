using System.Collections.Generic;
using System;

namespace Stateflow.Workflow.Conditions
{

	public class DelegateCondition : ICondition
	{
		readonly Func<IWorkflow, bool> _isAllowed;

		public DelegateCondition(Func<IWorkflow, bool> isAllowed)
		{
			if (isAllowed == null) throw new ArgumentNullException("isAllowed");
			_isAllowed = isAllowed;
		}

		#region ICondition implementation

		public bool IsAllowed(IWorkflow workflow)
		{
			if (workflow == null) throw new ArgumentNullException("workflow");
			return _isAllowed(workflow);
		}

		#endregion

	}
}