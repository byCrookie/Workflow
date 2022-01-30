using Autofac;
using Workflow.Factory;

namespace Workflow.Autofac.Factory;

internal class WorkflowFactoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WorkflowFactory>().As<IWorkflowFactory>();
        builder.RegisterGeneric(typeof(WorkflowFactory<>)).As(typeof(IWorkflowFactory<>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,>)).As(typeof(IWorkflowFactory<,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,>)).As(typeof(IWorkflowFactory<,,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,,>)).As(typeof(IWorkflowFactory<,,,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,,,>)).As(typeof(IWorkflowFactory<,,,,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,,,,>)).As(typeof(IWorkflowFactory<,,,,,>));
            
        base.Load(builder);
    }
}