namespace Workflow.Steps.Throw;

internal class WorkflowConditionThrowStep<TException, TContext> : 
    IWorkflowStep<TContext> 
    where TContext : WorkflowBaseContext
    where TException : Exception
{
    private readonly Func<TContext, Task> _action;
    private readonly Func<TContext, Task<bool>> _condition;

    public WorkflowConditionThrowStep(Func<TContext, Task<bool>> condition, Func<TContext, Task> action)
    {
        _condition = condition;
        _action = action;
    }

    public WorkflowConditionThrowStep(Func<TContext, bool> condition, Action<TContext> action)
    {
        _condition = context => Task.FromResult(condition(context));
        _action = context => Task.Run(() => action(context));
    }

    public async Task ExecuteAsync(TContext context)
    {
        await _action(context).ConfigureAwait(true);
        var instance = Activator.CreateInstance(typeof(TException));

        if (instance is null)
        {
            throw new ArgumentNullException($"Instance of type {typeof(TException)} could not been instantied");
        }
            
        throw (TException)instance;
    }

    public async Task<bool> ShouldExecuteAsync(TContext context)
    {
        return await context.ShouldExecuteAsync().ConfigureAwait(true) 
               && await _condition(context).ConfigureAwait(true);
    }
}