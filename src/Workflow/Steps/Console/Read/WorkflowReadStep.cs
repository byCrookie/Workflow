using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workflow.Property;

namespace Workflow.Steps.Console.Read
{
    internal class WorkflowReadStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Expression<Func<TContext, int>> _propertyPicker;

        public WorkflowReadStep(Expression<Func<TContext,int>> propertyPicker)
        {
            _propertyPicker = propertyPicker;
        }

        public Task ExecuteAsync(TContext context)
        {
            var line = System.Console.Read();
            return PropertyValueSetter<TContext, int>.SetAsync(context, line, _propertyPicker);
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}