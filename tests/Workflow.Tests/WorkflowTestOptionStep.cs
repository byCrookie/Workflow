namespace Workflow.Tests;

public sealed class WorkflowTestOptionStep<TContext, TOptions> : IWorkflowTestOptionStep<TContext, TOptions>
    where TContext : WorkflowTestContext
    where TOptions : WorkflowTestOptions
{
    private Lazy<TOptions>? _options;

    public Task ExecuteAsync(TContext context)
    {
        var workflowTestContext = (WorkflowTestContext)context;
        var options = _options is not null ? _options.Value : new WorkflowTestOptions();
        workflowTestContext.Flow.Add(options.Number.GetValueOrDefault());
        return Task.CompletedTask;
    }

    public Task<bool> ShouldExecuteAsync(TContext context)
    {
        return context.ShouldExecuteAsync();
    }

    public void SetOptions(Lazy<TOptions> options)
    {
        _options = options;
    }
}