namespace Workflow.Steps.Stop;

internal class WorkflowConditionStopStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    private readonly Func<TContext, Task<bool>> _condition;

    public WorkflowConditionStopStep(Func<TContext, Task<bool>> condition)
    {
        _condition = condition;
    }

    public WorkflowConditionStopStep(Func<TContext, bool> condition)
    {
        _condition = context => Task.FromResult(condition(context));
    }

    public Task ExecuteAsync(TContext context)
    {
        context.IsStop = true;
        return Task.CompletedTask;
    }

    public async Task<bool> ShouldExecuteAsync(TContext context)
    {
        return await context.ShouldExecuteAsync().ConfigureAwait(true)
               && await _condition(context).ConfigureAwait(true);
    }
}