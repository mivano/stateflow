using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stateflow.Utils;
using System;

namespace Stateflow.Workflow
{
	/// <summary>
	/// Defines a workflow.
	/// </summary>
	public class WorkflowDefinition
	{

		/// <summary>
		/// Gets or sets the name of this workflow.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		///  The current version of this workflow definition
		/// </summary>
		/// <value>The version.</value>
		public Version Version {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the description of this workflow
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the states. States define the workflow. Like a Create state, an Assigned state etc.
		/// Each state can have an entry and exit action(s).
		/// </summary>
		/// <value>
		/// The states.
		/// </value>
		public IList<State> States { get; set; }

		/// <summary>
		/// Gets or sets the triggers. A trigger can be used to move from state to state. Like Rejecting, Accepting, Closing.
		/// </summary>
		/// <value>
		/// The triggers.
		/// </value>
		public IList<Trigger> Triggers { get; set; }

		/// <summary>
		/// Gets or sets the transitions. A transition describes from which source state to destination state the workflow can move based on a certain trigger.
		/// For example; from the state Review, using accept, to the state Accepted. 
		/// Conditions can be used to guard the state transfer.
		/// </summary>
		/// <value>
		/// The transitions.
		/// </value>
		public IList<Transition> Transitions { get; set; }

		/// <summary>
		/// Serializes this instance to a json string value.
		/// </summary>
		/// <returns>A json serialization.</returns>
		public string Serialize()
		{
			return JsonConvert.SerializeObject (this, new JsonSerializerSettings {
				ContractResolver = new CamelCasePropertyNamesContractResolver (),
				Formatting = Formatting.Indented
			});
		}

		/// <summary>
		/// Converts the json back into a workflow definition.
		/// </summary>
		/// <param name="json">The json representation.</param>
		/// <returns>A workflow definition.</returns>
		public static WorkflowDefinition Deserialize(string json)
		{
			Enforce.ArgumentNotNull (json, "json");

			return JsonConvert.DeserializeObject<WorkflowDefinition> (json);
		}

		/// <summary>
		/// A utility method to validate the definition of a workflow.
		/// </summary>
		/// <remarks>
		/// To be used before the definition is being saved.
		/// </remarks>
		/// <returns>True if valid, otherwise false.</returns>
		public bool Validate()
		{
			// 1. StartSate and EndState are mandatory and should be declared only once.
			//
			var counters = new int[2] { 0, 0 };
			foreach (var state in States) {
				if (state is StartState) {
					counters [0]++;
					continue;
				}
				if (state is EndState) {
					counters [1]++;
					continue;
				}
			}
			if (counters [0] == 1 && counters [1] >= 1) {
				return true;
			}
			return false;
		}
	}
}