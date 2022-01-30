using Jab;

namespace Workflow.Jab.Tests;

[ServiceProvider]
[Import(typeof(IWorkflowModule))]
[Transient(typeof(IWorkflowTestStep<WorkflowTestContext>), typeof(WorkflowTestStep<WorkflowTestContext>))]
internal partial class ServiceProvider
{
    
}