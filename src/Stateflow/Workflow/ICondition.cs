namespace Stateflow.Workflow
{
    /// <summary>
    /// Validates the state change.
    /// </summary>
	public interface ICondition<TIdentifier>
    {
        /// <summary>
        /// Determines whether the specified state change is allowed.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        /// <returns></returns>
		bool IsAllowed(IWorkflow<TIdentifier> workflow);
    }
}