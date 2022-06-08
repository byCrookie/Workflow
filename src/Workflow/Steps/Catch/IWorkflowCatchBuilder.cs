namespace Workflow.Steps.Catch;


public interface IWorkflowCatchBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Catches exception of all types and executes an action. Runs the remaining steps after the catch step.
    /// </summary>
    /// <param name="action">action to execute on exception</param>
    IWorkflowBuilder<TContext> Catch(Action<TContext> action);
    
    /// <summary>
    /// Catches exceptions of a specific type and executes an action. Runs the remaining steps after the catch step.
    /// </summary>
    /// <param name="action">action to execute on exception</param>
    /// <typeparam name="TException">Type of exception to catch</typeparam>
    IWorkflowBuilder<TContext> Catch<TException>(Action<TContext> action) where TException : Exception;
    
    /// <summary>
    /// Catches exception of all types and executes an action. Runs the remaining steps after the catch step.
    /// </summary>
    /// <param name="action">func of type 'Task' to execute on exception</param>
    IWorkflowBuilder<TContext> CatchAsync(Func<TContext, Task> action);
    
    /// <summary>
    /// Catches exceptions of a specific type and executes an action. Runs the remaining steps after the catch step.
    /// </summary>
    /// <param name="action">func of type 'Task' to execute on exception</param>
    /// <typeparam name="TException">Type of exception to catch</typeparam>
    IWorkflowBuilder<TContext> CatchAsync<TException>(Func<TContext, Task> action) where TException : Exception;
}