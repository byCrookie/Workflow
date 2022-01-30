using Autofac;
using Workflow.Autofac.Factory;

namespace Workflow.Autofac;

public class WorkflowModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(WorkflowBuilder<>)).As(typeof(IWorkflowBuilder<>));

        builder.RegisterModule<WorkflowFactoryModule>();

        base.Load(builder);
    }
}