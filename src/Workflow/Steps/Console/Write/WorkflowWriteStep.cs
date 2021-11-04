using System;
using System.Threading.Tasks;

namespace Workflow.Steps.Console.Write
{
    internal class WorkflowWriteStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task<string>> _action;

        public WorkflowWriteStep(Func<TContext, Task<string>> action)
        {
            _action = action;
        }

        public WorkflowWriteStep(Func<TContext, string> action)
        {
            _action = context => Task.FromResult(action(context));
        }

        public async Task ExecuteAsync(TContext context)
        {
            System.Console.Write(await _action(context).ConfigureAwait(true));
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}