namespace Workflow.Steps.Stop;

public interface IWorkflowStopBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Stops the execution of the workflow.
    /// </summary>
    IWorkflowBuilder<TContext> StopAsync();

    /// <summary>
    /// Stops the execution of the parent workflow and current workflow.
    /// </summary>
    /// <param name="outerContext">Context of the parent workflow.</param>
    /// <typeparam name="TOuterContext">Type of the context of the parent workflow.</typeparam>
    IWorkflowBuilder<TContext> StopAsync<TOuterContext>(TOuterContext outerContext) where TOuterContext : WorkflowBaseContext;

    /// <summary>
    /// Stops the execution of the workflow and executes action.
    /// </summary>
    /// <param name="action">Action to execute when workflow is stopped.</param>
    IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task> action);

    /// <summary>
    /// Stops the execution of the workflow only if the condition is true.
    /// </summary>
    /// <param name="condition">If the 'condition' is true the workflow is stopped.</param>
    IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task<bool>> condition);
    
    /// <summary>
    /// Stops the execution of the workflow only if the condition is true and executes the action only if the condition is true.
    /// </summary>
    /// <param name="condition">If the 'condition' is true the workflow is stopped.</param>
    /// <param name="action">Action to execute when the 'condition' is true.</param>
    IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> action);
    
    /// <summary>
    /// Stops the execution of the workflow and executes action.
    /// </summary>
    /// <param name="action">Action to execute when workflow is stopped.</param>
    IWorkflowBuilder<TContext> Stop(Action<TContext> action);
    
    /// <summary>
    /// Stops the execution of the workflow only if the condition is true.
    /// </summary>
    /// <param name="condition">If the 'condition' is true the workflow is stopped.</param>
    IWorkflowBuilder<TContext> Stop(Func<TContext, bool> condition);
    
    /// <summary>
    /// Stops the execution of the workflow only if the condition is true and executes the action only if the condition is true.
    /// </summary>
    /// <param name="condition">If the 'condition' is true the workflow is stopped.</param>
    /// <param name="action">Action to execute when the 'condition' is true.</param>
    IWorkflowBuilder<TContext> Stop(Func<TContext, bool> condition, Action<TContext> action);
}