# Workflow
A dynamic workflow library for .NET applications (mainly console).

## Dependencies & Acknowledgements
This project relies on following to packages
* BuildSdk: https://github.com/byCrookie/BuildSdk
* DependencyInjection: https://github.com/byCrookie/DependencyInjection
* Jab Dependency-Injection: https://github.com/pakrym/jab
* Autofac: https://github.com/autofac/Autofac

## How to use

### Local Nuget Source
* Download the nuget package
* Add download path as nuget source
* Reference the package in your project

### Remote Nuget-Source

Add remote nuget source to your nuget.config:

* {Token}: Z2hwX1hybmFLaVIyTm1zaGVWRVpqMjVLbHZsNTBjdldKYjMzQ2hPeQ== -> Convert Base64 back to Text First
* Execute: dotnet nuget add source --username byCrookie --password {Token} --name byCrookie_Github --store-password-in-clear-text https://nuget.pkg.github.com/byCrookie/index.json

### Dev

* Clone the git repository
* Change the "localPackages" path in the nuget.config

## Contributing / Issues
All contributions are welcome! If you have any issues or feature requests, either implement it yourself or create an issue, thank you.

## Donation
If you like this project, feel free to donate and support further development. Thank you.

* Bitcoin (BTC) Donations using Bitcoin (BTC) Network -> bc1qygqya2w3hgpvy8hupctfkv5x06l69ydq4su2e2
* Ethereum (ETH) Donations using Ethereum (ETH) Network -> 0x1C0416cC1DDaAEEb3017D4b8Dcd3f0B82f4d94C1

## Docs
Workflows make it easy to create a console application flow. Workflow has various methods from reading multiline user content to using custom built steps.

### Dependency Injection

This project includes nuget packages for different dependency injection containers.

* Autofac (Workflow.Autofac)
* Jab (Workflow.Jab)
* Microsoft Dependency Injection (Workflow.DependencyInjection)

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
private async Task<ITypeCodeStrategy?> EvaluateModeAsync()
{
    var workflow = _workflowBuilder
        .Then(context => context.Modes, _ => _modeComposer.ComposeOrdered())
        .WriteLine(context => CreateInputMessage(context.Modes))
        .ReadLine(context => context.Input)
        .While(context => !ModeExists(context.Modes, context), whileFlow => whileFlow
            .WriteLine(_ => $@"{Cuts.Point()} Please select a valid mode")
            .ReadLine(context => context.Input)
            .ThenAsync<IExitOrContinueStep<TypeCodeContext>>()
        )
        .Then(context => context.Mode, context => context.Modes
            .SingleOrDefault(strategy => strategy
                .IsResponsibleFor(context.Input)))
        .Build();

    var workflowContext = await workflow.RunAsync(new TypeCodeContext()).ConfigureAwait(false);
    return workflowContext.Mode;
}
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

        _options = new SelectionStepOptions();
    }

    public async Task ExecuteAsync(TContext context)
    {
        var selectionContext = new SelectionContext();
        context.MapTo(selectionContext);

        var workflow = _workflowBuilder
            .While(c => string.IsNullOrEmpty(c.Input) || c.Selection == 0 || c.Selection > _options.Selections.Count, whileFlow => whileFlow
                .WriteLine(_ => $@"{Cuts.Medium()}")
                .WriteLine(_ => $@"{Cuts.Heading()} Select an option")
                .WriteLine(_ => CreateSelectionMenu(_options.Selections))
                .ReadLine(c => c.Input)
                .ThenAsync<IExitOrContinueStep<SelectionContext>>()
                .IfFlow(c => short.TryParse(c.Input?.Trim(), out _), ifFlow => ifFlow
                    .Then(c => c.Selection, c => Convert.ToInt16(c.Input?.Trim()))
                    .If(c => c.Selection > _options.Selections.Count || c.Selection < 1, _ => System.Console.WriteLine($@"{Cuts.Point()} Option is not valid"))
                )
            )
            .Build();

        var workflowContext = await workflow.RunAsync(selectionContext).ConfigureAwait(false);
        workflowContext.MapTo(context);
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
        return context.ShouldExecuteAsync();
    }

    public void SetOptions(TOptions options)
    {
        _options = options as SelectionStepOptions ?? new SelectionStepOptions();
    }
}
```
