using System.Collections.Generic;

namespace Stateflow.Workflow
{
    /// <summary>
    /// Workflow 
    /// </summary>
	public interface IWorkflow<TIdentifier>
    {

        /// <summary>
        /// Changes the current state to a new state.
        /// </summary>
		/// <param name="trigger">The trigger to that force the move to a new state.</param>
		void ChangeState(TIdentifier trigger);

		/// <summary>
		/// Changes the current state to a new state.
		/// </summary>
		/// <param name="trigger">The trigger to that force the move to a new state.</param>
		void ChangeState(Trigger<TIdentifier> trigger);


        /// <summary>
        /// Gets the permitted triggers based on the current state and the allowed triggers for this state.
        /// </summary>
        /// <value>
        /// The permitted triggers.
        /// </value>
		IEnumerable<Trigger<TIdentifier>> PermittedTriggers { get; }

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value>
        /// The state of the workflow.
        /// </value>
		State<TIdentifier> CurrentState { get; }

        /// <summary>
        /// Determines whether this workflow can change its state based on the trigger.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
		bool CanChangeState(Trigger<TIdentifier> trigger);

		/// <summary>
		/// Provides the workflow context.
		/// </summary>
	    object Context { get; }
    }
}