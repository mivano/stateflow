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
	public abstract class WorkflowEntity<TIdentifier>: IWorkflow<TIdentifier>
	{
		private readonly WorkflowEngine<TIdentifier> _workflowEngine;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowEntity"/> class.
		/// </summary>
		/// <param name="workflowDefinition">Workflow definition.</param>
		protected WorkflowEntity(WorkflowDefinition<TIdentifier> workflowDefinition)
		{
			_workflowEngine = new WorkflowEngine<TIdentifier>(workflowDefinition, null, this);
			_workflowEngine.OnStateTransition += WorkflowEngine_OnStateTransition;
		}

		void WorkflowEngine_OnStateTransition(object sender, StateChangeEventArgs<TIdentifier> e)
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
		protected abstract void OnStateTransition(State<TIdentifier> source, State<TIdentifier> destination, Trigger<TIdentifier> triggeredBy, bool isReentry);



		/// <summary>
		/// Determines whether this workflow can change its state based on the trigger.
		/// </summary>
		/// <param name="trigger">The trigger.</param>
		/// <returns></returns>
		public bool CanChangeState(Trigger<TIdentifier> trigger)
		{
			return _workflowEngine.CanChangeState (trigger);
		}

		/// <summary>
		/// Gets the permitted triggers from this step.
		/// </summary>
		/// <value>The permitted triggers.</value>
		public virtual IEnumerable<Trigger<TIdentifier>> PermittedTriggers
		{
			get { return _workflowEngine.PermittedTriggers; }
		}

		/// <summary>
		/// Gets or sets the state of the workflow.
		/// </summary>
		/// <value>
		/// The state of the workflow.
		/// </value>
		public virtual State<TIdentifier> CurrentState {
			get { return _workflowEngine.CurrentState; }
		}

		/// <summary>
		/// Change to a new state.
		/// </summary>
		/// <param name="trigger">New state.</param>
		public virtual void ChangeState(Trigger<TIdentifier> trigger)
		{
			_workflowEngine.ChangeState(trigger);
		}

		/// <summary>
		/// Changes the current state to a new state.
		/// </summary>
		/// <param name="trigger">The trigger to that force the move to a new state.</param>
		public void ChangeState(TIdentifier trigger)
		{
			_workflowEngine.ChangeState(trigger);
		}

		/// <summary>
		/// Gets the underlying work flow engine.
		/// </summary>
		/// <value>The work flow engine.</value>
		public WorkflowEngine<TIdentifier> WorkFlowEngine {
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