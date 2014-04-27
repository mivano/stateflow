namespace Stateflow.Workflow
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

        public bool IsAllowed(IWorkflow workflow)
        {
            var context = new ExpressionContext();

            if (workflow.Fields != null)
            {
                foreach (var field in workflow.Fields)
                {
                    context.Variables.Add(field.Name, field.Value);
                }
            }

            var expression = new DynamicExpression(Expression, ExpressionLanguage.Csharp);

            var boundExpression = expression.Bind(context);

            return (bool)boundExpression.Invoke(context); 
        }
    }
}