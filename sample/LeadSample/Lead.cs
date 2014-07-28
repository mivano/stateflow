using System;
using System.Collections.Generic;
using Stateflow.Fields;
using Stateflow.Workflow;
using Stateflow.Fields.DataStores;

namespace LeadSample
{
	public class Lead : WorkflowEntity, IIdentifiableBy<string>
	{
		public Lead(WorkflowDefinition workflowDefinition, string currentState)
			: base(workflowDefinition, currentState)
		{
			//Fields = new FieldCollection<string> ();
		}

		protected override void OnStateTransition(string fromState, string toState, string triggeredBy, bool workflowStarted, bool workflowEnded)
		{
			if (workflowStarted) {
				Console.WriteLine("Workflow has started with {0}.", fromState);				
			}

			if (workflowEnded) {
				Console.WriteLine("Workflow has ended with {0}.", toState);
			}

			Console.WriteLine("{0}: State changed from {1} to {2} because of trigger {3}.", DateTime.Now.ToShortTimeString(), fromState, toState, triggeredBy);
		}

		public FieldCollection<string> Fields { get; set; }

		public string Id
		{
			get;
			set;
		}
	}
}