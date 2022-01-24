namespace Workflow.Steps.While
{
    public interface IWorkflowWhileBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> While(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configure);
        IWorkflowBuilder<TContext> WhileAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configure);
    }
}