using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Utils;
using Stateless;

namespace Stateflow.Workflow
{

	/// <summary>
	/// This event is raised when the state is transitioned.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The <see cref="StateChangeEventArgs"/> instance containing the event data.</param>
	public delegate void StateChangeHandler<TIdentifier>(object sender, StateChangeEventArgs<TIdentifier> e);
				

	/// <summary>
	/// Contains the event arguments.
	/// </summary>
	public class StateChangeEventArgs<TIdentifier> : EventArgs
	{
		private readonly State<TIdentifier> _fromState;
		private readonly State<TIdentifier> _toState;
		private readonly Trigger<TIdentifier> _triggeredBy;
		private readonly bool _isReentry;

		/// <summary>
		/// Initializes a new instance of the <see cref="StateChangeEventArgs"/> class.
		/// </summary>
		/// <param name="fromState">From state.</param>
		/// <param name="toState">To state.</param>
		/// <param name="triggeredBy">The triggered by.</param>
		/// <param name="isReentry"></param>
		public StateChangeEventArgs(State<TIdentifier> fromState, State<TIdentifier> toState, Trigger<TIdentifier> triggeredBy, bool isReentry)
		{
			_fromState = fromState;
			_toState = toState;
			_triggeredBy = triggeredBy;
			_isReentry = isReentry;
		}

		/// <summary>
		/// Gets from state.
		/// </summary>
		/// <value>
		/// From state.
		/// </value>
		public State<TIdentifier> FromState
		{
			get { return _fromState; }
		}

		/// <summary>
		/// Gets to state.
		/// </summary>
		/// <value>
		/// To state.
		/// </value>
		public State<TIdentifier> ToState
		{
			get { return _toState; }
		}

		/// <summary>
		/// Gets the triggered by value.
		/// </summary>
		/// <value>
		/// The triggered by.
		/// </value>
		public Trigger<TIdentifier> TriggeredBy
		{
			get { return _triggeredBy; }
		}

		/// <summary>
		/// Gets a value indicating whether is a reentry.
		/// </summary>
		/// <value>
		///   <c>true</c> if [is reentry]; otherwise, <c>false</c>.
		/// </value>
		public bool IsReentry
		{
			get { return _isReentry; }
		}
	}
	
}