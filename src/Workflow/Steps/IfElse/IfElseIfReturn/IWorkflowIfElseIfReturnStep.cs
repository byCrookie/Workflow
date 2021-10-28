namespace Workflow.Steps.IfElse.IfElseIfReturn
{
    internal interface IWorkflowIfElseIfReturnStep<in TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
    {}
}