using System.Collections.Generic;
using System;

namespace Stateflow.Workflow
{
	/// <summary>
	/// State collection.
	/// </summary>
	public class StateCollection<TIdentifier> : Dictionary<TIdentifier, State<TIdentifier>>{

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add the specified state.
		/// </summary>
		/// <param name="state">State.</param>
		public void Add(State<TIdentifier> state){
			this.Add (state.Id, state);
		}

	}
	
}