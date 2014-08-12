using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Utils;
using Stateless;

namespace Stateflow.Workflow
{
	/// <summary>
	/// Use the workflow entity base class to implement workflow on sub classes. 
	/// You can also use the WorkflowEngine directly inside your code.
	/// </summary>
	public abstract class WorkflowEntity: IWorkflow
	{
		private readonly WorkflowEngine _workflowEngine;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowEntity"/> class.
		/// </summary>
		/// <param name="workflowDefinition">Workflow definition.</param>
		protected WorkflowEntity(WorkflowDefinition workflowDefinition)
		{
			_workflowEngine = new WorkflowEngine(workflowDefinition, null, this);
			_workflowEngine.OnStateTransition += WorkflowEngine_OnStateTransition;
		}

		void WorkflowEngine_OnStateTransition(object sender, StateChangeEventArgs e)
		{
			OnStateTransition (e.FromState, e.ToState, e.TriggeredBy, e.IsReentry);
		}

		/// <summary>
		/// Implement this method to react on state transitions.
		/// </summary>
		/// <param name="source">Source.</param>
		/// <param name="destination">Destination.</param>
		/// <param name="triggeredBy">Triggered by.</param>
		/// <param name="isReentry">If set to <c>true</c> is reentry.</param>
		protected abstract void OnStateTransition(State source, State destination, Trigger triggeredBy, bool isReentry);

		/// <summary>
		/// Determines whether this workflow can change its state based on the trigger.
		/// </summary>
		/// <param name="trigger">The trigger.</param>
		/// <returns></returns>
		public bool CanChangeState(string trigger)
		{
			return _workflowEngine.CanChangeState (trigger);
		}

		/// <summary>
		/// Gets the permitted triggers from this step.
		/// </summary>
		/// <value>The permitted triggers.</value>
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

		/// <summary>
		/// Change to a new state.
		/// </summary>
		/// <param name="newState">New state.</param>
		public virtual void ChangeState(string newState)
		{
			_workflowEngine.ChangeState(newState);
		}

		/// <summary>
		/// Gets the underlying work flow engine.
		/// </summary>
		/// <value>The work flow engine.</value>
		public WorkflowEngine WorkFlowEngine {
			get { return _workflowEngine; }
		}


		/// <summary>
		/// Provides the context workflow is bound to.
		/// </summary>
		public virtual object Context {
			get { return _workflowEngine.Context; }
		}
	}
    
}