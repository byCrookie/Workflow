namespace Workflow.Steps.IfElse
{
    internal class WorkflowIfElseFlowStep<TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {
        private readonly Func<TContext,Task<bool>> _condition;
        private readonly IWorkflow<TContext> _subWorkflowElse;
        private readonly IWorkflow<TContext> _subWorkflowIf;

        public WorkflowIfElseFlowStep(
            Func<TContext,Task<bool>> condition,
            IWorkflow<TContext> subWorkflowIf,
            IWorkflow<TContext> subWorkflowElse)
        {
            _condition = condition;
            _subWorkflowIf = subWorkflowIf;
            _subWorkflowElse = subWorkflowElse;
        }

        public WorkflowIfElseFlowStep(
            Func<TContext,bool> condition,
            IWorkflow<TContext> subWorkflowIf,
            IWorkflow<TContext> subWorkflowElse)
        {
            _condition = context => Task.FromResult(condition(context));
            _subWorkflowIf = subWorkflowIf;
            _subWorkflowElse = subWorkflowElse;
        }

        public async Task ExecuteAsync(TContext context)
        {
            if (await _condition(context).ConfigureAwait(true))
            {
                await _subWorkflowIf.RunAsync(context).ConfigureAwait(true);
            }
            else
            {
                await _subWorkflowElse.RunAsync(context).ConfigureAwait(true);
            }
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return context.ShouldExecuteAsync();
        }
    }
}