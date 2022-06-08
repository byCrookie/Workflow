namespace Workflow;

public interface IWorkflow<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Execute the workflow step by step.
    /// Workflow can only run once.
    /// </summary>
    /// <param name="context">Instance of context implementation of abstract type 'WorkflowBaseContext'. Context can be accessed in every step.</param>
    /// <returns>Same instance of context in parameter 'context'. Use reference directly or the given instance of the context in the parameter 'context'.</returns>
    Task<TContext> RunAsync(TContext context);
}