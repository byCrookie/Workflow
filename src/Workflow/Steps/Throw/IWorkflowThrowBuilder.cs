using System;
using System.Threading.Tasks;

namespace Workflow.Steps.Throw
{
    public interface IWorkflowThrowBuilder<TContext> where TContext : WorkflowBaseContext
    {
        IWorkflowBuilder<TContext> Throw<TException>(Action<TContext> action) where TException : Exception;
        IWorkflowBuilder<TContext> Throw<TException>(Func<TContext, bool> condition, Action<TContext> action) where TException : Exception;
        IWorkflowBuilder<TContext> Throw<TException>(string message, Action<TContext> action) where TException : Exception;
        IWorkflowBuilder<TContext> Throw<TException>(Func<TContext, bool> condition, string message, Action<TContext> action) where TException : Exception;
        IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task> action) where TException : Exception;
        IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task<bool>> condition, Func<TContext, Task> action) where TException : Exception;
        IWorkflowBuilder<TContext> ThrowAsync<TException>(string message, Func<TContext, Task> action) where TException : Exception;
        IWorkflowBuilder<TContext> ThrowAsync<TException>(Func<TContext, Task<bool>> condition, string message, Func<TContext, Task> action) where TException : Exception;
    }
}