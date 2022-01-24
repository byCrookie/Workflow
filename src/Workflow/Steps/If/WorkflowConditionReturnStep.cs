using System.Linq.Expressions;
using Workflow.Property;

namespace Workflow.Steps.If;

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
        return WorkflowPropertyValueSetter<TContext, TProperty>.SetAsync(context, _actionReturn, _propertyPicker);
    }

    public async Task<bool> ShouldExecuteAsync(TContext context)
    {
        return await context.ShouldExecuteAsync().ConfigureAwait(true)
               && await _condition(context).ConfigureAwait(true);
    }
}