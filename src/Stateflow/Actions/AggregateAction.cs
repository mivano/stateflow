using System.Collections.Generic;
using System;
using Stateflow.Workflow.Exceptions;

namespace Stateflow.Workflow.Actions
{

	/// <summary>
	/// Combines multiple actions together. Each action will be executed, but if one fails, an exception will be thrown.
	/// </summary>
	public class AggregateAction<TIdentifier>: IAction<TIdentifier>{

		readonly IEnumerable<IAction<TIdentifier>> _actions;

		/// <summary>
		/// Initializes a new instance of the <see cref="Stateflow.Workflow.Actions.AggregateAction"/> class.
		/// </summary>
		/// <param name="actions">Actions.</param>
		public AggregateAction(IEnumerable<IAction<TIdentifier>> actions)
		{
			if (actions == null) throw new ArgumentNullException("actions");
			_actions = actions;
		}

		#region IAction implementation

		/// <summary>
		/// Executes the action.
		/// </summary>
		/// <param name="workflow">The workflow.</param>
		public void Execute(IWorkflow<TIdentifier> workflow)
		{
			foreach (var action in _actions)
			{
				try
				{
					action.Execute(workflow);
				}
				catch (Exception ex)
				{
					throw new WorkflowActionException("Unable to execute action " + action.GetType(),ex);
				}
			}
		}

		#endregion

	}
	
}