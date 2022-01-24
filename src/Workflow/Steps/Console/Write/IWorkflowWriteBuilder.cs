namespace Workflow.Steps.Console.Write
{
    public interface IWorkflowWriteBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> WriteLine(Func<TContext, string> action);
        IWorkflowBuilder<TContext> WriteLineAsync(Func<TContext, Task<string>> action);
        IWorkflowBuilder<TContext> Write(Func<TContext, string> action);
        IWorkflowBuilder<TContext> WriteAsync(Func<TContext, Task<string>> action);
    }
}