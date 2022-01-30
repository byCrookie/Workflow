using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Workflow.Jab.Tests;

[TestClass]
public class WorkflowJabTest
{
    private WorkflowServiceProvider _serviceProvider = null!;

    [TestInitialize]
    public void Initialize()
    {
        _serviceProvider = new WorkflowServiceProvider();
    }

    [TestMethod]
    public void Resolve_WhenWorkflowWasAdded_ThenCanResolveWorkflowBuilder()
    {
        var workflowBuilder = _serviceProvider.GetService<IWorkflowBuilder<WorkflowTestContext>>();

        workflowBuilder.Should().NotBeNull();
    }
    
    [TestMethod]
    public async Task Resolve_WhenWorkflowWasAdded_ThenCanBuildWorkflowWithCustomSteps()
    {
        var workflowBuilder = _serviceProvider.GetService<IWorkflowBuilder<WorkflowTestContext>>();

        var workflow = workflowBuilder
            .ThenAsync<IWorkflowTestStep<WorkflowTestContext>>()
            .Build();

        var result = await workflow.RunAsync(new WorkflowTestContext()).ConfigureAwait(false);

        result.Valid.Should().BeTrue();
    }
}