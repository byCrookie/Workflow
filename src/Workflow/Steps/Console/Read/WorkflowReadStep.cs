using System.Linq.Expressions;
using Workflow.Property;

namespace Workflow.Steps.Console.Read;

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
        return WorkflowProperty<TContext, int>.SetAsync(context, line, _propertyPicker);
    }

    public Task<bool> ShouldExecuteAsync(TContext context)
    {
        return context.ShouldExecuteAsync();
    }
}