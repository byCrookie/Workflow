using System;
using System.Threading.Tasks;

namespace Workflow.Steps.While
{
    internal class WorkflowWhileStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task<bool>> _condition;
        private readonly IWorkflow<TContext> _subWorkflow;

        public WorkflowWhileStep(Func<TContext, Task<bool>> condition, IWorkflow<TContext> subWorkflow)
        {
            _condition = condition;
            _subWorkflow = subWorkflow;
        }

        public WorkflowWhileStep(Func<TContext, bool> condition, IWorkflow<TContext> subWorkflow)
        {
            _condition = context => Task.FromResult(condition(context));
            _subWorkflow = subWorkflow;
        }


        public async Task ExecuteAsync(TContext context)
        {
            while (context.ShouldExecute() && await _condition(context).ConfigureAwait(true))
            {
                await _subWorkflow.RunAsync(context).ConfigureAwait(true);
            }
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}