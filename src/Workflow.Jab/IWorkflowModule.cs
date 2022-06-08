using Jab;
using Workflow.Jab.Factory;

namespace Workflow.Jab;

/// <summary>
/// Registers all components necessary to run a workflow.
/// Additionally: Workflow builder needs to be registered for every context implementation separately.
/// Additionally: All custom workflow steps have to be registered for their context implementation separately.
/// </summary>
[ServiceProviderModule]
[Import(typeof(IWorkflowFactoryModule))]
[Transient(typeof(IWorkflowBuilder<>), typeof(WorkflowBuilder<>))]
public interface IWorkflowModule
{
}