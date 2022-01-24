namespace Workflow.Steps.Throw
{
    internal class WorkflowMessageThrowStep<TException, TContext> : 
        IWorkflowStep<TContext> 
        where TContext : WorkflowBaseContext
        where TException : Exception
    {
        private readonly Func<TContext, Task> _action;
        private readonly string _message;

        public WorkflowMessageThrowStep(string message, Func<TContext, Task> action)
        {
            _message = message;
            _action = action;
        }

        public WorkflowMessageThrowStep(string message, Action<TContext> action)
        {
            _message = message;
            _action = context => Task.Run(() => action(context));
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

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecuteAsync();
        }
    }
}