using Autofac;
using Workflow.Autofac.Factory;

namespace Workflow.Autofac;

/// <summary>
/// Registers all components necessary to run a workflow.
/// Additionally: All custom workflow steps have to be registered for their context implementation separately.
/// </summary>
public class WorkflowModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(WorkflowBuilder<>)).As(typeof(IWorkflowBuilder<>));

        builder.RegisterModule<WorkflowFactoryModule>();

        base.Load(builder);
    }
}