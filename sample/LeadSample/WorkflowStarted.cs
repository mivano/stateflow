using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stateflow.Workflow;

namespace LeadSample
{
	public class WorkflowStarted: IAction
	{
		public void Execute(IWorkflow workflow)
		{
			Console.WriteLine("Workflow started: {0}", workflow.GetType().Name);
		}
	}
}
