namespace Stateflow.Workflow
{
    public interface IAction
    {
        void Execute(IWorkflow workflow);
    }
}