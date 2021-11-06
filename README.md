# Workflow
A dynamic workflow library for .NET applications (mainly console).

### Usage
Workflows make it easy to create a console application flow. Workflow has various methods from reading multiline user content to using custom built steps.

#### Example
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
