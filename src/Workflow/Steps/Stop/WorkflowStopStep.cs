using System.Threading.Tasks;

namespace Workflow.Steps.Stop
{
    internal class WorkflowStopStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        public Task ExecuteAsync(TContext context)
        {
            context.IsStop = true;
            return Task.CompletedTask;
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}