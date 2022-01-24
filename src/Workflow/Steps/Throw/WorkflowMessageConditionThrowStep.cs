namespace Workflow.Steps.Throw
{
    internal class WorkflowMessageConditionThrowStep<TException, TContext> : 
        IWorkflowStep<TContext> 
        where TContext : WorkflowBaseContext
        where TException : Exception
    {
        private readonly Func<TContext, Task> _action;
        private readonly Func<TContext, Task<bool>> _condition;
        private readonly string _message;

        public WorkflowMessageConditionThrowStep(Func<TContext, Task<bool>> condition, string message, Func<TContext, Task> action)
        {
            _condition = condition;
            _message = message;
            _action = action;
        }

        public WorkflowMessageConditionThrowStep(Func<TContext, bool> condition, string message, Action<TContext> action)
        {
            _condition = context => Task.FromResult(condition(context));
            _action = context => Task.Run(() => action(context));
            _message = message;
        }

        public async Task ExecuteAsync(TContext context)
        {
            await _action(context).ConfigureAwait(true);
            
            var instance = Activator.CreateInstance(typeof(TException), _message);

            if (instance is null)
            {
                throw new Exception(_message);
            }
            
            throw (TException)instance;
        }

        public async Task<bool> ShouldExecuteAsync(TContext context)
        {
            return await context.ShouldExecuteAsync().ConfigureAwait(true) 
                   && await _condition(context).ConfigureAwait(true);
        }
    }
}