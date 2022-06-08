namespace Workflow.Steps.Throw;

public interface IWorkflowThrowBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Throws exception of a specific type and executes action.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> Throw<TException>(Action<TContext> action) where TException : Exception;

    /// <summary>
    /// Throws exception of a specific type only if condition is true and executes action only if condition is true.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed and exception thrown.</param>
    /// <param name="action">Action to execute when 'condition' is true.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> Throw<TException>(Func<TContext, bool> condition, Action<TContext> action) where TException : Exception;

    /// <summary>
    /// Throws exception of a specific type plus message and executes action.
    /// </summary>
    /// <param name="message">Message of exception.</param>
    /// <param name="action">Action to execute.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> Throw<TException>(string? message, Action<TContext> action) where TException : Exception;

    /// <summary>
    /// Throws exception of a specific type plus message and executes action only if condition is true.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed and exception thrown.</param>
    /// <param name="message">Message of exception.</param>
    /// <param name="action">Action to execute when 'condition' is true.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> Throw<TException>(Func<TContext, bool> condition, string? message, Action<TContext> action) where TException : Exception;

    /// <summary>
    /// Throws exception of a specific type and executes action.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task> action) where TException : Exception;
    
    /// <summary>
    /// Throws exception of a specific type only if condition is true and executes action only if condition is true.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed and exception thrown.</param>
    /// <param name="action">Action to execute when 'condition' is true.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task<bool>> condition, Func<TContext, Task> action) where TException : Exception;
    
    /// <summary>
    /// Throws exception of a specific type plus message and executes action.
    /// </summary>
    /// <param name="message">Message of exception.</param>
    /// <param name="action">Action to execute.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> ThrowAsync<TException>(string? message, Func<TContext, Task> action) where TException : Exception;
    
    /// <summary>
    /// Throws exception of a specific type plus message and executes action only if condition is true.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed and exception thrown.</param>
    /// <param name="message">Message of exception.</param>
    /// <param name="action">Action to execute when 'condition' is true.</param>
    /// <typeparam name="TException">Type of exception to throw.</typeparam>
    IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task<bool>> condition, string? message, Func<TContext, Task> action) where TException : Exception;
}