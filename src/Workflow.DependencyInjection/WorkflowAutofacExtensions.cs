using Microsoft.Extensions.DependencyInjection;

namespace Workflow.DependencyInjection;

/// <summary>
/// Extension method to register all components necessary to run a workflow.
/// </summary>
public static class WorkflowAutofacExtensions
{
    public static IServiceCollection AddWorkflow(this IServiceCollection services)
    {
        new WorkflowModule().Load(services);
        return services;
    }
}