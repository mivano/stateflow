namespace Stateflow.Workflow
{
    /// <summary>
    /// A trigger
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name. Use this for example for buttons.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description of this trigger. This provides additional information about the trigger.
        /// </summary>
        public string Description { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Stateflow.Workflow.Trigger"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Stateflow.Workflow.Trigger"/>.</returns>
		public override string ToString()
		{
			return string.Format("[Trigger: Name={0}, DisplayName={1}]", Name, DisplayName);
		}
    }
}