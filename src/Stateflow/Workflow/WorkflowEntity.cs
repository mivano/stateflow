using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace Stateflow.Workflow
{

    /// <summary>
    /// This base class can be used to add workflow capabilities to a class.
    /// </summary>
    public abstract class WorkflowEntity : IWorkflow
    {
        private StateMachine<string, string> _stateMachine;

        protected WorkflowEntity(WorkflowDefinition workflowDefinition, string currentState)
        {
            ToStateMachine(workflowDefinition, currentState);
        }

        private void ToStateMachine(WorkflowDefinition workflowDefinition, string currentState)
        {
            WorkflowState = currentState ?? workflowDefinition.Transitions.First().FromState;
            _stateMachine = new StateMachine<string, string>(() => WorkflowState, s => WorkflowState = s);

            //  Get a distinct list of states with a trigger from state configuration
            //  "State => Trigger => TargetState
            var states = workflowDefinition.Transitions.AsQueryable()
                .Select(x => x.FromState)
                .Distinct()
                .Select(x => x)
                .ToList();

            //  Assing triggers to states
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

            _stateMachine.OnTransitioned(OnTransitionAction);
        }

        private void OnTransitionAction(StateMachine<string, string>.Transition transition)
        {
            OnStateTransition(transition.Source, transition.Destination, transition.Trigger);
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
        /// <param name="newState">The new state.</param>
        public virtual void ChangeState(string newState)
        {
            _stateMachine.Fire(newState);
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