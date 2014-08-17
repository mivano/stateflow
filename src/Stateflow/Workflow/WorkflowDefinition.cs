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
	public class WorkflowDefinition<TIdentifier>: IIdentifiableBy<TIdentifier>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.WorkflowDefinition`1"/> class.
		/// </summary>
		public WorkflowDefinition (TIdentifier id)
		{
			Id = id;
			States = new StateCollection<TIdentifier>();
			Triggers = new TriggerCollection<TIdentifier>();
			Transitions = new List<Transition<TIdentifier>> ();
		}

		/// <summary>
		/// Gets or sets the id of this workflow.
		/// </summary>
		/// <value>
		/// The Identifier.
		/// </value>
		public TIdentifier Id { get; set; }

	
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
		public StateCollection<TIdentifier> States { get; set; }

		/// <summary>
		/// Gets or sets the triggers. A trigger can be used to move from state to state. Like Rejecting, Accepting, Closing.
		/// </summary>
		/// <value>
		/// The triggers.
		/// </value>
		public TriggerCollection<TIdentifier> Triggers { get; set; }

		/// <summary>
		/// Gets or sets the transitions. A transition describes from which source state to destination state the workflow can move based on a certain trigger.
		/// For example; from the state Review, using accept, to the state Accepted. 
		/// Conditions can be used to guard the state transfer.
		/// </summary>
		/// <value>
		/// The transitions.
		/// </value>
		public IList<Transition<TIdentifier>> Transitions { get; set; }

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
		public static WorkflowDefinition<TIdentifier> Deserialize(string json)
		{
			Enforce.ArgumentNotNull (json, "json");

			return JsonConvert.DeserializeObject<WorkflowDefinition<TIdentifier>> (json);
		}

		/// <summary>
		/// A utility method to validate the definition of a workflow.
		/// </summary>
		/// <remarks>
		/// To be used before the definition is being saved.
		/// </remarks>
		/// <returns>True if valid, otherwise false.</returns>
		public bool IsValid()
		{
			// 1. StartSate and EndState are mandatory and should be declared only once.
			//
			var counters = new int[2] { 0, 0 };
			foreach (var state in States.Values) {
				if (state.GetType().GetGenericTypeDefinition() == typeof(StartState<>))
				 {
					counters [0]++;
					continue;
				}
				if (state.GetType().GetGenericTypeDefinition() == typeof(EndState<>))
				{
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