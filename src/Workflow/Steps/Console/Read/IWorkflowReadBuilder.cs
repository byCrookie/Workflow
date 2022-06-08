using System.Linq.Expressions;

namespace Workflow.Steps.Console.Read;

public interface IWorkflowReadBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Reads the next character from the standard input stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    IWorkflowBuilder<TContext> ReadLine(Expression<Func<TContext, string?>> propertyPicker);

    /// <summary>
    /// Reads the next line of characters from the standard input stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    IWorkflowBuilder<TContext> Read(Expression<Func<TContext, int>> propertyPicker);

    /// <summary>
    /// Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    IWorkflowBuilder<TContext> ReadKey(Expression<Func<TContext, ConsoleKeyInfo>> propertyPicker);
    
    /// <summary>
    /// Reads the next lines of characters from the standard input stream.
    /// Works only in the context of a console application.
    /// </summary>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    /// <param name="options">Options to configure reading of multiline text.</param>
    IWorkflowBuilder<TContext> ReadMultiLine(Expression<Func<TContext, string?>> propertyPicker, Action<WorkflowMultiLineOptions> options);
}