using Autofac;
using DependencyInjection.Factory;

namespace Workflow.Autofac.Factory;

internal class WorkflowFactoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WorkflowFactory>().As<IFactory>();
        builder.RegisterGeneric(typeof(WorkflowFactory<>)).As(typeof(IFactory<>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,>)).As(typeof(IFactory<,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,>)).As(typeof(IFactory<,,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,,>)).As(typeof(IFactory<,,,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,,,>)).As(typeof(IFactory<,,,,>));
        builder.RegisterGeneric(typeof(WorkflowFactory<,,,,,>)).As(typeof(IFactory<,,,,,>));
            
        base.Load(builder);
    }
}