namespace Workflow.Steps.Catch;

internal class WorkflowTypeCatchStep<TException, TContext> : 
    IWorkflowStep<TContext> 
    where TContext : WorkflowBaseContext 
    where TException : Exception
{
    private readonly Func<TContext, Task> _action;

    public WorkflowTypeCatchStep(Func<TContext, Task> action)
    {
        _action = action;
    }

    public WorkflowTypeCatchStep(Action<TContext> action)
    {
        _action = context => Task.Run(() => action(context));
    }

    public async Task ExecuteAsync(TContext context)
    {
        await _action(context).ConfigureAwait(true);
        context.Exception = null;
    }

    public async Task<bool> ShouldExecuteAsync(TContext context)
    {
        return context.Exception is TException && await context.ShouldExecuteAsync().ConfigureAwait(true);
    }
}