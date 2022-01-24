namespace Workflow.Steps.Stop;

public interface IWorkflowStopBuilder<TContext> where TContext : WorkflowBaseContext
{
    IWorkflowBuilder<TContext> StopAsync();
    IWorkflowBuilder<TContext> StopAsync<TOuterContext>(TOuterContext outerContext) where TOuterContext : WorkflowBaseContext;
    IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task> action);
    IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task<bool>> condition);
    IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> action);
    IWorkflowBuilder<TContext> Stop(Action<TContext> action);
    IWorkflowBuilder<TContext> Stop(Func<TContext, bool> condition);
    IWorkflowBuilder<TContext> Stop(Func<TContext, bool> condition, Action<TContext> action);
}