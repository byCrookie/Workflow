using Autofac;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Workflow.Autofac;

namespace Workflow.Tests;

[TestClass]
public sealed class WorkflowTest
{
    private IWorkflowBuilder<WorkflowTestContext> _workflowBuilder = null!;

    private const int Error = 9999;

    [TestInitialize]
    public void Initialize()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.AddWorkflow();
        containerBuilder.RegisterModule(new WorkflowTestModule());
        var container = containerBuilder.Build();

        _workflowBuilder = container.Resolve<IWorkflowBuilder<WorkflowTestContext>>();
    }

    [TestMethod]
    public async Task RunAsync_WhenWorkflowIsConfigured_ThenExecute()
    {
        var workflow = _workflowBuilder
            .Then(c => c.Flow.Add(1))
            .ThenAsync(c =>
            {
                c.Flow.Add(2);
                return Task.CompletedTask;
            })
            .Then(c => c.Set1, _ => true)
            .ThenAsync(c => c.Set2, _ => Task.FromResult(true))
            .If(_ => true, c => c.Flow.Add(3))
            .If(_ => false, c => c.Flow.Add(Error))
            .If(_ => true, c => c.Set3, _ => true)
            .IfFlow(_ => true, ifFlow => ifFlow.Then(c => c.Flow.Add(4)))
            .IfFlow(_ => false, ifFlow => ifFlow.Then(c => c.Flow.Add(Error)))
            .IfElseFlow(_ => true,
                ifFlow => ifFlow.Then(c => c.Flow.Add(5)),
                elseFlow => elseFlow.Then(c => c.Flow.Add(Error)
                )
            )
            .IfElseFlow(_ => false,
                ifFlow => ifFlow.Then(c => c.Flow.Add(Error)),
                elseFlow => elseFlow.Then(c => c.Flow.Add(6)
                )
            )
            .While(c => c.Counter < 2, whileFlow => whileFlow.Then(c => c.Flow.Add(7)).Then(c => c.Counter++))
            .ThenAsync<IWorkflowTestOptionStep<WorkflowTestContext, WorkflowTestOptions>, WorkflowTestOptions>(
                options => options.Number = 8
            )
            .Build();

        var result = await workflow.RunAsync(new WorkflowTestContext()).ConfigureAwait(false);

        result.Counter.Should().Be(2);
        result.Flow.Should().NotContain(Error);
        result.Set1.Should().BeTrue();
        result.Set2.Should().BeTrue();
        result.Set3.Should().BeTrue();
        result.Flow.Should().BeEquivalentTo(new List<int>
        {
            1, 2, 3, 4, 5, 6, 7, 7, 8
        });
    }
}