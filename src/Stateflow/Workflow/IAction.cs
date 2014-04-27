namespace Stateflow.Workflow
{
    /// <summary>
    /// An action to perform.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        void Execute(IWorkflow workflow);
    }
}