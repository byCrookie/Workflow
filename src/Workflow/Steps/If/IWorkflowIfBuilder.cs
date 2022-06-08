using System.Linq.Expressions;

namespace Workflow.Steps.If;

public interface IWorkflowIfBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Executes an action based on a condition.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed.</param>
    /// <param name="action">Action to execute when condition is true.</param>
    IWorkflowBuilder<TContext> If(Func<TContext, bool> condition, Action<TContext> action);
    
    /// <summary>
    /// Executes an action based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed.</param>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    /// <param name="actionReturn">Func to retrieve value to set on 'propertyPicker'.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> If<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, TProperty> actionReturn);
    
    /// <summary>
    /// Executes an action based on a condition.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed.</param>
    /// <param name="action">Action to execute when condition is true.</param>
    IWorkflowBuilder<TContext> IfAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> action);
    
    /// <summary>
    /// Executes an action based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If 'condition' is true 'action' is executed.</param>
    /// <param name="propertyPicker">Selector for property on context to set value to.</param>
    /// <param name="actionReturn">Func to retrieve value to set on 'propertyPicker'.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, Task<TProperty>> actionReturn);
    
    /// <summary>
    /// Executes a custom workflow step based on a condition.
    /// </summary>
    /// <param name="condition">If 'condition' is true custom workflow step is executed.</param>
    /// <typeparam name="TStep">Type of custom workflow step.</typeparam>
    IWorkflowBuilder<TContext> If<TStep>(Func<TContext, bool> condition) where TStep : IWorkflowStep<TContext>;
    
    /// <summary>
    /// Executes a custom workflow step based on a condition.
    /// </summary>
    /// <param name="condition">If 'condition' is true custom workflow step is executed.</param>
    /// <typeparam name="TStep">Type of custom workflow step.</typeparam>
    IWorkflowBuilder<TContext> IfAsync<TStep>(Func<TContext, Task<bool>> condition) where TStep : IWorkflowStep<TContext>;
    
    /// <summary>
    /// Executes a sub workflow based on a condition.
    /// </summary>
    /// <param name="condition">If 'condition' is true sub workflow is executed.</param>
    /// <param name="configure">Builder to configure sub workflow.</param>
    IWorkflowBuilder<TContext> IfFlow(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configure);
    
    /// <summary>
    /// Executes a sub workflow based on a condition.
    /// </summary>
    /// <param name="condition">If 'condition' is true sub workflow is executed.</param>
    /// <param name="configure">Builder to configure sub workflow.</param>
    IWorkflowBuilder<TContext> IfFlowAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configure);
}