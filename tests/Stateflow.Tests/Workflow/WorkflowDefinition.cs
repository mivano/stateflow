using NUnit.Framework;
using System;
using Stateflow.Workflow;

namespace Stateflow.Tests.Workflow
{
	[TestFixture ()]
	public class WorkflowDefinition
	{
		[Test ()]
		public void CanCreateWorkflow ()
		{
			var wfDef = new WorkflowDefinition<string> ("test");

			wfDef.Triggers.Add (new Trigger<string> ("progress"));
			wfDef.Triggers.Add (new Trigger<string> ("close"));

			wfDef.States.Add (new StartState<string> ("new"));
			wfDef.States.Add (new State<string> ("processing"));
			wfDef.States.Add (new EndState<string> ("closed"));

			wfDef.Transitions.Add (new Transition<string> (wfDef.States["new"], wfDef.States["processing"], wfDef.Triggers["progress"]));
			wfDef.Transitions.Add (new Transition<string> (wfDef.States["processing"], wfDef.States["closed"], wfDef.Triggers["close"]));

			Assert.IsTrue (wfDef.IsValid());

		}
	}
}

