namespace Stateflow.Workflow
{
    /// <summary>
    /// A trigger
    /// </summary>
	public class Trigger<TIdentifier>: IIdentifiableBy<TIdentifier>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.Trigger`1"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public Trigger (TIdentifier id)
		{
			Id = id;
		}

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The Identifier.
        /// </value>
		public TIdentifier Id { get; set; }

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
			return string.Format("[Trigger: Id={0}, DisplayName={1}]", Id, DisplayName);
		}
    }
}