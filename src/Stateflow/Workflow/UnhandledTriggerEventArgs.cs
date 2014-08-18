using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Utils;
using Stateless;

namespace Stateflow.Workflow
{
	/// <summary>
	/// Unhandled trigger handler.
	/// </summary>
	public delegate void UnhandledTriggerHandler<TIdentifier>(object sender, UnhandledTriggerEventArgs<TIdentifier> e);

	/// <summary>
	/// Unhandled trigger event arguments.
	/// </summary>
	public class UnhandledTriggerEventArgs<TIdentifier>: EventArgs{

		private readonly State<TIdentifier> _state;
		private readonly Trigger<TIdentifier> _trigger;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.UnhandledTriggerEventArgs`1"/> class.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="trigger">Trigger.</param>
		public UnhandledTriggerEventArgs(State<TIdentifier> state, Trigger<TIdentifier> trigger)
		{
			_state = state;
			_trigger = trigger;
		}

		/// <summary>
		/// Gets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public State<TIdentifier> State
		{
			get { return _state; }
		}
	
		/// <summary>
		/// Gets the trigger.
		/// </summary>
		/// <value>
		/// The trigger.
		/// </value>
		public Trigger<TIdentifier> Trigger
		{
			get { return _trigger; }
		}
	}
	
}