namespace Workflow.Jab.Tests;

internal interface IWorkflowTestStep<in TContext> : IWorkflowStep<TContext> where TContext : WorkflowTestContext
{
}