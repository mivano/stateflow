using System;
using System.Collections.Generic;
using Stateflow.Fields;
using Stateflow.Workflow;
using Stateflow.Fields.DataStores;

namespace LeadSample
{
	public class Lead : IIdentifiableBy<string>
	{
	    private readonly WorkflowEngine _workflowEngine;

	    public Lead(WorkflowDefinition workflowDefinition)
		{
            _workflowEngine = new WorkflowEngine(workflowDefinition, null);
            _workflowEngine.OnStateTransition += WorkflowEngine_OnStateTransition;
		}

        void WorkflowEngine_OnStateTransition(object sender, StateChangeEventArgs e)
        {
            Console.WriteLine("{0}: State changed from {1} to {2} because of trigger {3}.", DateTime.Now.ToShortTimeString(), e.FromState, e.ToState, e.TriggeredBy);
        }

        public virtual IEnumerable<string> PermittedTriggers
        {
            get { return _workflowEngine.PermittedTriggers; }
        }

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value>
        /// The state of the workflow.
        /// </value>
        public virtual string WorkflowState {
            get { return _workflowEngine.WorkflowState; }
            set { _workflowEngine.WorkflowState = value; }
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