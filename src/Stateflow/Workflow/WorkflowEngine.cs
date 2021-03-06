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
	public class WorkflowEngine<TIdentifier> : IWorkflow<TIdentifier>
	{
		/// <summary>
		/// This event is raised when a state transition.
		/// </summary>
		public event StateChangeHandler<TIdentifier> OnStateTransition;

		/// <summary>
		/// Occurs when a trigger is unhandled.
		/// </summary>
		public event UnhandledTriggerHandler<TIdentifier> OnTriggerUnhandled;

		private StateMachine<State<TIdentifier>, Trigger<TIdentifier>> _stateMachine;
		private WorkflowDefinition<TIdentifier> _workflowDefinition;
		private readonly object _workflowContext;


		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowEngine`1"/> class.
		/// </summary>
		/// <param name="workflowDefinition">Workflow definition.</param>
		public WorkflowEngine (WorkflowDefinition<TIdentifier> workflowDefinition) : this (workflowDefinition, null)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowEngine"/> class.
		/// </summary>
		/// <param name="workflowDefinition">Workflow definition.</param>
		/// <param name="currentState">Current state.</param>
		public WorkflowEngine (WorkflowDefinition<TIdentifier> workflowDefinition, State<TIdentifier> currentState) : this (workflowDefinition, currentState, null)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowEngine"/> class.
		/// </summary>
		/// <param name="workflowDefinition">The workflow definition.</param>
		/// <param name="currentState">Current state of the workflow. If null, it will be first state found in the workflow definition.</param>
		/// <param name="workflowContext">The context that the workflow engine is bound to.</param>
		public WorkflowEngine (WorkflowDefinition<TIdentifier> workflowDefinition, State<TIdentifier> currentState, object workflowContext)
		{
			_workflowContext = workflowContext;
			ToStateMachine (workflowDefinition, currentState);
		}

		private void ToStateMachine(WorkflowDefinition<TIdentifier> workflowDefinition, State<TIdentifier> currentState)
		{
			Enforce.ArgumentNotNull (workflowDefinition, "workflowDefinition");

			_workflowDefinition = workflowDefinition;

			CurrentState = currentState ?? workflowDefinition.Transitions.First ().FromState;
			_stateMachine = new StateMachine<State<TIdentifier>, Trigger<TIdentifier>> (() => CurrentState, s => CurrentState = s);

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
							Condition = config.Condition,
							IsReentrant = config.IsReentrant
						})
					.ToList ();

				var stateConfig = _stateMachine.Configure(state);

				triggers.ForEach (trig => {
					if (trig.Condition == null)
					{
						if (trig.IsReentrant)
							stateConfig.PermitReentry(trig.Trigger);
						else
						stateConfig.Permit (trig.Trigger, trig.TargetState);
					}
					else
						stateConfig.PermitIf (trig.Trigger, trig.TargetState, ConditionalGuard (trig.Condition));

				});

				if (state.SuperState!=null)
					stateConfig.SubstateOf(state.SuperState);

				if (state.EntryAction != null) {
					stateConfig.OnEntry (t => ExecuteAction (t, state.EntryAction));
				}

				if (state.ExitAction != null) {
					stateConfig.OnExit(t => ExecuteAction (t, state.ExitAction));
				}

			});



			// Handle exceptions
			_stateMachine.OnUnhandledTrigger (OnUnhandledTrigger);

			// For all the state transitions
			_stateMachine.OnTransitioned (OnTransitionAction);

		}

		/// <summary>
		/// Raises the unhandled trigger event.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="trigger">Trigger.</param>
		protected virtual void OnUnhandledTrigger(State<TIdentifier> state, Trigger<TIdentifier> trigger)
		{
			if (OnTriggerUnhandled != null) {
				OnTriggerUnhandled (this, new UnhandledTriggerEventArgs<TIdentifier> (state, trigger));
			}
		}

		/// <summary>
		/// When the state is transitioned.
		/// </summary>
		/// <param name="transition"></param>
		protected virtual void OnTransitionAction(StateMachine<State<TIdentifier>, Trigger<TIdentifier>>.Transition transition)
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


				OnStateTransition (this, new StateChangeEventArgs<TIdentifier> (sourceState, destinationState, trigger, transition.IsReentry));
			}
		}

		private void ExecuteAction(StateMachine<State<TIdentifier>, Trigger<TIdentifier>>.Transition transition, IAction<TIdentifier> entryAction)
		{
			if (entryAction == null)
				return;

			// Convert transition to a more generic class so there is no need for a dependency on Stateless
			StateChangeEventArgs<TIdentifier> e = null;
			if (transition != null)
				e = new StateChangeEventArgs<TIdentifier> (transition.Source, transition.Destination, transition.Trigger, transition.IsReentry);

			entryAction.Execute (this, e);
		}

		private Func<bool> ConditionalGuard(ICondition<TIdentifier> condition)
		{
			return () => condition.IsAllowed (this);
		}

		/// <summary>
		/// Changes the current state to a new state using the identifier of the trigger.
		/// </summary>
		/// <param name="trigger">Trigger.</param>
		public virtual void ChangeState(TIdentifier trigger)
		{

			Trigger<TIdentifier> t;

			if (_workflowDefinition.Triggers.TryGetValue (trigger, out t)) {
				ChangeState (t);
			}
		}

		/// <summary>
		/// Changes the current state to a new state.
		/// </summary>
		/// <param name="trigger">The new state/trigger.</param>
		public virtual void ChangeState(Trigger<TIdentifier> trigger)
		{
			Enforce.ArgumentNotNull (trigger, "trigger");

			_stateMachine.Fire (trigger);
		}

		/// <summary>
		/// Determines whether this workflow can change its state based on the trigger.
		/// </summary>
		/// <param name="trigger">The new state/trigger.</param>
		/// <returns></returns>
		public virtual bool CanChangeState(Trigger<TIdentifier> trigger)
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
		public virtual IEnumerable<Trigger<TIdentifier>> PermittedTriggers {
			get { return _stateMachine.PermittedTriggers; }
		}

		/// <summary>
		/// Gets or sets the state of the workflow.
		/// </summary>
		/// <value>
		/// The state of the workflow.
		/// </value>
		public virtual State<TIdentifier> CurrentState { get; set; }


		/// <summary>
		/// The context that the workflow engine is bound to.
		/// </summary>
		public virtual object Context {
			get { return _workflowContext; }
		}
	}
}