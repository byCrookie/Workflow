using System.Linq.Expressions;

namespace Workflow.Steps.Then;

public interface IWorkflowThenBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Executes action and sets property to return value.
    /// </summary>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    /// <param name="actionReturn">Func to retrieve value to set on 'propertyPicker'.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> Then<TProperty>(Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, TProperty> actionReturn);
    
    /// <summary>
    /// Executes action.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    IWorkflowBuilder<TContext> Then(Action<TContext> action);
    
    /// <summary>
    /// Executes action and sets property to return value.
    /// </summary>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    /// <param name="actionReturn">Func to retrieve value to set on 'propertyPicker'.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> ThenAsync<TProperty>(Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, Task<TProperty>> actionReturn);
    
    /// <summary>
    /// Executes action.
    /// </summary>
    /// <param name="action">Action to execute.</param>
    IWorkflowBuilder<TContext> ThenAsync(Func<TContext, Task> action);
    
    /// <summary>
    /// Executes custom workflow step.
    /// </summary>
    /// <typeparam name="TStep">Custom workflow step to execute.</typeparam>
    IWorkflowBuilder<TContext> ThenAsync<TStep>() where TStep : IWorkflowStep<TContext>;
    
    /// <summary>
    /// Executes custom workflow step with parameters.
    /// </summary>
    /// <param name="parameter">Parameter object to configure.</param>
    /// <typeparam name="TStep">Custom workflow step to execute.</typeparam>
    /// <typeparam name="TParameter">Type of parameter object.</typeparam>
    IWorkflowBuilder<TContext> ThenAsync<TStep, TParameter>(TParameter? parameter) where TStep : IWorkflowParameterStep<TContext, TParameter>;
    
    /// <summary>
    /// Executes custom workflow step with options.
    /// </summary>
    /// <param name="configure">Options object to configure.</param>
    /// <typeparam name="TStep">Custom workflow step to execute.</typeparam>
    /// <typeparam name="TConfig">Type of options object.</typeparam>
    IWorkflowBuilder<TContext> ThenAsync<TStep, TConfig>(Action<TConfig>? configure) where TStep : IWorkflowOptionsStep<TContext, TConfig>;
}