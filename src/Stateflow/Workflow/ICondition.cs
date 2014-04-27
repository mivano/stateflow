namespace Stateflow.Workflow
{
    public interface ICondition
    {
        bool IsAllowed(IWorkflow workflow);
    }
}