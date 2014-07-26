using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Utils;
using Stateless;

namespace Stateflow.Workflow
{

    /// <summary>
    /// This base class can be used to add workflow capabilities to a class.
    /// </summary>
    public abstract class WorkflowEntity : IWorkflow
    {
		private WorkflowDefinition _workflowDefinition;
        private StateMachine<string, string> _stateMachine;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowEntity"/> class.
        /// </summary>
        /// <param name="workflowDefinition">The workflow definition.</param>
        /// <param name="currentState">Current state of the workflow. If null, it will be first state found in the workflow definition.</param>
        protected WorkflowEntity(WorkflowDefinition workflowDefinition, string currentState)
        {
			_workflowDefinition = workflowDefinition;
            ToStateMachine(workflowDefinition, currentState);
        }

        private void ToStateMachine(WorkflowDefinition workflowDefinition, string currentState)
        {
            Enforce.ArgumentNotNull(workflowDefinition, "workflowDefinition");

            WorkflowState = currentState ?? workflowDefinition.Transitions.First().FromState;
            _stateMachine = new StateMachine<string, string>(() => WorkflowState, s => WorkflowState = s);

            //  Get a distinct list of states with a trigger from state configuration:
            //  State => Trigger => TargetState
            var states = workflowDefinition.Transitions.AsQueryable()
                .Select(x => x.FromState)
                .Distinct()
                .Select(x => x)
                .ToList();

            // Assign triggers to states
            states.ForEach(state =>
            {
                var triggers = workflowDefinition.Transitions.AsQueryable()
                    .Where(config => config.FromState == state)
                    .Select(config => new { Trigger = config.TriggerBy, TargetState = config.ToState, Conditions = config.Conditions })
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

        private void OnTransitionAction(StateMachine<string, string>.Transition transition)
        {
			var states = GetTransitionStates(transition.Source, transition.Destination);

			// Signal start workflow
			if (states[0] != null && states[0] is StartState)
			{
				OnWorkflowStarted(transition.Source);
			}

			// Signal state transition
			OnStateTransition(transition.Source, transition.Destination, transition.Trigger);

			// Signal end workflow
			if (states[1] != null && states[1] is EndState)
			{
				OnWorkflowEnded(transition.Destination);
			}
        }

		private State[] GetTransitionStates(string source, string destination)
		{
			var result = new State[2];
			foreach (var state in _workflowDefinition.States)
			{
				if (state.Name.Equals(source, StringComparison.InvariantCultureIgnoreCase))
				{
					result[0] = state;
					continue;
				}
				if (state.Name.Equals(destination, StringComparison.InvariantCultureIgnoreCase))
				{
					result[1] = state;
					continue;
				}
				if (result[0] != null && result[1] != null)
				{
					break;
				}
			}
			return result;
		}

        /// <summary>
        /// Called when there is a transition of the current state to a new state for a given trigger.
        /// This allows you to build up e.g. workflow history.
        /// </summary>
        /// <param name="fromState">From state.</param>
        /// <param name="toState">To state.</param>
        /// <param name="triggeredBy">Transition triggered by.</param>
        protected virtual void OnStateTransition(string fromState, string toState, string triggeredBy)
        {
        }

		/// <summary>
		/// Called when workflow has started, meaning it has moved from the first state to the next.
		/// </summary>
		/// <param name="state">State name.</param>
		protected virtual void OnWorkflowStarted(string state)
		{
		}

		/// <summary>
		/// Called when workflow has ended, meaning it has moved to the final state.
		/// </summary>
		/// <param name="state">State name.</param>
		protected virtual void OnWorkflowEnded(string state)
		{
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
		/// <param name="trigger">The trigger.</param>
        public virtual void ChangeState(string trigger)
        {
			Enforce.ArgumentNotNull(trigger, "trigger");

			_stateMachine.Fire(trigger);
        }

        /// <summary>
        /// Determines whether this workflow can change its state based on the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
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
    }
}