using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workflow.Property;

namespace Workflow.Steps.IfElse.IfElseIfReturn
{
    internal class WorkflowIfElseIfReturnStep<TContext, TProperty> : IWorkflowIfElseIfReturnStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext,Task<bool>> _condition;
        private readonly Func<TContext, Task> _elseStep;
        private readonly Func<TContext,Task<TProperty>> _ifStep;
        private readonly Expression<Func<TContext, TProperty>> _propertyIfPicker;

        public WorkflowIfElseIfReturnStep(
            Func<TContext,Task<bool>> condition,
            Func<TContext,Task<TProperty>> ifStep,
            Expression<Func<TContext,TProperty>> propertyIfPicker,
            Func<TContext, Task> elseStep)
        {
            _condition = condition;
            _ifStep = ifStep;
            _propertyIfPicker = propertyIfPicker;
            _elseStep = elseStep;
        }

        public WorkflowIfElseIfReturnStep(
            Func<TContext,bool> condition,
            Func<TContext,TProperty> ifStep,
            Expression<Func<TContext,TProperty>> propertyIfPicker,
            Action<TContext> elseStep)
        {
            _condition = context => Task.FromResult(condition(context));
            _ifStep = context => Task.FromResult(ifStep(context));
            _propertyIfPicker = propertyIfPicker;
            _elseStep = context => Task.Run(() => elseStep(context));
        }

        public async Task ExecuteAsync(TContext context)
        {
            if (await _condition(context).ConfigureAwait(true))
            {
                await PropertyValueSetter<TContext, TProperty>.SetAsync(context, _ifStep, _propertyIfPicker).ConfigureAwait(true);
            }
            else
            {
                await _elseStep(context).ConfigureAwait(true);
            }
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}