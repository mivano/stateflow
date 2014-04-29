using Expressions;
using Stateflow.Workflow;
using System.Collections.Generic;

namespace Stateflow.Expressions
{
    public class ExpressionCondition : ICondition
    {
        public string Expression { get; set; }

        public ExpressionCondition()
        {

        }

        public ExpressionCondition(string expression)
        {
            Expression = expression;
        }

        public virtual bool IsAllowed(IWorkflow workflow)
        {

			return Evaluate (null);
        }

		public virtual bool Evaluate(Dictionary<string, object> fields){
	
			ExpressionContext context = new ExpressionContext();
			var expression = new DynamicExpression(Expression, ExpressionLanguage.Csharp);

			if (fields != null) {

				foreach (var field in fields) {
					context.Variables.Add(field.Key, field.Value);
				}
			}

			var boundExpression = expression.Bind(context);

			return (bool)boundExpression.Invoke(context);


		}
    }
}