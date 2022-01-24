namespace Workflow.Steps.If;

internal interface IWorkflowConditionReturnStep<in TContext> : IWorkflowStep<TContext> where TContext : WorkflowBaseContext
{}