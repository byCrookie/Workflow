namespace Workflow.Jab.Tests;

public class WorkflowTestStep<TContext> : IWorkflowTestStep<TContext> where TContext : WorkflowTestContext
{
    public Task ExecuteAsync(TContext context)
    {
        var workflowTestContext = context as WorkflowTestContext;
        workflowTestContext.Valid = true;
        return Task.CompletedTask;
    }

    public Task<bool> ShouldExecuteAsync(TContext context)
    {
        return context.ShouldExecuteAsync();
    }
}