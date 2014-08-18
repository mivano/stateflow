using System;
using Stateflow.Workflow;

namespace LeadSample
{
	public class MailAction : IAction<string>
    {
		public void Execute(IWorkflow<string> workflow, StateChangeEventArgs<string> transition)
        {
            Console.WriteLine("Mail action executed: {0}. Context: {1}", workflow.GetType().Name, workflow.Context.GetType().Name);
        }
    }

	public class RejectAction : IAction<string>
	{
		public void Execute(IWorkflow<string> workflow, StateChangeEventArgs<string> transition)
		{
			Console.WriteLine("Rejected action");
		}
	}

}