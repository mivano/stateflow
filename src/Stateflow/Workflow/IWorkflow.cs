using System.Collections.Generic;

namespace Stateflow.Workflow
{
    /// <summary>
    /// Workflow 
    /// </summary>
    public interface IWorkflow
    {

        /// <summary>
        /// Changes the current state to a new state.
        /// </summary>
        /// <param name="newState">The new state.</param>
        void ChangeState(string newState);

        /// <summary>
        /// Gets the permitted triggers based on the current state and the allowed triggers for this state.
        /// </summary>
        /// <value>
        /// The permitted triggers.
        /// </value>
        IEnumerable<string> PermittedTriggers { get; }

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value>
        /// The state of the workflow.
        /// </value>
        string WorkflowState { get; set; }

        /// <summary>
        /// Determines whether this workflow can change its state based on the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        bool CanChangeState(string trigger);

		/// <summary>
		/// Provides the workflow context.
		/// </summary>
	    object Context { get; }
    }
}