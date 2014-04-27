using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace Stateflow.Workflow
{


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
                    .Select(config => new { Trigger = config.TriggerBy,  TargetState = config.ToState, Conditions = config.Conditions })
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

        protected virtual void OnTransitionAction(StateMachine<string, string>.Transition transition)
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


        public virtual void ChangeState(string newState)
        {
            _stateMachine.Fire(newState);
        }

        public virtual IEnumerable<string> PermittedTriggers
        {
            get { return _stateMachine.PermittedTriggers; }
        }

        public virtual string WorkflowState { get; set; }
    }
}