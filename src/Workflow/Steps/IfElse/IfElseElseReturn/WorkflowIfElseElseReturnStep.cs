using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Workflow.Property;

namespace Workflow.Steps.IfElse.IfElseElseReturn
{
    internal class WorkflowIfElseElseReturnStep<TContext, TProperty> : IWorkflowIfElseElseReturnStep<TContext>
        where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext, Task<bool>> _condition;
        private readonly Func<TContext, Task<TProperty>> _elseStep;
        private readonly Func<TContext, Task> _ifStep;
        private readonly Expression<Func<TContext, TProperty>> _propertyElsePicker;

        public WorkflowIfElseElseReturnStep(
            Func<TContext, Task<bool>> condition,
            Func<TContext, Task> ifStep,
            Func<TContext, Task<TProperty>> elseStep,
            Expression<Func<TContext, TProperty>> propertyElsePicker)
        {
            _condition = condition;
            _ifStep = ifStep;
            _elseStep = elseStep;
            _propertyElsePicker = propertyElsePicker;
        }


        public WorkflowIfElseElseReturnStep(
            Func<TContext, bool> condition,
            Action<TContext> ifStep,
            Func<TContext, TProperty> elseStep,
            Expression<Func<TContext, TProperty>> propertyElsePicker)
        {
            _condition = context => Task.FromResult(condition(context));
            _ifStep = context => Task.Run(() => ifStep(context));
            _elseStep = context => Task.Run(() => elseStep(context));
            _propertyElsePicker = propertyElsePicker;
        }

        public async Task ExecuteAsync(TContext context)
        {
            if (await _condition(context).ConfigureAwait(false))
            {
                await _ifStep(context).ConfigureAwait(false);
            }
            else
            {
                await PropertyValueSetter<TContext, TProperty>.SetAsync(context, _elseStep, _propertyElsePicker).ConfigureAwait(false);
            }
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }
    }
}