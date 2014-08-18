using System;
using System.Collections.Generic;
using Stateflow.Fields;
using Stateflow.Workflow;
using Stateflow.Fields.DataStores;

namespace LeadSample
{
	public class Lead : IIdentifiableBy<string>
	{
		private readonly WorkflowEngine<string> _workflowEngine;

		public Lead(WorkflowDefinition<string> workflowDefinition)
		{
			_workflowEngine = new WorkflowEngine<string>(workflowDefinition, null, this);
            _workflowEngine.OnStateTransition += WorkflowEngine_OnStateTransition;
		}

		void WorkflowEngine_OnStateTransition(object sender, StateChangeEventArgs<string> e)
        {
			if (e.FromState is StartState<string>)
				Console.WriteLine ("Workflow started");

			if (e.ToState is EndState<string>)
				Console.WriteLine ("Workflow completed");

            Console.WriteLine("{0}: State changed from {1} to {2} because of trigger {3}.", DateTime.Now.ToShortTimeString(), e.FromState, e.ToState, e.TriggeredBy);
        }

		public virtual IEnumerable<Trigger<string>> PermittedTriggers
        {
            get { return _workflowEngine.PermittedTriggers; }
        }

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value>
        /// The state of the workflow.
        /// </value>
		public virtual State<string> CurrentState {
			get { return _workflowEngine.CurrentState; }
        }

	    public virtual void ChangeState(string newState)
	    {
	        _workflowEngine.ChangeState(newState);
	    }

		public IFieldsItem<string> Fields { get; set; }

		public string Id
		{
			get;
			set;
		}
	}
}