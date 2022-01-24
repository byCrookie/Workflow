using System.Linq.Expressions;

namespace Workflow.Steps.If
{
    public interface IWorkflowIfBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> If(Func<TContext, bool> condition, Action<TContext> action);
        IWorkflowBuilder<TContext> If<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, TProperty> actionReturn);
        IWorkflowBuilder<TContext> IfAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> action);
        IWorkflowBuilder<TContext> IfAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, Task<TProperty>> actionReturn);
        IWorkflowBuilder<TContext> IfAsync<TStep>(Func<TContext, Task<bool>> condition) where TStep : IWorkflowStep<TContext>;
        IWorkflowBuilder<TContext> IfAsync<TStep>(Func<TContext, bool> condition) where TStep : IWorkflowStep<TContext>;
        IWorkflowBuilder<TContext> IfFlow(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configure);
        IWorkflowBuilder<TContext> IfFlowAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configure);
    }
}