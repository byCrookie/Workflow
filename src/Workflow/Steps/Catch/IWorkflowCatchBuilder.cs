using System;
using System.Threading.Tasks;

namespace Workflow.Steps.Catch
{
    public interface IWorkflowCatchBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> Catch(Action<TContext> action);
        IWorkflowBuilder<TContext> Catch<TException>(Action<TContext> action) where TException : Exception;
        IWorkflowBuilder<TContext> CatchAsync(Func<TContext, Task> action);
        IWorkflowBuilder<TContext> CatchAsync<TException>(Func<TContext, Task> action) where TException : Exception;
    }
}