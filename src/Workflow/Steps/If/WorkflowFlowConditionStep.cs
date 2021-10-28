using System;
using System.Threading.Tasks;

namespace Workflow.Steps.If
{
    internal class WorkflowFlowConditionStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task<bool>> _condition;
        private readonly IWorkflow<TContext> _subWorkflow;

        public WorkflowFlowConditionStep(Func<TContext, Task<bool>> condition, IWorkflow<TContext> subWorkflow)
        {
            _condition = condition;
            _subWorkflow = subWorkflow;
        }

        public WorkflowFlowConditionStep(Func<TContext, bool> condition, IWorkflow<TContext> subWorkflow)
        {
            _condition = context => Task.FromResult(condition(context));
            _subWorkflow = subWorkflow;
        }

        public Task ExecuteAsync(TContext context)
        {
            return _subWorkflow.RunAsync(context);
        }

        public async Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecute() && await _condition(context).ConfigureAwait(false);
        }
    }
}