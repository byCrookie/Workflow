using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workflow.Steps.Catch;

namespace Workflow
{
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
            var doneSteps = new List<IWorkflowStep<TContext>>();

            while (doneSteps.Count != _steps.Count)
            {
                await ExecuteStepsAsync(context, doneSteps, _steps).ConfigureAwait(false);
            }

            return context;
        }

        private async Task ExecuteStepsAsync(TContext context, ICollection<IWorkflowStep<TContext>> doneSteps,
            IEnumerable<IWorkflowStep<TContext>> steps)
        {
            try
            {
                foreach (var step in steps)
                {
                    doneSteps.Add(step);
                    
                    if (await step.ShouldExecuteAsync(context).ConfigureAwait(false))
                    {
                        await step.ExecuteAsync(context).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception e)
            {
                var stepsTodo = _steps.Except(doneSteps).ToList();
                var exception = new WorkflowException<TContext>(e, context, doneSteps.Last());

                if (stepsTodo.Any(step => step is WorkflowCatchStep<TContext>))
                {
                    context.Exception = exception;
                    var catchStep = stepsTodo.First(step => step is WorkflowCatchStep<TContext>);
                    stepsTodo.RemoveRange(0, stepsTodo.IndexOf(catchStep));
                    await ExecuteStepsAsync(context, doneSteps, stepsTodo).ConfigureAwait(false);
                }
                else
                {
                    throw exception;
                }
            }
        }
    }
}