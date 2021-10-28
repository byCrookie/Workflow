using System.Threading.Tasks;

namespace Workflow
{
    public interface IWorkflowStep<in TContext> where TContext : WorkflowBaseContext
    {
        Task ExecuteAsync(TContext context);
        Task<bool> ShouldExecuteAsync(TContext context);
    }
}