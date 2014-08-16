using System.Collections.Generic;
using System;

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
        /// Gets or sets the description of this state. This provides additional information about the state.
        /// </summary>
        public string Description { get; set; }

		/// <summary>
		/// Gets or sets the entry actions.
		/// </summary>
		/// <value>
		/// The entry actions.
		/// </value>
		public IAction EntryAction { get; set; }

		/// <summary>
		/// Gets or sets the exit actions.
		/// </summary>
		/// <value>
		/// The exit actions.
		/// </value>
		public IAction ExitAction { get; set; }

		public static bool operator ==(State a, State b) { return (a.Equals(b)); }

		public static bool operator !=(State a, State b) { return !(a.Equals(b)); }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof (State))
			{
				return false;
			}

			return Equals((State) obj);
		}

		public bool Equals(State other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Equals(other.Name, Name);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Name != null ? Name.GetHashCode() : 0)*397);
			}
		}


		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Stateflow.Workflow.State"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Stateflow.Workflow.State"/>.</returns>
		public override string ToString()
		{
			return string.Format ("[State: Name={0}, DisplayName={1}]", Name, DisplayName);
		}
	}

	/// <summary>
	/// Defines a start state
	/// </summary>
	public class StartState : State
	{
	}

	/// <summary>
	/// Defines an end state.
	/// </summary>
	public class EndState : State
	{
	}


	
}