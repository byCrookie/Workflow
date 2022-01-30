using Autofac;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Workflow.Autofac.Tests;

[TestClass]
public class WorkflowAutofacTest
{
    private IContainer _container = null!;

    [TestInitialize]
    public void Initialize()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule<TestModule>();
        containerBuilder.AddWorkflow();
        _container = containerBuilder.Build();
    }

    [TestMethod]
    public void Resolve_WhenWorkflowWasAdded_ThenCanResolveWorkflowBuilder()
    {
        var workflowBuilder = _container.Resolve<IWorkflowBuilder<WorkflowTestContext>>();

        workflowBuilder.Should().NotBeNull();
    }
    
    [TestMethod]
    public async Task Resolve_WhenWorkflowWasAdded_ThenCanBuildWorkflowWithCustomSteps()
    {
        var workflowBuilder = _container.Resolve<IWorkflowBuilder<WorkflowTestContext>>();

        var workflow = workflowBuilder
            .ThenAsync<WorkflowTestStep<WorkflowTestContext>>()
            .Build();

        var result = await workflow.RunAsync(new WorkflowTestContext()).ConfigureAwait(false);

        result.Valid.Should().BeTrue();
    }
}