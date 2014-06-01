using System;
using System.Collections.Generic;
using Stateflow.Workflow;
using Stateflow.Fields;

namespace LeadSample
{
    public class Lead : WorkflowEntity 
    {
        public Lead(WorkflowDefinition workflowDefinition, string currentState) : base(workflowDefinition, currentState)
        {
			Fields = new FieldCollection<string> ();
        }

        protected override void OnStateTransition(string fromState, string toState, string triggeredBy)
        {
            Console.WriteLine("State changed from {0} to {1} because of trigger {2}.", fromState, toState, triggeredBy);
        }

		public FieldCollection<string> Fields { get; set; }
    }
}