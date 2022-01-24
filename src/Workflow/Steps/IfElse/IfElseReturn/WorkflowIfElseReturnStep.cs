using System.Linq.Expressions;
using Workflow.Property;

namespace Workflow.Steps.IfElse.IfElseReturn
{
    internal class WorkflowIfElseReturnStep<TContext, TProperty> : IWorkflowIfElseReturnStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext,Task<bool>> _condition;
        private readonly Func<TContext,Task<TProperty>> _elseStep;
        private readonly Func<TContext,Task<TProperty>> _ifStep;
        private readonly Expression<Func<TContext, TProperty>> _propertyElsePicker;
        private readonly Expression<Func<TContext, TProperty>> _propertyIfPicker;

        public WorkflowIfElseReturnStep(
            Func<TContext,Task<bool>> condition,
            Func<TContext,Task<TProperty>> ifStep,
            Expression<Func<TContext,TProperty>> propertyIfPicker,
            Func<TContext,Task<TProperty>> elseStep,
            Expression<Func<TContext,TProperty>> propertyElsePicker)
        {
            _condition = condition;
            _ifStep = ifStep;
            _propertyIfPicker = propertyIfPicker;
            _elseStep = elseStep;
            _propertyElsePicker = propertyElsePicker;
        }

        public WorkflowIfElseReturnStep(
            Func<TContext,bool> condition,
            Func<TContext,TProperty> ifStep,
            Expression<Func<TContext,TProperty>> propertyIfPicker,
            Func<TContext,TProperty> elseStep,
            Expression<Func<TContext,TProperty>> propertyElsePicker)
        {
            _condition = context => Task.FromResult(condition(context));
            _ifStep = context => Task.Run(() => ifStep(context));
            _propertyIfPicker = propertyIfPicker;
            _elseStep = context => Task.Run(() => elseStep(context));
            _propertyElsePicker = propertyElsePicker;
        }

        public async Task ExecuteAsync(TContext context)
        {
            if (await _condition(context).ConfigureAwait(true))
            {
                await PropertyValueSetter<TContext, TProperty>.SetAsync(context, _ifStep, _propertyIfPicker).ConfigureAwait(true);
            }
            else
            {
                await PropertyValueSetter<TContext, TProperty>.SetAsync(context, _elseStep, _propertyElsePicker).ConfigureAwait(true);
            }
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecuteAsync();
        }
    }
}