namespace Workflow.Tests;

internal interface IWorkflowTestOptionStep<in TContext, TOptions> : IWorkflowOptionsStep<TContext, TOptions> where TContext : WorkflowTestContext
{
}