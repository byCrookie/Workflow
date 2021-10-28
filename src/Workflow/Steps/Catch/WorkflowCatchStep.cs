using System;
using System.Threading.Tasks;

namespace Workflow.Steps.Catch
{
    internal class WorkflowCatchStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task> _action;

        public WorkflowCatchStep(Func<TContext, Task> action)
        {
            _action = action;
        }

        public WorkflowCatchStep(Action<TContext> action)
        {
            _action = context => Task.Run(() => action(context));
        }

        public async Task ExecuteAsync(TContext context)
        {
            await _action(context).ConfigureAwait(false);
            context.Exception = null;
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.Exception != null && context.ShouldExecute());
        }
    }
}