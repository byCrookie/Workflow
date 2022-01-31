namespace Workflow.Steps.Console.Write;

internal class WorkflowWriteLineStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    private readonly Func<TContext, Task<string?>> _action;

    public WorkflowWriteLineStep(Func<TContext, Task<string?>> action)
    {
        _action = action;
    }

    public WorkflowWriteLineStep(Func<TContext, string?> action)
    {
        _action = context => Task.FromResult(action(context));
    }

    public async Task ExecuteAsync(TContext context)
    {
        System.Console.WriteLine(await _action(context).ConfigureAwait(true));
    }

    public Task<bool> ShouldExecuteAsync(TContext context)
    {
        return context.ShouldExecuteAsync();
    }
}