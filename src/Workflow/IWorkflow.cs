namespace Workflow
{
    public interface IWorkflow<TContext> where TContext : WorkflowBaseContext
    {
        void AddStep(IWorkflowStep<TContext> step);
        Task<TContext> RunAsync(TContext context);
    }
}