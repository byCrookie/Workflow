namespace Workflow.Steps.While;

public interface IWorkflowWhileBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Executes a sub workflow as long as the condition is true.
    /// </summary>
    /// <param name="condition">Condition to keep while loop running.</param>
    /// <param name="configure">Build to configure sub workflow.</param>
    IWorkflowBuilder<TContext> While(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configure);
    
    /// <summary>
    /// Executes a sub workflow as long as the condition is true.
    /// </summary>
    /// <param name="condition">Condition to keep while loop running.</param>
    /// <param name="configure">Build to configure sub workflow.</param>
    IWorkflowBuilder<TContext> WhileAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configure);
}