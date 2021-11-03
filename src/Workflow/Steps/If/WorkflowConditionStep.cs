using System;
using System.Threading.Tasks;

namespace Workflow.Steps.If
{
    internal class WorkflowConditionStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task> _action;
        private readonly Func<TContext, Task<bool>> _condition;

        public WorkflowConditionStep(Func<TContext, bool> condition,  Action<TContext> action)
        {
            _condition = context => Task.FromResult(condition(context));
            _action = context => Task.Run(() => action(context));
        }

        public WorkflowConditionStep(Func<TContext, Task<bool>> condition,  Func<TContext, Task> action)
        {
            _condition = condition;
            _action = action;
        }
        
        public WorkflowConditionStep(Func<TContext, bool> condition,  Func<TContext, Task> action)
        {
            _condition = context => Task.FromResult(condition(context));
            _action = action;
        }

        public Task ExecuteAsync(TContext context)
        {
           return _action(context);
        }

        public async Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecute() && await _condition(context).ConfigureAwait(true);
        }
    }
}