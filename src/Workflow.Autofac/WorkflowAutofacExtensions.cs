using Autofac;

namespace Workflow.Autofac;

public static class WorkflowAutofacExtensions
{
    public static ContainerBuilder AddWorkflow(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule<WorkflowModule>();
        return containerBuilder;
    }
}