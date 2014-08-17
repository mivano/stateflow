using System.Collections.Generic;
using System;

namespace Stateflow.Workflow
{


	/// <summary>
	/// A state in the workflow.
	/// </summary>
	public class State<TIdentifier>: IIdentifiableBy<TIdentifier>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.State`1"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="onEntryStateAction">On entry state action.</param>
		/// <param name="onExitStateAction">On exit state action.</param>
		public State (TIdentifier id, IAction<TIdentifier> onEntryStateAction =null, IAction<TIdentifier> onExitStateAction=null)
		{
			Id = id;
			EntryAction = onEntryStateAction;
			ExitAction = onExitStateAction;
		}


		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		/// <value>
		/// The Identifier of this state.
		/// </value>
		public TIdentifier Id { get; set; }

		/// <summary>
		/// Gets or sets the display name. 
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the description of this state. This provides additional information about the state.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the entry action that will be executed when the state is entered.
		/// </summary>
		/// <value>
		/// The entry action.
		/// </value>
		public IAction<TIdentifier> EntryAction { get; private set; }

		/// <summary>
		/// Gets or sets the exit action that will be executed when the state is been exited.
		/// </summary>
		/// <value>
		/// The exit action.
		/// </value>
		public IAction<TIdentifier> ExitAction { get; private set; }

		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static bool operator ==(State<TIdentifier> a, State<TIdentifier> b)
		{
			return (a.Equals (b));
		}

		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static bool operator !=(State<TIdentifier> a, State<TIdentifier> b)
		{
			return !(a.Equals (b));
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Stateflow.Workflow.State`1"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Stateflow.Workflow.State`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="Stateflow.Workflow.State`1"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals (null, obj)) {
				return false;
			}
			if (ReferenceEquals (this, obj)) {
				return true;
			}
			if (obj.GetType () != typeof(State<TIdentifier>)) {
				return false;
			}

			return Equals ((State<TIdentifier>)obj);
		}

		/// <summary>
		/// Determines whether the specified <see cref="Stateflow.Workflow.State`1[[`0]]"/> is equal to the current <see cref="Stateflow.Workflow.State`1"/>.
		/// </summary>
		/// <param name="other">The <see cref="Stateflow.Workflow.State`1[[`0]]"/> to compare with the current <see cref="Stateflow.Workflow.State`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Stateflow.Workflow.State`1[[`0]]"/> is equal to the current
		/// <see cref="Stateflow.Workflow.State`1"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(State<TIdentifier> other)
		{
			if (ReferenceEquals (null, other)) {
				return false;
			}
			if (ReferenceEquals (this, other)) {
				return true;
			}

			return Equals (other.Id, Id);
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="Stateflow.Workflow.State`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode()
		{
			unchecked {
				return ((Id != null ? Id.GetHashCode () : 0) * 397);
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Stateflow.Workflow.State`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Stateflow.Workflow.State`1"/>.</returns>
		public override string ToString()
		{
			return string.Format ("[State: Id={0}, DisplayName={1}]", Id, DisplayName);
		}
	}

	/// <summary>
	/// Defines a start state
	/// </summary>
	public class StartState<TIdentifier> : State<TIdentifier>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.StartState`1"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="onEntryStateAction">On entry state action.</param>
		/// <param name="onExitStateAction">On exit state action.</param>
		public StartState (TIdentifier id, IAction<TIdentifier> onEntryStateAction = null, IAction<TIdentifier> onExitStateAction=null) : base (id, onEntryStateAction, onExitStateAction)
		{
			
		}
	}

	/// <summary>
	/// Defines an end state.
	/// </summary>
	public class EndState<TIdentifier> : State<TIdentifier>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.EndState`1"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="onEntryStateAction">On entry state action.</param>
		/// <param name="onExitStateAction">On exit state action.</param>
		public EndState (TIdentifier id, IAction<TIdentifier> onEntryStateAction=null, IAction<TIdentifier> onExitStateAction=null) : base (id, onEntryStateAction, onExitStateAction)
		{

		}

	}


	
}