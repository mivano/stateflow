using System;
using System.Collections.Generic;
using Stateflow.Workflow;

namespace LeadSample
{
    public class Lead : WorkflowEntity 
    {
        public Lead(WorkflowDefinition workflowDefinition, string currentState) : base(workflowDefinition, currentState)
        {
            
        }

        protected override void OnStateTransition(string fromState, string toState, string triggeredBy)
        {
            Console.WriteLine("State changed from {0} to {1} because of trigger {2}.", fromState, toState, triggeredBy);
        }

        public List<Field> Fields { get; set; }
    }
}