using Jab;
using Workflow.Jab.Factory;

namespace Workflow.Jab;

[ServiceProviderModule]
[Import(typeof(IWorkflowFactoryModule))]
[Transient(typeof(IWorkflowBuilder<>), typeof(WorkflowBuilder<>))]
public partial interface IWorkflowModule
{
}