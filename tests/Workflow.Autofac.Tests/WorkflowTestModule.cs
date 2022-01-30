using Autofac;

namespace Workflow.Autofac.Tests;

internal class WorkflowTestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(WorkflowTestStep<>)).As(typeof(WorkflowTestStep<>));
        
        base.Load(builder);
    }
}