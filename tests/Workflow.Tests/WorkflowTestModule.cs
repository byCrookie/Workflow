using Autofac;

namespace Workflow.Tests;

internal class WorkflowTestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(WorkflowTestOptionStep<,>)).As(typeof(IWorkflowTestOptionStep<,>));
        
        base.Load(builder);
    }
}