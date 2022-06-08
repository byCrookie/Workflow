namespace Workflow.Steps.Console.Write;

public interface IWorkflowWriteBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="action">Func to retrieve string value to write.</param>
    IWorkflowBuilder<TContext> WriteLine(Func<TContext, string?> action);
    
    /// <summary>
    /// Writes the specified string value, followed by the current line terminator, to the standard output stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="action">Func of type 'Task' to retrieve string value to write.</param>
    IWorkflowBuilder<TContext> WriteLineAsync(Func<TContext, Task<string?>> action);
    
    /// <summary>
    /// Writes the specified string value to the standard output stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="action">Func to retrieve string value to write.</param>
    IWorkflowBuilder<TContext> Write(Func<TContext, string?> action);
    
    /// <summary>
    /// Writes the specified string value to the standard output stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="action">Func of type 'Task' to retrieve string value to write.</param>
    IWorkflowBuilder<TContext> WriteAsync(Func<TContext, Task<string?>> action);
}