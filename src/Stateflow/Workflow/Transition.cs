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
        public string FromState { get; set; }
        /// <summary>
        /// Gets or sets the destination state.
        /// </summary>
        /// <value>
        /// To state.
        /// </value>
        public string ToState { get; set; }
        /// <summary>
        /// Gets or sets the trigger used to invoke this transition.
        /// </summary>
        /// <value>
        /// The trigger by.
        /// </value>
        public string TriggerBy { get; set; }
        /// <summary>
        /// Gets or sets the conditions which should be evaluated to true.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public IList<ICondition> Conditions { get; set; }
    }
}