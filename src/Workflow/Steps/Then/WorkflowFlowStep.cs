using System.Threading.Tasks;

namespace Workflow.Steps.Then
{
    internal class WorkflowFlowStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly IWorkflow<TContext> _subWorkflow;

        public WorkflowFlowStep(IWorkflow<TContext> subWorkflow)
        {
            _subWorkflow = subWorkflow;
        }

        public Task ExecuteAsync(TContext context)
        {
            return _subWorkflow.RunAsync(context);
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}