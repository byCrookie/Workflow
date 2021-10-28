using System.Threading.Tasks;

namespace Workflow.Steps.Stop
{
    internal class WorkflowStopWithOuterStopStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly WorkflowBaseContext _outerContext;

        public WorkflowStopWithOuterStopStep(WorkflowBaseContext outerContext)
        {
            _outerContext = outerContext;
        }

        public Task ExecuteAsync(TContext context)
        {
            context.IsStop = true;
            _outerContext.IsStop = true;
            return Task.CompletedTask;
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}