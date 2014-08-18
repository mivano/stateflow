using System.Collections.Generic;
using System;

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
		/// <param name = "isReentrant">Indicates if this transistion is reentrant</param>
		/// <param name = "condition">The condition that needs to evaluate to try before this trigger is allowed to move to the next state.</param>
		public Transition (State<TIdentifier> fromState, State<TIdentifier> toState, Trigger<TIdentifier> triggeredBy, bool isReentrant = false, ICondition<TIdentifier> condition=null)
		{
			FromState = fromState;
			ToState = toState;
			TriggerBy = triggeredBy;
			Condition = condition;

			IsReentrant = isReentrant;
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

		bool isReentrant;
		
		/// <summary>
		/// Marks if this state transition is reentrant. 
		/// </summary>
		/// <value><c>true</c> if this instance is reentry; otherwise, <c>false</c>.</value>
		public bool IsReentrant {
			get {
				return isReentrant;
			}
			set {
				if (value == true) {
					if (FromState.Equals (ToState) == false)
						throw new InvalidOperationException (string.Format("If a transition needs to be reentrant, then the from and to state of trigger {0} to {1} needs to be the same and not point to {2}. ", TriggerBy.Id, FromState.Id, ToState.Id));
				}
				isReentrant = value;
			}
		}
    }
}