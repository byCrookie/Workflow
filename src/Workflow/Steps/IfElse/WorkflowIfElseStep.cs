using System;
using System.Threading.Tasks;

namespace Workflow.Steps.IfElse
{
    internal class WorkflowIfElseStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext,Task<bool>> _condition;
        private readonly Func<TContext, Task> _elseStep;
        private readonly Func<TContext, Task> _ifStep;

        public WorkflowIfElseStep(
            Func<TContext,Task<bool>> condition,
            Func<TContext, Task> ifStep,
            Func<TContext, Task> elseStep)
        {
            _condition = condition;
            _ifStep = ifStep;
            _elseStep = elseStep;
        }

        public WorkflowIfElseStep(
            Func<TContext,bool> condition,
            Action<TContext> ifStep,
            Action<TContext> elseStep)
        {
            _condition = context => Task.FromResult(condition(context));
            _ifStep = context => Task.Run(() => ifStep(context));
            _elseStep = context => Task.Run(() => elseStep(context));
        }

        public async Task ExecuteAsync(TContext context)
        { 
            if (await _condition(context).ConfigureAwait(true))
            {
                await _ifStep(context).ConfigureAwait(true);
            }
            else
            {
                await _elseStep(context).ConfigureAwait(true);
            }
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}