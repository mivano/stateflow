using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Utils;
using Stateless;

namespace Stateflow.Workflow
{

	/// <summary>
	/// This event is raised when the state is transitioned.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The <see cref="StateChangeEventArgs"/> instance containing the event data.</param>
	public delegate void StateChangeHandler(object sender, StateChangeEventArgs e);
				
	/// <summary>
	/// Contains the event arguments.
	/// </summary>
	public class StateChangeEventArgs : EventArgs
	{
		private readonly State _fromState;
		private readonly State _toState;
		private readonly Trigger _triggeredBy;
		private readonly bool _isReentry;

		/// <summary>
		/// Initializes a new instance of the <see cref="StateChangeEventArgs"/> class.
		/// </summary>
		/// <param name="fromState">From state.</param>
		/// <param name="toState">To state.</param>
		/// <param name="triggeredBy">The triggered by.</param>
		/// <param name="isReentry"></param>
		public StateChangeEventArgs(State fromState, State toState, Trigger triggeredBy, bool isReentry)
		{
			_fromState = fromState;
			_toState = toState;
			_triggeredBy = triggeredBy;
			_isReentry = isReentry;
		}


		/// <summary>
		/// Gets from state.
		/// </summary>
		/// <value>
		/// From state.
		/// </value>
		public State FromState
		{
			get { return _fromState; }
		}

		/// <summary>
		/// Gets to state.
		/// </summary>
		/// <value>
		/// To state.
		/// </value>
		public State ToState
		{
			get { return _toState; }
		}

		/// <summary>
		/// Gets the triggered by value.
		/// </summary>
		/// <value>
		/// The triggered by.
		/// </value>
		public Trigger TriggeredBy
		{
			get { return _triggeredBy; }
		}

		/// <summary>
		/// Gets a value indicating whether is a reentry.
		/// </summary>
		/// <value>
		///   <c>true</c> if [is reentry]; otherwise, <c>false</c>.
		/// </value>
		public bool IsReentry
		{
			get { return _isReentry; }
		}
	}

	/// <summary>
	/// This engine class that can be used to add workflow capabilities to a class.
	/// </summary>
	public class WorkflowEngine : IWorkflow
	{
		/// <summary>
		/// This event is raised when a state transition.
		/// </summary>
		public event StateChangeHandler OnStateTransition;

		private StateMachine<string, string> _stateMachine;
		private WorkflowDefinition _workflowDefinition;
		private readonly object _workflowContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowEngine"/> class.
		/// </summary>
		/// <param name="workflowDefinition">The workflow definition.</param>
		/// <param name="currentState">Current state of the workflow. If null, it will be first state found in the workflow definition.</param>
		/// <param name="workflowContext">The context that the workflow engine is bound to.</param>
		public WorkflowEngine(WorkflowDefinition workflowDefinition, string currentState, object workflowContext)
		{
			_workflowContext = workflowContext;
			ToStateMachine(workflowDefinition, currentState);
		}

		private void ToStateMachine(WorkflowDefinition workflowDefinition, string currentState)
		{
			Enforce.ArgumentNotNull(workflowDefinition, "workflowDefinition");

			_workflowDefinition = workflowDefinition;

			WorkflowState = currentState ?? workflowDefinition.Transitions.First().FromState;
			_stateMachine = new StateMachine<string, string>(() => WorkflowState, s => WorkflowState = s);

			//  Get a distinct list of states with a trigger from state configuration
			//  "State => Trigger => TargetState
			var states = workflowDefinition.Transitions.AsQueryable()
				.Select(x => x.FromState)
				.Distinct()
				.Select(x => x)
				.ToList();

			//  Assign triggers to states
			states.ForEach(state =>
			{
				var triggers = workflowDefinition.Transitions.AsQueryable()
					.Where(config => config.FromState == state)
					.Select(config => new { 
							Trigger = config.TriggerBy, 
							TargetState = config.ToState, 
							Conditions = config.Conditions 
						})
					.ToList();

				triggers.ForEach(trig =>
				{
					if (trig.Conditions == null || trig.Conditions.Any() == false)
						_stateMachine.Configure(state).Permit(trig.Trigger, trig.TargetState);
					else
						_stateMachine.Configure(state)
							.PermitIf(trig.Trigger, trig.TargetState, ConditionalGuard(trig.Conditions));


				});

				var ws = workflowDefinition.States.First(a => a.Name == state);
				if (ws.EntryActions != null)
				{
					_stateMachine.Configure(state).OnEntry(() => ExecuteActions(ws.EntryActions));
				}

				if (ws.ExitActions != null)
				{
					_stateMachine.Configure(state).OnExit(() => ExecuteActions(ws.ExitActions));
				}

			});

			// For all the state transitions
			_stateMachine.OnTransitioned(OnTransitionAction);

		}

