using System.Linq.Expressions;

namespace Workflow.Steps.Then
{
    public interface IWorkflowThenBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> Then<TProperty>(Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, TProperty> actionReturn);
        IWorkflowBuilder<TContext> Then(Action<TContext> action);
        IWorkflowBuilder<TContext> ThenAsync<TProperty>(Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, Task<TProperty>> actionReturn);
        IWorkflowBuilder<TContext> ThenAsync(Func<TContext, Task> action);
        IWorkflowBuilder<TContext> ThenAsync<TStep>() where TStep : IWorkflowStep<TContext>;
        IWorkflowBuilder<TContext> ThenAsync<TStep, TParameter>(TParameter parameter) where TStep : IWorkflowParameterStep<TContext, TParameter>;
        IWorkflowBuilder<TContext> ThenAsync<TStep, TConfig>(Action<TConfig> configure) where TStep : IWorkflowOptionsStep<TContext, TConfig>;
    }
}