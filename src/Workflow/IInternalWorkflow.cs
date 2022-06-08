namespace Workflow;

internal interface IInternalWorkflow<TContext> : IWorkflow<TContext> where TContext : WorkflowBaseContext
{
    void AddStep(IWorkflowStep<TContext> step);
}