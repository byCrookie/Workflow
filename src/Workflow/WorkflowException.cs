namespace Workflow;

internal class WorkflowException<TContext> : Exception where TContext : WorkflowBaseContext
{
    public WorkflowException(Exception exception, TContext context, IWorkflowStep<TContext> step) 
        : base($"Step: {step.GetType().Name}, Context - {context.PropertiesToString<TContext>()}", exception)
    {
    }
}