namespace Workflow.Steps.Then;

internal class WorkflowStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    private readonly Func<TContext, Task> _action;

    public WorkflowStep(Func<TContext, Task> action)
    {
        _action = action;
    }

    public WorkflowStep(Action<TContext> action)
    {
        _action = context => Task.Run(() => action(context));
    }

    public Task ExecuteAsync(TContext context)
    {
        return _action(context);
    }

    public Task<bool> ShouldExecuteAsync(TContext context)
    {
        return context.ShouldExecuteAsync();
    }
}