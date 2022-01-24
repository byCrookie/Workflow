namespace Workflow;

public interface IWorkflowOptionsStep<in TContext, in TOptions> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    void SetOptions(TOptions options);
}