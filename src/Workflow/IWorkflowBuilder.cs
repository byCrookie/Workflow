using Workflow.Steps.Catch;
using Workflow.Steps.Console.Read;
using Workflow.Steps.Console.Write;
using Workflow.Steps.If;
using Workflow.Steps.IfElse;
using Workflow.Steps.Stop;
using Workflow.Steps.Then;
using Workflow.Steps.Throw;
using Workflow.Steps.While;

namespace Workflow;

/// <summary>
/// Configure the workflow with different steps.
/// </summary>
/// <typeparam name="TContext">Type of custom context implementation.</typeparam>
public interface IWorkflowBuilder<TContext> :
    IWorkflowCatchBuilder<TContext>,
    IWorkflowThrowBuilder<TContext>,
    IWorkflowStopBuilder<TContext>,
    IWorkflowWhileBuilder<TContext>,
    IWorkflowThenBuilder<TContext>,
    IWorkflowIfElseBuilder<TContext>,
    IWorkflowIfBuilder<TContext>,
    IWorkflowWriteBuilder<TContext>,
    IWorkflowReadBuilder<TContext>
    where TContext : WorkflowBaseContext
{
    /// <summary>
    /// Builds the configured workflow.
    /// Builder is not thread-safe. Never call same instance in parallel.
    /// </summary>
    /// <returns>Workflow with steps to execute.</returns>
    IWorkflow<TContext> Build();
}