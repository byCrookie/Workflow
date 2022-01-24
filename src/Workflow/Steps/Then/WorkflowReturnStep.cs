using System.Linq.Expressions;
using Workflow.Property;

namespace Workflow.Steps.Then
{
    internal class WorkflowReturnStep<TContext, TProperty> : IWorkflowReturnStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext,Task<TProperty>> _actionReturn;
        private readonly Expression<Func<TContext, TProperty>> _propertyPicker;

        public WorkflowReturnStep(
            Func<TContext,Task<TProperty>> actionReturn,
            Expression<Func<TContext,TProperty>> propertyPicker)
        {
            _actionReturn = actionReturn;
            _propertyPicker = propertyPicker;
        }


        public WorkflowReturnStep(
            Func<TContext,TProperty> actionReturn,
            Expression<Func<TContext,TProperty>> propertyPicker)
        {
            _actionReturn = context => Task.FromResult(actionReturn(context));
            _propertyPicker = propertyPicker;
        }

        public Task ExecuteAsync(TContext context)
        {
            return PropertyValueSetter<TContext, TProperty>.SetAsync(context, _actionReturn, _propertyPicker);
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecuteAsync();
        }
    }
}