namespace Workflow.Steps.Stop
{
    internal class WorkflowActionStopStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task> _action;

        public WorkflowActionStopStep(Func<TContext, Task> action)
        {
            _action = action;
        }

        public WorkflowActionStopStep(Action<TContext> action)
        {
            _action = context => Task.Run(() => action(context));
        }

        public async Task ExecuteAsync(TContext context)
        {
            await _action(context).ConfigureAwait(true);
            context.IsStop = true;
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecuteAsync();
        }
    }
}