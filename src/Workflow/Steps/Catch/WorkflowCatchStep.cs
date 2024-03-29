﻿namespace Workflow.Steps.Catch;

internal class WorkflowCatchStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    private readonly Func<TContext, Task> _action;

    public WorkflowCatchStep(Func<TContext, Task> action)
    {
        _action = action;
    }

    public WorkflowCatchStep(Action<TContext> action)
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
        return context.Exception != null && await context.ShouldExecuteAsync().ConfigureAwait(true);
    }
}