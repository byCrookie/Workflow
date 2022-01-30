using Jab;
using Workflow.Factory;

namespace Workflow.Jab.Factory;

[ServiceProviderModule]
[Transient(typeof(IWorkflowFactory), typeof(WorkflowFactory))]
[Transient(typeof(IWorkflowFactory<>), typeof(WorkflowFactory<>))]
[Transient(typeof(IWorkflowFactory<,>), typeof(WorkflowFactory<,>))]
[Transient(typeof(IWorkflowFactory<,,>), typeof(WorkflowFactory<,,>))]
[Transient(typeof(IWorkflowFactory<,,,>), typeof(WorkflowFactory<,,,>))]
[Transient(typeof(IWorkflowFactory<,,,,>), typeof(WorkflowFactory<,,,,>))]
[Transient(typeof(IWorkflowFactory<,,,,,>), typeof(WorkflowFactory<,,,,,>))]
internal interface IWorkflowFactoryModule
{
}