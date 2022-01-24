namespace Workflow.Steps.IfElse.IfElseReturn;

internal interface IWorkflowIfElseReturnStep<in TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{}