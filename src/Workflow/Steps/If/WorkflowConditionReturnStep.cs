using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workflow.Property;

namespace Workflow.Steps.If
{
    internal class WorkflowConditionReturnStep<TContext, TProperty> : IWorkflowConditionReturnStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext,Task<TProperty>> _actionReturn;
        private readonly Func<TContext, Task<bool>> _condition;
        private readonly Expression<Func<TContext, TProperty>> _propertyPicker;

        public WorkflowConditionReturnStep(
            Func<TContext,Task<bool>> condition,
            Func<TContext,Task<TProperty>> actionReturn,
            Expression<Func<TContext,TProperty>> propertyPicker)
        {
            _condition = condition;
            _actionReturn = actionReturn;
            _propertyPicker = propertyPicker;
        }

        public WorkflowConditionReturnStep(
            Func<TContext,bool> condition,
            Func<TContext,TProperty> actionReturn,
            Expression<Func<TContext,TProperty>> propertyPicker)
        {
            _condition = context => Task.FromResult(condition(context));
            _actionReturn = context => Task.FromResult(actionReturn(context));
            _propertyPicker = propertyPicker;
        }

        public Task ExecuteAsync(TContext context)
        {
            return PropertyValueSetter<TContext, TProperty>.SetAsync(context, _actionReturn, _propertyPicker);
        }

        public async Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecute() && await _condition(context).ConfigureAwait(false);
        }
    }
}