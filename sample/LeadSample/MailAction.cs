using System;
using Stateflow.Workflow;

namespace LeadSample
{
    public class MailAction : IAction
    {
        public void Execute(IWorkflow workflow)
        {
            Console.WriteLine("Mail action executed: {0}", workflow.GetType().Name);
        }
    }
}