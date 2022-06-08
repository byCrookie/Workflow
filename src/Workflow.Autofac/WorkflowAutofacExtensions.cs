using Autofac;

namespace Workflow.Autofac;

/// <summary>
/// Extension method to register all components necessary to run a workflow.
/// </summary>
public static class WorkflowAutofacExtensions
{
    public static ContainerBuilder AddWorkflow(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule<WorkflowModule>();
        return containerBuilder;
    }
}