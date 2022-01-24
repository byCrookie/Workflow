using System.Linq.Expressions;
using Workflow.Property;

namespace Workflow.Steps.Console.Read
{
    internal class WorkflowReadLineStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Expression<Func<TContext, string>> _propertyPicker;

        public WorkflowReadLineStep(Expression<Func<TContext,string>> propertyPicker)
        {
            _propertyPicker = propertyPicker;
        }

        public Task ExecuteAsync(TContext context)
        {
            var line = System.Console.ReadLine();
            return PropertyValueSetter<TContext, string>.SetAsync(context, line, _propertyPicker);
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecuteAsync();
        }
    }
}