# Workflow
A dynamic workflow library for .NET applications (mainly console).

## Dependencies & Acknowledgements
This project relies on following to packages
* BuildSdk: https://github.com/byCrookie/BuildSdk
* Jab Dependency-Injection: https://github.com/pakrym/jab

## How to use

### Local Nuget Source
* Download the nuget package
* Add download path as nuget source
* Reference the package in your project

### Remote Nuget-Source

Add the following remote nuget source to your nuget.config file.

```xml
<configuration>

  <packageSources>
    <add key="github" value="https://nuget.pkg.github.com/byCrookie/index.json" />
  </packageSources>

  <packageSourceCredentials>
    <github>
      <add key="Username" value="byCrookie" />
      <add key="ClearTextPassword" value="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" />
    </github>
  </packageSourceCredentials>

</configuration>
```

### Dev

* Clone the git repository
* Change the "localPackages" path in the nuget.config

## Contributing / Issues
All contributions are welcome! If you have any issues or feature requests, either implement it yourself or create an issue, thank you.

## Donation
If you like this project, feel free to donate and support the further development. Thank you.

Bitcoin (BTC) Donations using Bitcoin (BTC) Network -> 14vZ2rRTEWXhvfLrxSboTN15k5XuRL1AHq 

## Docs
Workflows make it easy to create a console application flow. Workflow has various methods from reading multiline user content to using custom built steps.

### Dependency Injection

This project includes nuget packages for diffrent dependency injection containers.

* Autofac (Workflow.Autofac)
* Jab (Workflow.Jab)

#### Autofac

```C#
public class FrameworkModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.AddWorkflow();

        base.Load(builder);
    }
}
```

#### Jab

```C#
[ServiceProvider]
[Import(typeof(IWorkflowModule))]
[Transient(typeof(IWorkflowTestStep<WorkflowTestContext>), typeof(WorkflowTestStep<WorkflowTestContext>))]
public partial class CustomServiceProvider
{
}
```

### Example
```C#
var workflow = _workflowBuilder
    .WriteLine(context => $@"{Cuts.Point()} Input type")
    .Then(context => context.TypeName, context => Console.ReadLine())
    .While(context => !_typeProvider.HasByName(context.TypeName.Trim()), whileFlow => whileFlow
        .WriteLine(context => $@"{Cuts.Point()} Type not found")
        .WriteLine(context => $@"{Cuts.Point()} Please input input type")
        .Then(context => context.TypeName, context => Console.ReadLine())
        .IfFlow(context => string.IsNullOrEmpty(context.TypeName), ifFlow => ifFlow
            .WriteLine(context => $@"{Cuts.Point()} Press enter to exit or space to continue")
            .IfFlow(context => Console.ReadKey().Key == ConsoleKey.Enter, ifFlowLeave => ifFlowLeave
                .StopAsync()
            )
        )
    )
    .Then(context => context.SelectedTypes, context => _typeProvider.TryGetByName(context.TypeName.Trim()).ToList())
    .ThenAsync<IMultipleTypeSelectionStep<BuilderContext>>()
    .Stop(c => !c.SelectedType.IsClass, c => Console.WriteLine($@"{Cuts.Point()} Type has to be a class"))
    .Then(context => context.BuilderCode, context => GenerateBuilderCode(context.SelectedType))
    .Build();

var workflowContext = await workflow.RunAsync(new BuilderContext()).ConfigureAwait(false);
return workflowContext.BuilderCode;
```

#### Custom Step

```C#
var workflow = _workflowEvaluationBuilder
    .ThenAsync<ISelectionStep<UnitTestDependencyEvaluationContext, SelectionStepOptions>,
        SelectionStepOptions>(config =>
        {
            config.Selections = new List<string>
            {
                "Input type by name",
                "Input dependencies manually (,)"
            };
        }
    )
```

```C#
internal class SelectionStep<TContext, TOptions> :
        ISelectionStep<TContext, TOptions>
        where TContext : WorkflowBaseContext, ISelectionContext
    {
        private readonly IWorkflowBuilder<SelectionContext> _workflowBuilder;
        private SelectionStepOptions _options;

        public SelectionStep(IWorkflowBuilder<SelectionContext> workflowBuilder)
        {
            _workflowBuilder = workflowBuilder;
        }

        public async Task ExecuteAsync(TContext context)
        {
            var selectionContext = new SelectionContext();

            var workflow = _workflowBuilder
                .While(c => c.Selection == 0 || c.Selection > _options.Selections.Count, whileFlow => whileFlow
                    .WriteLine(_ => $@"{Cuts.Medium()}")
                    .WriteLine(_ => $@"{Cuts.Heading()} Select an option")
                    .WriteLine(_ => CreateSelectionMenu(_options.Selections))
                    .ReadLine(c => c.Input)
                    .IfFlow(c => string.IsNullOrEmpty(c.Input.Trim()), ifFlow => ifFlow
                        .WriteLine(_ => $@"{Cuts.Point()} Press enter to exit or space to continue")
                        .IfFlow(_ => Console.ReadKey().Key == ConsoleKey.Enter, ifFlowLeave => ifFlowLeave
                            .StopAsync(context)
                        )
                    )
                    .Then(c => c.Selection, c => Convert.ToInt16(c.Input.Trim()))
                    .If(c => c.Selection > _options.Selections.Count || c.Selection < 1, _ => Console.WriteLine($@"{Cuts.Point()} Option is not valid"))
                )
                .Build();

            var workflowContext = await workflow.RunAsync(selectionContext).ConfigureAwait(false);
            context.Selection = workflowContext.Selection;
        }

        private static string CreateSelectionMenu(IReadOnlyList<string> selections)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"{Cuts.Medium()}");
            for (var index = 0; index < selections.Count; index++)
            {
                stringBuilder.AppendLine($@"{Cuts.Point()} {index + 1} - {selections[index]}");
            }

            stringBuilder.Append($@"{Cuts.Medium()}");
            return stringBuilder.ToString();
        }

        public Task<bool> ShouldExecuteAsync(TContext context)
        {
            return Task.FromResult(context.ShouldExecute());
        }

        public void SetOptions(TOptions options)
        {
            _options = options as SelectionStepOptions;
        }
    }
```
