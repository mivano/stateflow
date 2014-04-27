using System.Collections.Generic;

namespace Stateflow.Workflow
{
    public interface IWorkflow
    {
        void ChangeState(string newState);
        IEnumerable<string> PermittedTriggers { get; }
        string WorkflowState { get; set; }
    }
}