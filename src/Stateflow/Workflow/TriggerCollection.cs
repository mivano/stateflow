using System.Collections.Generic;
using System;

namespace Stateflow.Workflow
{

	/// <summary>
	/// Trigger collection.
	/// </summary>
	public class TriggerCollection<TIdentifier> : Dictionary<TIdentifier, Trigger<TIdentifier>>{

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// </summary>
		/// <param name="trigger">Trigger.</param>
		public void Add(Trigger<TIdentifier> trigger){
			this.Add (trigger.Id, trigger);
		}

	}
	
}