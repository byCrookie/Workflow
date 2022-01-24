namespace Workflow.Steps.Stop;

internal class WorkflowActionConditionStopStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    private readonly Func<TContext, Task> _action;
    private readonly Func<TContext, Task<bool>> _condition;

    public WorkflowActionConditionStopStep(Func<TContext, Task<bool>> condition, Func<TContext, Task> action)
    {
        _condition = condition;
        _action = action;
    }

    public WorkflowActionConditionStopStep(Func<TContext, bool> condition, Action<TContext> action)
    {
        _condition = context => Task.FromResult(condition(context));
        _action = context => Task.Run(() => action(context));
    }

    public async Task ExecuteAsync(TContext context)
    {
        await _action(context).ConfigureAwait(true);
        context.IsStop = true;
    }

    public async Task<bool> ShouldExecuteAsync(TContext context)
    {
        return await context.ShouldExecuteAsync().ConfigureAwait(true) 
               && await _condition(context).ConfigureAwait(true);
    }
}