
namespace Stateflow.Workflow
{
    /// <summary>
    /// An action to perform.
    /// </summary>
	public interface IAction<TIdentifier>
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
		/// <param name = "transition">The actual transition that took place.</param>
		void Execute(IWorkflow<TIdentifier> workflow,StateChangeEventArgs<TIdentifier> transition);
    }
}