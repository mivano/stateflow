using System.Collections.Generic;

namespace Stateflow.Workflow
{
    /// <summary>
    /// A transition moves, using a trigger, from one state to another state.
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// Gets or sets source state.
        /// </summary>
        /// <value>
        /// From state.
        /// </value>
        public State FromState { get; set; }

        /// <summary>
        /// Gets or sets the destination state.
        /// </summary>
        /// <value>
        /// To state.
        /// </value>
        public State ToState { get; set; }

        /// <summary>
        /// Gets or sets the trigger used to invoke this transition.
        /// </summary>
        /// <value>
        /// The trigger by.
        /// </value>
        public Trigger TriggerBy { get; set; }

        /// <summary>
        /// Gets or sets the conditions which should be evaluated to true.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public ICondition Condition { get; set; }

		/// <summary>
		/// Gets or sets the actions that needs to be executed when the transition occurs.
		/// </summary>
		/// <value>The actions.</value>
		public IAction Action {get;set;}
    }
}