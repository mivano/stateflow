using System;
using System.Collections.Generic;
using System.Linq;
using Stateflow.Workflow;

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
            var wd = new WorkflowDefinition
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
                       //  Conditions = new List<ICondition>{ new ExpressionCondition("price > 10000")}
                    } ,
                      new Transition
                    {
                         FromState = "UnderReview",
                         ToState = "Closed",
                         TriggerBy = "Close"   ,
                      //   Conditions = new List<ICondition>{ new ExpressionCondition("price <= 10000")}
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

            var ser = wd.Serialize();

            var lead = new Lead(wd, "Created");

            lead.Fields = new List<Field>{ new Field
            {
                Name = "price", Value = 100000
            }};

            while (lead.PermittedTriggers.Any())
            {

                Console.WriteLine("Current workflow state: {0}", lead.WorkflowState);
                Console.WriteLine("  next steps: {0}", String.Join(", ", lead.PermittedTriggers.ToArray()));

                Console.WriteLine("Enter the new state name:");
                var nextStep = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nextStep))
                    break;

                lead.ChangeState(nextStep);

            }

            Console.WriteLine("No further steps, press a key to continue");
            Console.ReadKey(true);

        }
    }
}
