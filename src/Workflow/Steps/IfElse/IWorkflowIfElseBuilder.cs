using System.Linq.Expressions;

namespace Workflow.Steps.IfElse
{
    public interface IWorkflowIfElseBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> IfElse(Func<TContext, bool> condition, Action<TContext> ifStep, Action<TContext> elseStep);
        IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, TProperty> actionIfReturn, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, TProperty> actionElseReturn);
        IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, TProperty> actionIfReturn, Action<TContext> actionElse);
        IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Action<TContext> actionIf, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, TProperty> actionElseReturn);
        IWorkflowBuilder<TContext> IfElseAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> ifStep, Func<TContext, Task> elseStep);
        IWorkflowBuilder<TContext> IfElseAsync<TIfStep, TElseStep>(Func<TContext, Task<bool>> condition) where TIfStep : IWorkflowStep<TContext> where TElseStep : IWorkflowStep<TContext>;
        IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, Task<TProperty>> actionIfReturn, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, Task<TProperty>> actionElseReturn);
        IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, Task<TProperty>> actionIfReturn, Func<TContext, Task> actionElse);
        IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Func<TContext, Task> actionIf, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, Task<TProperty>> actionElseReturn);
        IWorkflowBuilder<TContext> IfElseFlow(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configureIf, Action<IWorkflowBuilder<TContext>> configureElse);
        IWorkflowBuilder<TContext> IfElseFlowAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configureIf, Action<IWorkflowBuilder<TContext>> configureElse);
    }
}