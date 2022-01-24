using System.Linq.Expressions;
using Workflow.Factory;
using Workflow.Steps.Catch;
using Workflow.Steps.Console.Read;
using Workflow.Steps.Console.Write;
using Workflow.Steps.If;
using Workflow.Steps.IfElse;
using Workflow.Steps.IfElse.IfElseElseReturn;
using Workflow.Steps.IfElse.IfElseIfReturn;
using Workflow.Steps.IfElse.IfElseReturn;
using Workflow.Steps.Stop;
using Workflow.Steps.Then;
using Workflow.Steps.Throw;
using Workflow.Steps.While;

namespace Workflow
{
    public class WorkflowBuilder<TContext> : IWorkflowBuilder<TContext>  where TContext : WorkflowBaseContext
    {
        private readonly IWorkflowFactory _workflowFactory;
        private IWorkflow<TContext> _workflow;

        public WorkflowBuilder(IWorkflowFactory workflowFactory)
        {
            _workflowFactory = workflowFactory;
            _workflow = new Workflow<TContext>();
        }

        public IWorkflowBuilder<TContext> Catch(Action<TContext> action)
        {
            _workflow.AddStep(new WorkflowCatchStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> CatchAsync(Func<TContext, Task> action)
        {
            _workflow.AddStep(new WorkflowCatchStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> Catch<TException>(Action<TContext> action) where TException : Exception
        {
            _workflow.AddStep(new WorkflowTypeCatchStep<TException, TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> CatchAsync<TException>(Func<TContext, Task> action) where TException : Exception
        {
            _workflow.AddStep(new WorkflowTypeCatchStep<TException, TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> Throw<TException>(Action<TContext> action) where TException: Exception
        {
            _workflow.AddStep(new WorkflowThrowStep<TException, TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task> action) where TException: Exception
        {
            _workflow.AddStep(new WorkflowThrowStep<TException, TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> Throw<TException>(Func<TContext, bool> condition, Action<TContext> action) where TException : Exception
        {
            _workflow.AddStep(new WorkflowConditionThrowStep<TException, TContext>(condition, action));
            return this;
        }

        public IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task<bool>> condition, Func<TContext, Task> action) where TException : Exception
        {
            _workflow.AddStep(new WorkflowConditionThrowStep<TException, TContext>(condition, action));
            return this;
        }

        public IWorkflowBuilder<TContext> Throw<TException>(string message, Action<TContext> action) where TException: Exception
        {
            _workflow.AddStep(new WorkflowMessageThrowStep<TException, TContext>(message, action));
            return this;
        }

        public IWorkflowBuilder<TContext> ThrowAsync<TException>(string message, Func<TContext, Task> action) where TException: Exception
        {
            _workflow.AddStep(new WorkflowMessageThrowStep<TException, TContext>(message, action));
            return this;
        }

        public IWorkflowBuilder<TContext> Throw<TException>(Func<TContext, bool> condition, string message, Action<TContext> action) where TException : Exception
        {
            _workflow.AddStep(new WorkflowMessageConditionThrowStep<TException, TContext>(condition, message, action));
            return this;
        }

        public IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task<bool>> condition, string message, Func<TContext, Task> action) where TException : Exception
        {
            _workflow.AddStep(new WorkflowMessageConditionThrowStep<TException, TContext>(condition, message, action));
            return this;
        }

        public IWorkflowBuilder<TContext> StopAsync()
        {
            _workflow.AddStep(new WorkflowStopStep<TContext>());
            return this;
        }

        public IWorkflowBuilder<TContext> StopAsync<TOuterContext>(TOuterContext outerContext) where TOuterContext : WorkflowBaseContext
        {
            _workflow.AddStep(new WorkflowStopWithOuterStopStep<TContext>(outerContext));
            return this;
        }

        public IWorkflowBuilder<TContext> Stop(Func<TContext, bool> condition)
        {
            _workflow.AddStep(new WorkflowConditionStopStep<TContext>(condition));
            return this;
        }

        public IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task<bool>> condition)
        {
            _workflow.AddStep(new WorkflowConditionStopStep<TContext>(condition));
            return this;
        }

        public IWorkflowBuilder<TContext> Stop(Action<TContext> action)
        {
            _workflow.AddStep(new WorkflowActionStopStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task> action)
        {
            _workflow.AddStep(new WorkflowActionStopStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> Stop(Func<TContext, bool> condition, Action<TContext> action)
        {
            _workflow.AddStep(new WorkflowActionConditionStopStep<TContext>(condition, action));
            return this;
        }

        public IWorkflowBuilder<TContext> StopAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> action)
        {
            _workflow.AddStep(new WorkflowActionConditionStopStep<TContext>(condition, action));
            return this;
        }

        public IWorkflowBuilder<TContext> While(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configure)
        {
            var builder = _workflowFactory.Create<IWorkflowBuilder<TContext>>();
            configure(builder);
            var subWorkflow = builder.Build();
            _workflow.AddStep(new WorkflowWhileStep<TContext>(condition, subWorkflow));
            return this;
        }

        public IWorkflowBuilder<TContext> WhileAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configure)
        {
            var builder = _workflowFactory.Create<IWorkflowBuilder<TContext>>();
            configure(builder);
            var subWorkflow = builder.Build();
            _workflow.AddStep(new WorkflowWhileStep<TContext>(condition, subWorkflow));
            return this;
        }

        public IWorkflowBuilder<TContext> Then<TProperty>(Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, TProperty> actionReturn)
        {
            _workflow.AddStep(new WorkflowReturnStep<TContext, TProperty>(actionReturn, propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> ThenAsync<TProperty>(Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, Task<TProperty>> actionReturn)
        {
            _workflow.AddStep(new WorkflowReturnStep<TContext, TProperty>(actionReturn, propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> Then(Action<TContext> action)
        {
            _workflow.AddStep(new WorkflowStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> ThenAsync(Func<TContext, Task> action)
        {
            _workflow.AddStep(new WorkflowStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> ThenAsync<TStep>() where TStep : IWorkflowStep<TContext>
        {
            _workflow.AddStep(_workflowFactory.Create<TStep>());
            return this;
        }

        public IWorkflowBuilder<TContext> ThenAsync<TStep, TParameter>(TParameter parameter) where TStep : IWorkflowParameterStep<TContext, TParameter>
        {
            var step = _workflowFactory.Create<TStep>();
            step.SetParameter(parameter);
            _workflow.AddStep(step);
            return this;
        }

        public IWorkflowBuilder<TContext> ThenAsync<TStep, TConfig>(Action<TConfig> configure) where TStep : IWorkflowOptionsStep<TContext, TConfig>
        {
            var step = _workflowFactory.Create<TStep>();
            var configuration = Activator.CreateInstance<TConfig>();
            configure?.Invoke(configuration);
            step.SetOptions(configuration);
            _workflow.AddStep(step);
            return this;
        }

        public IWorkflowBuilder<TContext> If(Func<TContext, bool> condition, Action<TContext> action)
        {
            _workflow.AddStep(new WorkflowConditionStep<TContext>(condition, action));
            return this;
        }

        public IWorkflowBuilder<TContext> IfAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> action)
        {
            _workflow.AddStep(new WorkflowConditionStep<TContext>(condition, action));
            return this;
        }

        public IWorkflowBuilder<TContext> IfAsync<TStep>(Func<TContext, Task<bool>> condition) where TStep : IWorkflowStep<TContext>
        {
            var step = _workflowFactory.Create<TStep>();
            var conditionStep = new WorkflowConditionStep<TContext>(condition, step.ExecuteAsync);
            _workflow.AddStep(conditionStep);
            return this;
        }
        
        public IWorkflowBuilder<TContext> IfAsync<TStep>(Func<TContext, bool> condition) where TStep : IWorkflowStep<TContext>
        {
            var step = _workflowFactory.Create<TStep>();
            var conditionStep = new WorkflowConditionStep<TContext>(condition, step.ExecuteAsync);
            _workflow.AddStep(conditionStep);
            return this;
        }

        public IWorkflowBuilder<TContext> WriteLine(Func<TContext, string> action)
        {
            _workflow.AddStep(new WorkflowWriteLineStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> WriteLineAsync(Func<TContext, Task<string>> action)
        {
            _workflow.AddStep(new WorkflowWriteLineStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> Write(Func<TContext, string> action)
        {
            _workflow.AddStep(new WorkflowWriteStep<TContext>(action));
            return this;
        }

        public IWorkflowBuilder<TContext> WriteAsync(Func<TContext, Task<string>> action)
        {
            _workflow.AddStep(new WorkflowWriteStep<TContext>(action));
            return this;
        }
        
        public IWorkflowBuilder<TContext> ReadLine(Expression<Func<TContext, string>> propertyPicker)
        {
            _workflow.AddStep(new WorkflowReadLineStep<TContext>(propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> Read(Expression<Func<TContext, int>> propertyPicker)
        {
            _workflow.AddStep(new WorkflowReadStep<TContext>(propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> ReadKey(Expression<Func<TContext, ConsoleKeyInfo>> propertyPicker)
        {
            _workflow.AddStep(new WorkflowReadKeyStep<TContext>(propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> ReadMultiLine(Expression<Func<TContext, string>> propertyPicker, Action<MultiLineOptions> options)
        {
            var multiLineOptions = new MultiLineOptions();
            options?.Invoke(multiLineOptions);
            _workflow.AddStep(new WorkflowReadMultiLineStep<TContext>(propertyPicker, multiLineOptions));
            return this;
        }

        public IWorkflowBuilder<TContext> If<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, TProperty> actionReturn)
        {
            _workflow.AddStep(new WorkflowConditionReturnStep<TContext, TProperty>(condition, actionReturn, propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> IfAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyPicker, Func<TContext, Task<TProperty>> actionReturn)
        {
            _workflow.AddStep(new WorkflowConditionReturnStep<TContext, TProperty>(condition, actionReturn, propertyPicker));
            return this;
        }

        public IWorkflowBuilder<TContext> IfFlow(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configure)
        {
            var builder = _workflowFactory.Create<IWorkflowBuilder<TContext>>();
            configure(builder);
            var subWorkflow = builder.Build();
            _workflow.AddStep(new WorkflowFlowConditionStep<TContext>(condition, subWorkflow));
            return this;
        }

        public IWorkflowBuilder<TContext> IfFlowAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configure)
        {
            var builder = _workflowFactory.Create<IWorkflowBuilder<TContext>>();
            configure(builder);
            var subWorkflow = builder.Build();
            _workflow.AddStep(new WorkflowFlowConditionStep<TContext>(condition, subWorkflow));
            return this;
        }

        public IWorkflowBuilder<TContext> IfElse(Func<TContext, bool> condition, Action<TContext> ifStep, Action<TContext> elseStep)
        {
            _workflow.AddStep(new WorkflowIfElseStep<TContext>(condition, ifStep, elseStep));
            return this;
        }

        public IWorkflowBuilder<TContext> IfElseAsync(Func<TContext, Task<bool>> condition, Func<TContext, Task> ifStep, Func<TContext, Task> elseStep)
        {
            _workflow.AddStep(new WorkflowIfElseStep<TContext>(condition, ifStep, elseStep));
            return this;
        }

        public IWorkflowBuilder<TContext> IfElse<TProperty>(
            Func<TContext, bool> condition,
            Expression<Func<TContext, TProperty>> propertyIfPicker,
            Func<TContext, TProperty> actionIfReturn,
            Expression<Func<TContext, TProperty>> propertyElsePicker,
            Func<TContext, TProperty> actionElseReturn)
        {
            _workflow.AddStep(new WorkflowIfElseReturnStep<TContext, TProperty>(
                condition,
                actionIfReturn,
                propertyIfPicker,
                actionElseReturn,
                propertyElsePicker)
            );
            return this;
        }

        public IWorkflowBuilder<TContext> IfElseAsync<TProperty>(
            Func<TContext, Task<bool>> condition,
            Expression<Func<TContext, TProperty>> propertyIfPicker,
            Func<TContext, Task<TProperty>> actionIfReturn,
            Expression<Func<TContext, TProperty>> propertyElsePicker,
            Func<TContext, Task<TProperty>> actionElseReturn)
        {
            _workflow.AddStep(new WorkflowIfElseReturnStep<TContext, TProperty>(
                condition,
                actionIfReturn,
                propertyIfPicker,
                actionElseReturn,
                propertyElsePicker)
            );
            return this;
        }

        public IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, TProperty> actionIfReturn, Action<TContext> actionElse)
        {
            _workflow.AddStep(new WorkflowIfElseIfReturnStep<TContext, TProperty>(
                condition,
                actionIfReturn,
                propertyIfPicker,
                actionElse)
            );
            return this;
        }

        public IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Expression<Func<TContext, TProperty>> propertyIfPicker, Func<TContext, Task<TProperty>> actionIfReturn, Func<TContext, Task> actionElse)
        {
            _workflow.AddStep(new WorkflowIfElseIfReturnStep<TContext, TProperty>(
                condition,
                actionIfReturn,
                propertyIfPicker,
                actionElse)
            );
            return this;
        }

        public IWorkflowBuilder<TContext> IfElse<TProperty>(Func<TContext, bool> condition, Action<TContext> actionIf, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, TProperty> actionElseReturn)
        {
            _workflow.AddStep(new WorkflowIfElseElseReturnStep<TContext, TProperty>(
                condition,
                actionIf,
                actionElseReturn,
                propertyElsePicker)
            );
            return this;
        }

        public IWorkflowBuilder<TContext> IfElseAsync<TProperty>(Func<TContext, Task<bool>> condition, Func<TContext, Task> actionIf, Expression<Func<TContext, TProperty>> propertyElsePicker, Func<TContext, Task<TProperty>> actionElseReturn)
        {
            _workflow.AddStep(new WorkflowIfElseElseReturnStep<TContext, TProperty>(
                condition,
                actionIf,
                actionElseReturn,
                propertyElsePicker)
            );
            return this;
        }

        public IWorkflowBuilder<TContext> IfElseAsync<TIfStep, TElseStep>(Func<TContext, Task<bool>> condition)
            where TIfStep : IWorkflowStep<TContext>
            where TElseStep : IWorkflowStep<TContext>
        {
            var ifStep = _workflowFactory.Create<TIfStep>();
            var elseStep = _workflowFactory.Create<TElseStep>();
            _workflow.AddStep(new WorkflowIfElseStep<TContext>(condition, ifStep.ExecuteAsync, elseStep.ExecuteAsync));
            return this;
        }

        public IWorkflowBuilder<TContext> IfElseFlow(Func<TContext, bool> condition, Action<IWorkflowBuilder<TContext>> configureIf, Action<IWorkflowBuilder<TContext>> configureElse)
        {
            var builder = _workflowFactory.Create<IWorkflowBuilder<TContext>>();

            configureIf(builder);
            var subWorkflowIf = builder.Build();

            configureElse(builder);
            var subWorkflowElse = builder.Build();

            _workflow.AddStep(new WorkflowIfElseFlowStep<TContext>(condition, subWorkflowIf, subWorkflowElse));

            return this;
        }

        public IWorkflowBuilder<TContext> IfElseFlowAsync(Func<TContext, Task<bool>> condition, Action<IWorkflowBuilder<TContext>> configureIf, Action<IWorkflowBuilder<TContext>> configureElse)
        {
            var builder = _workflowFactory.Create<IWorkflowBuilder<TContext>>();

            configureIf(builder);
            var subWorkflowIf = builder.Build();

            configureElse(builder);
            var subWorkflowElse = builder.Build();

            _workflow.AddStep(new WorkflowIfElseFlowStep<TContext>(condition, subWorkflowIf, subWorkflowElse));

            return this;
        }

        public IWorkflow<TContext> Build()
        {
            var workflow = _workflow;
            _workflow = new Workflow<TContext>();
            return workflow;
        }
    }
}