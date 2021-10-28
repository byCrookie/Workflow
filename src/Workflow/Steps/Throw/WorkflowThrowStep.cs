using System;
using System.Threading.Tasks;

namespace Workflow.Steps.Throw
{
    internal class WorkflowThrowStep<TException, TContext> : 
        IWorkflowStep<TContext> 
        where TContext : WorkflowBaseContext
        where TException : Exception
    {
        private readonly Func<TContext,Task> _action;

        public WorkflowThrowStep(Func<TContext,Task> action)
        {
            _action = action;
        }

        public WorkflowThrowStep(Action<TContext> action)
        {
            _action = context => Task.Run(() => action(context));
        }

        public async Task ExecuteAsync(TContext context)
        {
            await _action(context).ConfigureAwait(false);
            
            var instance = Activator.CreateInstance(typeof(TException));

            if (instance is null)
            {
                throw new ArgumentNullException($"Instance of type {typeof(TException)} could not been instantied");
            }
            
            throw (TException)instance;
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}