using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Utils;
using Stateless;

namespace Stateflow.Workflow
{
				

	/// <summary>
	/// This engine class that can be used to add workflow capabilities to a class.
	/// </summary>
	public class WorkflowEngine : IWorkflow
	{
		/// <summary>
		/// This event is raised when a state transition.
		/// </summary>
		public event StateChangeHandler OnStateTransition;

		private StateMachine<State, Trigger> _stateMachine;
		private WorkflowDefinition _workflowDefinition;
		private readonly object _workflowContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowEngine"/> class.
		/// </summary>
		/// <param name="workflowDefinition">Workflow definition.</param>
		public WorkflowEngine (WorkflowDefinition workflowDefinition): this(workflowDefinition, null)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowEngine"/> class.
		/// </summary>
		/// <param name="workflowDefinition">Workflow definition.</param>
		/// <param name="currentState">Current state.</param>
		public WorkflowEngine (WorkflowDefinition workflowDefinition, State currentState): this(workflowDefinition, currentState, null)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowEngine"/> class.
		/// </summary>
		/// <param name="workflowDefinition">The workflow definition.</param>
		/// <param name="currentState">Current state of the workflow. If null, it will be first state found in the workflow definition.</param>
		/// <param name="workflowContext">The context that the workflow engine is bound to.</param>
		public WorkflowEngine (WorkflowDefinition workflowDefinition, State currentState, object workflowContext)
		{
			_workflowContext = workflowContext;
			ToStateMachine (workflowDefinition, currentState);
		}

		private void ToStateMachine(WorkflowDefinition workflowDefinition, State currentState)
		{
			Enforce.ArgumentNotNull (workflowDefinition, "workflowDefinition");

			_workflowDefinition = workflowDefinition;

			CurrentState = currentState ?? workflowDefinition.Transitions.First ().FromState;
			_stateMachine = new StateMachine<State, Trigger> (() => CurrentState, s => CurrentState = s);

			//  Get a distinct list of states with a trigger from state configuration
			//  "State => Trigger => TargetState
			var states = workflowDefinition.Transitions.AsQueryable ()
				.Select (x => x.FromState)
				.Distinct ()
				.Select (x => x)
				.ToList ();

			//  Assign triggers to states
			states.ForEach (state => {
				var triggers = workflowDefinition.Transitions.AsQueryable ()
					.Where (config => config.FromState == state)
					.Select (config => new { 
							Trigger = config.TriggerBy, 
							TargetState = config.ToState, 
							Condition = config.Condition 
						})
					.ToList ();

				triggers.ForEach (trig => {
					if (trig.Condition == null)
						_stateMachine.Configure (state).Permit (trig.Trigger, trig.TargetState);
					else
						_stateMachine.Configure (state)
							.PermitIf (trig.Trigger, trig.TargetState, ConditionalGuard (trig.Condition));


				});

				var ws =state;
				if (ws.EntryAction != null) {
					_stateMachine.Configure (state).OnEntry (() => ExecuteAction (ws.EntryAction));
				}

				if (ws.ExitAction != null) {
					_stateMachine.Configure (state).OnExit (() => ExecuteAction (ws.ExitAction));
				}

			});

			// For all the state transitions
			_stateMachine.OnTransitioned (OnTransitionAction);

		}

		/// <summary>
		/// When the state is transitioned.
		/// </summary>
		/// <param name="transition"></param>
		protected virtual void OnTransitionAction(StateMachine<State, Trigger>.Transition transition)
		{
			if (OnStateTransition != null) {

				// Find the states
				var sourceState = transition.Source;  
				var destinationState = transition.Destination;  

				if (sourceState == null)
					throw new InvalidOperationException (string.Format ("Unable to find the source state {0} in the list of available states.", transition.Source));

				if (destinationState == null)
					throw new InvalidOperationException (string.Format ("Unable to find the destination state {0} in the list of available states.", transition.Destination));

				// Find the trigger
				var trigger = transition.Trigger;

				if (trigger == null)
					throw new InvalidOperationException (string.Format ("Unable to find the trigger {0} in the list of available triggers.", transition.Trigger));


				OnStateTransition (this, new StateChangeEventArgs (sourceState, destinationState, trigger, transition.IsReentry));
			}
		}

		private void ExecuteAction(IAction entryAction)
		{
			entryAction.Execute (this);
				}

		private Func<bool> ConditionalGuard(ICondition condition)
		{
			return () => condition.IsAllowed (this);
		}

		/// <summary>
		/// Changes the current state to a new state using the name of the trigger.
		/// </summary>
		/// <param name="trigger">Trigger.</param>
		public virtual void ChangeState(string trigger)
		{
			Enforce.ArgumentNotNull (trigger, "trigger");

			Trigger t;

			if (_workflowDefinition.Triggers.TryGetValue (trigger, out t)) {
				ChangeState (t);
			}
		}

		/// <summary>
		/// Changes the current state to a new state.
		/// </summary>
		/// <param name="trigger">The new state/trigger.</param>
		public virtual void ChangeState(Trigger trigger)
		{
			Enforce.ArgumentNotNull (trigger, "trigger");

			_stateMachine.Fire (trigger);
		}

		/// <summary>
		/// Determines whether this workflow can change its state based on the trigger.
		/// </summary>
		/// <param name="trigger">The new state/trigger.</param>
		/// <returns></returns>
		public virtual bool CanChangeState(Trigger trigger)
		{
			Enforce.ArgumentNotNull (trigger, "trigger");

			return _stateMachine.CanFire (trigger);
		}

		/// <summary>
		/// Gets the permitted triggers based on the current state and the allowed triggers for this state.
		/// </summary>
		/// <value>
		/// The permitted triggers.
		/// </value>
		public virtual IEnumerable<Trigger> PermittedTriggers {
			get { return _stateMachine.PermittedTriggers; }
		}

		/// <summary>
		/// Gets or sets the state of the workflow.
		/// </summary>
		/// <value>
		/// The state of the workflow.
		/// </value>
		public virtual State CurrentState { get; set; }


		/// <summary>
		/// The context that the workflow engine is bound to.
		/// </summary>
		public virtual object Context {
			get { return _workflowContext; }
		}
	}
}