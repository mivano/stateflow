using System.Collections.Generic;

namespace Stateflow.Workflow
{
    /// <summary>
    /// A state in the workflow.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the entry actions.
        /// </summary>
        /// <value>
        /// The entry actions.
        /// </value>
        public IList<IAction> EntryActions { get; set; }
        /// <summary>
        /// Gets or sets the exit actions.
        /// </summary>
        /// <value>
        /// The exit actions.
        /// </value>
        public IList<IAction> ExitActions { get; set; }
    }
}