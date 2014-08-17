using System.Collections.Generic;

namespace Stateflow.Workflow
{
    /// <summary>
    /// A transition moves, using a trigger, from one state to another state.
    /// </summary>
	public class Transition<TIdentifier>: IIdentifiableBy<TIdentifier>
    {
		#region IIdentifiableBy implementation

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public TIdentifier Id {
			get;
			set;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.Transition`1"/> class.
		/// </summary>
		public Transition ()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.Transition`1"/> class.
		/// </summary>
		/// <param name="fromState">From state.</param>
		/// <param name="toState">To state.</param>
		/// <param name="triggeredBy">Triggered by.</param>
		public Transition (State<TIdentifier> fromState, State<TIdentifier> toState, Trigger<TIdentifier> triggeredBy)
		{
			FromState = fromState;
			ToState = toState;
			TriggerBy = triggeredBy;
		}

        /// <summary>
        /// Gets or sets source state.
        /// </summary>
        /// <value>
        /// From state.
        /// </value>
		public State<TIdentifier> FromState { get; set; }

        /// <summary>
        /// Gets or sets the destination state.
        /// </summary>
        /// <value>
        /// To state.
        /// </value>
		public State<TIdentifier> ToState { get; set; }

        /// <summary>
        /// Gets or sets the trigger used to invoke this transition.
        /// </summary>
        /// <value>
        /// The trigger by.
        /// </value>
		public Trigger<TIdentifier> TriggerBy { get; set; }

        /// <summary>
        /// Gets or sets the conditions which should be evaluated to true.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
		public ICondition<TIdentifier> Condition { get; set; }

		/// <summary>
		/// Gets or sets the actions that needs to be executed when the transition occurs.
		/// </summary>
		/// <value>The actions.</value>
		public IAction<TIdentifier> Action {get;set;}
    }
}