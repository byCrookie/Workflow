using DependencyInjection;
using DependencyInjection.Microsoft.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Workflow.DependencyInjection;

/// <summary>
/// Registers all components necessary to run a workflow.
/// Additionally: All custom workflow steps have to be registered for their context implementation separately.
/// </summary>
public sealed class WorkflowModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddTransient(typeof(IWorkflowBuilder<>), typeof(WorkflowBuilder<>));
        
        AddModule(new DependencyInjectionModule());
        
        base.Load(services);
    }
}