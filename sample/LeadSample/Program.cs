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
			var p = new Program();
			p.CreateStateMachine();
		}



		public void CreateStateMachine()
		{
			var workflowDefinition = new WorkflowDefinition
			{
				Name = "Test",
				States = new List<State>
				{
					new State
					{
						Name = "Created",
						DisplayName = "Lead Created"
					},

					 new State
					{
						Name = "AwaitingReview",
						DisplayName = "Ready for Review"
					},
					new State
					{
						Name = "UnderReview",
						DisplayName = "Lead under Review"  ,
						EntryActions = new List<IAction>{new MailAction()}
					},
					new State
					{
						Name = "Approved",
						DisplayName = "Lead is Approved"
					} ,
					 new State
					{
						Name = "Closed",
						DisplayName = "Lead is Closed"
					} 
				},
				Triggers = new List<Trigger>
				{
					 new Trigger
					{
						Name = "New"
					},
					new Trigger
					{
						Name = "Assign"
					},

					new Trigger
					{
						Name = "Approve"
					}
					,
					new Trigger
					{
						Name = "Reject"
					} ,
					new Trigger
					{
						Name="Close"
					}
				},
				Transitions = new List<Transition>
				{
					  new Transition
					{
						 FromState = "Created",
						 ToState = "AwaitingReview",
						 TriggerBy = "Review"
					}  ,
					new Transition
					{
						 FromState = "AwaitingReview",
						 ToState = "UnderReview",
						 TriggerBy = "Assign"
					}  ,
					
					new Transition
					{
						 FromState = "UnderReview",
						 ToState = "Approved",
						 TriggerBy = "Approve"   ,
						Conditions = new List<ICondition>{ new LeadExpressionCondition("price > 10000")}
					} ,
					  new Transition
					{
						 FromState = "UnderReview",
						 ToState = "Closed",
						 TriggerBy = "Close"   ,
						Conditions = new List<ICondition>{ new LeadExpressionCondition("price <= 10000")}
					} ,
					 new Transition
					{
						 FromState = "Approved",
						 ToState = "Closed",
						 TriggerBy = "Close"
					},
					new Transition
					{
						 FromState = "UnderReview",
						 ToState = "AwaitingReview",
						 TriggerBy = "Reject"
					}
				}
			};

			//var ser = wd.Serialize();

			var dataStore = new InMemoryDataStore<string>();
			var lead = new Lead(workflowDefinition)
			{
				Id = Guid.NewGuid().ToString()
			};
			var field1Definition = new FieldDefinition<string>()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "ProductName",
				FieldType = FieldType.SingleLineText,
				DefaultValue = "Product Name",
				IsEditable = true
			};
			var field2Definition = new FieldDefinition<string>()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "price",
				FieldType = FieldType.Number,
				DefaultValue = 0,
				IsEditable = true
			};
			var defaultTemplate = new Template<string>(dataStore, "DefaultTemplate");
			defaultTemplate.FieldDefinitions.Add(field1Definition);
			defaultTemplate.FieldDefinitions.Add(field2Definition);

			var fieldsItem = defaultTemplate.CreateNew();
			fieldsItem.Id = lead.Id;
			fieldsItem.SetFieldValue(field1Definition, "Toyota iQ");
			fieldsItem.SetFieldValue(field2Definition, 11000);
			fieldsItem.Save();

			lead.Fields = fieldsItem;

			while (lead.PermittedTriggers.Any())
			{

				Console.WriteLine("Current workflow state: {0}", lead.WorkflowState);
				Console.WriteLine("  next available steps: {0}", String.Join(", ", lead.PermittedTriggers.ToArray()));

				Console.WriteLine("Enter the new state name:");
				var nextStep = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(nextStep))
					break;

				lead.ChangeState(nextStep);

			}

			Console.WriteLine("No further steps, press a key to quit");
			Console.ReadKey(true);
		}
	}

}
