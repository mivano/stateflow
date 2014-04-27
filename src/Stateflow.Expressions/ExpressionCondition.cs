using Expressions;
using Stateflow.Workflow;

namespace Stateflow.Expressions
{
    public class ExpressionCondition   : ICondition
    {
        public string Expression { get; set; }

        public ExpressionCondition()
        {
            
        }

        public ExpressionCondition(string expression)
        {
            Expression = expression;
        }

        public virtual  bool IsAllowed(IWorkflow workflow)
        {
            var context = new ExpressionContext();

            var expression = new DynamicExpression(Expression, ExpressionLanguage.Csharp);

            var boundExpression = expression.Bind(context);

            return (bool)boundExpression.Invoke(context); 
        }
    }
}