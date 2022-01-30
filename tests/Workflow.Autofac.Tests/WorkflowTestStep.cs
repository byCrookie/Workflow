namespace Workflow.Autofac.Tests;

internal class WorkflowTestStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowTestContext
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