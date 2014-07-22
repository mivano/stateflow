using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Fields;
using Stateflow.Workflow;
using Stateflow.Expressions;
using Stateflow.Fields.DataStores;

namespace LeadSample
{

	public class LeadExpressionCondition : ExpressionCondition
	{

		public LeadExpressionCondition(string expression)
			: base(expression)
		{

		}

		public override bool IsAllowed(IWorkflow workflow)
		{
			var leadWorkflow = workflow as Lead;
			Dictionary<string, object> fields = null;
			if (leadWorkflow != null)
			{
				fields = leadWorkflow.Fields.Fields.Values
					.Select(a => new KeyValuePair<string, object>(a.Name, a.Value))
					.ToDictionary(a => a.Key, b => b.Value);
			}
			return base.Evaluate(fields);
		}

	}
}
