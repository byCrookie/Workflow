using Workflow.Steps.Catch;

namespace Workflow;

internal class Workflow<TContext> : IWorkflow<TContext> where TContext: WorkflowBaseContext
{
    private readonly List<IWorkflowStep<TContext>> _steps;

    public Workflow()
    {
        _steps = new List<IWorkflowStep<TContext>>();
    }

    public void AddStep(IWorkflowStep<TContext> step)
    {
        _steps.Add(step);
    }

    public async Task<TContext> RunAsync(TContext context)
    {
        var executedSteps = new List<IWorkflowStep<TContext>>();

        while (executedSteps.Count != _steps.Count)
        {
            await ExecuteStepsAsync(context, executedSteps, _steps).ConfigureAwait(true);
        }

        return context;
    }

    private async Task ExecuteStepsAsync(TContext context, ICollection<IWorkflowStep<TContext>> executedSteps,
        IEnumerable<IWorkflowStep<TContext>> stepsToExecute)
    {
        try
        {
            foreach (var step in stepsToExecute)
            {
                executedSteps.Add(step);
                    
                if (await step.ShouldExecuteAsync(context).ConfigureAwait(true))
                {
                    await step.ExecuteAsync(context).ConfigureAwait(true);
                }
            }
        }
        catch (Exception exception)
        {
            var remainingSteps = _steps.Except(executedSteps).ToList();
            var workflowException = new WorkflowException<TContext>(exception, context, executedSteps.Last());

            if (remainingSteps.Any(step => step is WorkflowCatchStep<TContext>))
            {
                context.Exception = workflowException;
                CatchExceptionFirstCatchStep(remainingSteps);
                await ExecuteStepsAsync(context, executedSteps, remainingSteps).ConfigureAwait(true);
            }
            else
            {
                throw workflowException;
            }
        }
    }

    private static void CatchExceptionFirstCatchStep(List<IWorkflowStep<TContext>> stepsTodo)
    {
        var catchStep = stepsTodo.First(step => step is WorkflowCatchStep<TContext>);
        stepsTodo.RemoveRange(0, stepsTodo.IndexOf(catchStep));
    }
}