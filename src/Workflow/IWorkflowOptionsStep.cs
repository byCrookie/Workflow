namespace Workflow;

public interface IWorkflowOptionsStep<in TContext, TOptions> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{
    void SetOptions(Lazy<TOptions> options);
}