using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Fields;
using Stateflow.Workflow;
using Stateflow.Expressions;
using Stateflow.Fields.DataStores;

namespace LeadSample
{
	class Program
	{
		static void Main(string[] args)
		{
			var p = new Program ();
			p.CreateStateMachine ();
		}



		public void CreateStateMachine()
		{
			var workflowDefinition = new WorkflowDefinition<string> ("name");

			workflowDefinition.States.Add (
				new StartState<string> ("Created", new MailAction ()) {
					DisplayName = "Lead Created"
				});

			workflowDefinition.States.Add (
				new State<string> ("AwaitingReview") {
					DisplayName = "Ready for Review"
				});
			workflowDefinition.States.Add (
				new State<string> ("UnderReview", new MailAction ()) {
					DisplayName = "Lead under Review"
				});

			workflowDefinition.States.Add (new State<string> ("Approved") {
				DisplayName = "Lead is Approved"
			});

			workflowDefinition.States.Add (new EndState<string> ("Closed", new MailAction()) {
				DisplayName = "Lead is Closed"
			});

			workflowDefinition.Triggers.AddRange (new List<Trigger<string>> {
				new Trigger<string> ("New"),
				new Trigger<string> ("Review"),
				new Trigger<string> ("Assign"),
				new Trigger<string> ("Approve"),
				new Trigger<string> ("Reject"),
				new Trigger<string> ("Close")
			});

			workflowDefinition.Transitions.AddRange (new List<Transition<string>> {
				new Transition<String> {
					FromState = workflowDefinition.States ["Created"],
					ToState = workflowDefinition.States ["AwaitingReview"],
					TriggerBy = workflowDefinition.Triggers ["Review"]
				},
				new Transition<String> {
					FromState = workflowDefinition.States ["AwaitingReview"],
					ToState = workflowDefinition.States ["UnderReview"],
					TriggerBy = workflowDefinition.Triggers ["Assign"]
				},

				new Transition<String> {
					FromState = workflowDefinition.States ["UnderReview"],
					ToState = workflowDefinition.States ["UnderReview"],
					TriggerBy = workflowDefinition.Triggers ["Assign"],
					IsReentrant = true
				},
				new Transition<String> {
					FromState = workflowDefinition.States ["UnderReview"],
					ToState = workflowDefinition.States ["Approved"],
					TriggerBy = workflowDefinition.Triggers ["Approve"],
					//Conditions = new List<ICondition>{ new LeadExpressionCondition("price > 10000")}
				},
				new Transition<String> {
					FromState = workflowDefinition.States ["UnderReview"],
					ToState = workflowDefinition.States ["Closed"],
					TriggerBy = workflowDefinition.Triggers ["Close"],
					//Conditions = new List<ICondition>{ new LeadExpressionCondition("price <= 10000")}
				},
				new Transition<String> {
					FromState = workflowDefinition.States ["Approved"],
					ToState = workflowDefinition.States ["Closed"],
					TriggerBy = workflowDefinition.Triggers ["Close"]
				},
				new Transition<String> {
					FromState = workflowDefinition.States ["UnderReview"],
					ToState = workflowDefinition.States ["AwaitingReview"],
					TriggerBy = workflowDefinition.Triggers ["Reject"]
				}
			}
			);

			//var ser = wd.Serialize();

			var dataStore = new InMemoryDataStore<string> ();
			var lead = new Lead (workflowDefinition) {
				Id = Guid.NewGuid ().ToString ()
			};
			var field1Definition = new FieldDefinition<string> () {
				Id = Guid.NewGuid ().ToString (),
				Name = "ProductName",
				FieldType = FieldType.SingleLineText,
				DefaultValue = "Product Name",
				IsEditable = true
			};
			var field2Definition = new FieldDefinition<string> () {
				Id = Guid.NewGuid ().ToString (),
				Name = "price",
				FieldType = FieldType.Number,
				DefaultValue = 0,
				IsEditable = true
			};
			var defaultTemplate = new Template<string> (dataStore, "DefaultTemplate");
			defaultTemplate.FieldDefinitions.Add (field1Definition);
			defaultTemplate.FieldDefinitions.Add (field2Definition);

			var fieldsItem = defaultTemplate.CreateNew ();
			fieldsItem.Id = lead.Id;
			fieldsItem.SetFieldValue (field1Definition, "Toyota iQ");
			fieldsItem.SetFieldValue (field2Definition, 11000);
			fieldsItem.Save ();

			lead.Fields = fieldsItem;

			// Validate workflow definition
			//
			if (!workflowDefinition.IsValid ()) {
				Console.WriteLine ("Invalid workflow definition! Make sure it contains only one StartState and EndState.");
				Console.ReadKey (true);
				return;
			}

			while (lead.PermittedTriggers.Any ()) {

				Console.WriteLine ("Current workflow state: {0}", lead.CurrentState);
				Console.WriteLine ("  next available steps: {0}", String.Join (", ", lead.PermittedTriggers
					.Select(a=>string.Format("{0}",a.Id)).ToArray ()));

				Console.WriteLine ("Enter the new state name:");
				var nextStep = Console.ReadLine ();

				if (string.IsNullOrWhiteSpace (nextStep))
					break;

				lead.ChangeState (nextStep);

			}

			Console.WriteLine ("No further steps, press a key to quit");
			Console.ReadKey (true);
		}
	}

}
