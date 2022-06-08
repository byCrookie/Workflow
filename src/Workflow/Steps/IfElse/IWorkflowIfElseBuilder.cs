using System.Linq.Expressions;

namespace Workflow.Steps.IfElse;

public interface IWorkflowIfElseBuilder<TContext> where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Executes one of two actions based on a condition.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="ifStep">Action to execute when 'condition' is true.</param>
    /// <param name="elseStep">Action to execute when 'condition' is false.</param>
    IWorkflowBuilder<TContext> IfElse(Func<TContext, bool> condition, Action<TContext> ifStep, Action<TContext> elseStep);
    
    /// <summary>
    /// Executes one of two actions based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="propertyIfPicker">Selector for property on context to set value to if 'condition' is true</param>
    /// <param name="actionIfReturn">Func to retrieve value to set on 'propertyIfPicker' if 'condition' is true.</param>
    /// <param name="propertyElsePicker">Selector for property on context to set value to if 'condition' is false.</param>
    /// <param name="actionElseReturn">Func to retrieve value to set on 'propertyElsePicker' if 'condition' is false.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, TProperty> actionIfReturn, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, TProperty> actionElseReturn);
    
    /// <summary>
    /// Executes one of two actions based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="propertyIfPicker">Selector for property on context to set value to if 'condition' is true</param>
    /// <param name="actionIfReturn">Func to retrieve value to set on 'propertyIfPicker' if 'condition' is true.</param>
    /// <param name="actionElse">Action to execute if 'condition' is false.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, TProperty> actionIfReturn, Action<TContext> actionElse);
    
    /// <summary>
    /// Executes one of two actions based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="actionIf">Func to execute if 'condition' is true.</param>
    /// <param name="propertyElsePicker">Selector for property on context to set value to if 'condition' is false.</param>
    /// <param name="actionElseReturn">Func to retrieve value to set on 'propertyElsePicker' if 'condition' is false.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Action<TContext> actionIf, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, TProperty> actionElseReturn);
   
    /// <summary>
    /// Executes one of two actions based on a condition.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="ifStep">Action to execute when 'condition' is true.</param>
    /// <param name="elseStep">Action to execute when 'condition' is false.</param>
    IWorkflowBuilder<TContext> IfElseAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> ifStep, Func<TContext, Task> elseStep);
    
    /// <summary>
    /// Executes one of two custom workflow steps based on a condition.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <typeparam name="TIfStep">Custom workflow step to execute when 'condition' is true.</typeparam>
    /// <typeparam name="TElseStep">Custom workflow step to execute when 'condition' is false.</typeparam>
    IWorkflowBuilder<TContext> IfElseAsync<TIfStep, TElseStep>(Func<TContext, Task<bool>> condition) where TIfStep : IWorkflowStep<TContext> where TElseStep : IWorkflowStep<TContext>;
    
    /// <summary>
    /// Executes one of two actions based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="propertyIfPicker">Selector for property on context to set value to if 'condition' is true</param>
    /// <param name="actionIfReturn">Func to retrieve value to set on 'propertyIfPicker' if 'condition' is true.</param>
    /// <param name="propertyElsePicker">Selector for property on context to set value to if 'condition' is false.</param>
    /// <param name="actionElseReturn">Func to retrieve value to set on 'propertyElsePicker' if 'condition' is false.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, Task<TProperty>> actionIfReturn, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, Task<TProperty>> actionElseReturn);
    
    /// <summary>
    /// Executes one of two actions based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="propertyIfPicker">Selector for property on context to set value to if 'condition' is true</param>
    /// <param name="actionIfReturn">Func to retrieve value to set on 'propertyIfPicker' if 'condition' is true.</param>
    /// <param name="actionElse">Action to execute if 'condition' is false.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, Task<TProperty>> actionIfReturn, Func<TContext, Task> actionElse);
    
    /// <summary>
    /// Executes one of two actions based on a condition and sets property value to return value of the action.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="actionIf">Func to execute if 'condition' is true.</param>
    /// <param name="propertyElsePicker">Selector for property on context to set value to if 'condition' is false.</param>
    /// <param name="actionElseReturn">Func to retrieve value to set on 'propertyElsePicker' if 'condition' is false.</param>
    /// <typeparam name="TProperty">Type of value of step.</typeparam>
    IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Func<TContext, Task> actionIf, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, Task<TProperty>> actionElseReturn);
    
    /// <summary>
    /// Executes one of two sub workflows based on a condition.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="configureIf">Builder to configure sub workflow to execute when 'condition' is true.</param>
    /// <param name="configureElse">Builder to configure sub workflow to execute when 'condition' is false.</param>
    IWorkflowBuilder<TContext> IfElseFlow(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configureIf, Action<IWorkflowBuilder<TContext>> configureElse);
    
    /// <summary>
    /// Executes one of two sub workflows based on a condition.
    /// </summary>
    /// <param name="condition">If else is based on the 'condition'.</param>
    /// <param name="configureIf">Builder to configure sub workflow to execute when 'condition' is true.</param>
    /// <param name="configureElse">Builder to configure sub workflow to execute when 'condition' is false.</param>
    IWorkflowBuilder<TContext> IfElseFlowAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configureIf, Action<IWorkflowBuilder<TContext>> configureElse);
}