		/// <summary>
		/// When the state is transitioned.
		/// </summary>
		/// <param name="transition"></param>
		protected virtual void OnTransitionAction(StateMachine<string, string>.Transition transition)
		{
			if (OnStateTransition != null)
			{

				// Find the states
				var sourceState =_workflowDefinition.States.FirstOrDefault(a=>a.Name.Equals(transition.Source, StringComparison.InvariantCultureIgnoreCase));  
				var destinationState =_workflowDefinition.States.FirstOrDefault(a=>a.Name.Equals(transition.Destination, StringComparison.InvariantCultureIgnoreCase));  

				if (sourceState==null)
					throw new InvalidOperationException(string.Format("Unable to find the source state {0} in the list of available states.", transition.Source));

				if (destinationState==null)
					throw new InvalidOperationException(string.Format("Unable to find the destination state {0} in the list of available states.", transition.Destination));

				// Find the trigger
				var trigger = _workflowDefinition.Triggers.FirstOrDefault(a=>a.Name.Equals(transition.Trigger,  StringComparison.InvariantCultureIgnoreCase));

				if (trigger==null)
					throw new InvalidOperationException(string.Format("Unable to find the trigger {0} in the list of available triggers.", transition.Trigger));


				OnStateTransition (this, new StateChangeEventArgs (sourceState, destinationState, trigger, transition.IsReentry));
			}
		}
																  
		private void ExecuteActions(IEnumerable<IAction> entryActions)
		{
			foreach (var entryAction in entryActions)
			{
				entryAction.Execute(this);
			}
		}

		private Func<bool> ConditionalGuard(IEnumerable<ICondition> conditions)
		{
			return () => conditions.Any(c => c.IsAllowed(this));
		}


		/// <summary>
		/// Changes the current state to a new state.
		/// </summary>
		/// <param name="trigger">The new state/trigger.</param>
		public virtual void ChangeState(string trigger)
		{
			Enforce.ArgumentNotNull(trigger, "trigger");

			_stateMachine.Fire(trigger);
		}

		/// <summary>
		/// Determines whether this workflow can change its state based on the trigger.
		/// </summary>
		/// <param name="trigger">The new state/trigger.</param>
		/// <returns></returns>
		public virtual bool CanChangeState(string trigger)
		{
			Enforce.ArgumentNotNull(trigger, "trigger");

			return _stateMachine.CanFire(trigger);
		}

		/// <summary>
		/// Gets the permitted triggers based on the current state and the allowed triggers for this state.
		/// </summary>
		/// <value>
		/// The permitted triggers.
		/// </value>
		public virtual IEnumerable<string> PermittedTriggers
		{
			get { return _stateMachine.PermittedTriggers; }
		}

		/// <summary>
		/// Gets or sets the state of the workflow.
		/// </summary>
		/// <value>
		/// The state of the workflow.
		/// </value>
		public virtual string WorkflowState { get; set; }


		/// <summary>
		/// The context that the workflow engine is bound to.
		/// </summary>
		public virtual object Context
		{
			get { return _workflowContext; }
		}
	}
}