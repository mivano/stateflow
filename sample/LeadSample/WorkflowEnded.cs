using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stateflow.Workflow;

namespace LeadSample
{
	public class WorkflowEnded: IAction
    {
        public void Execute(IWorkflow workflow)
        {
            Console.WriteLine("Workflow ended: {0}", workflow.GetType().Name);
        }
    }
}
