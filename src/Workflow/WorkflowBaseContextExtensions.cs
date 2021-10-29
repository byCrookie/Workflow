using System;

namespace Workflow
{
    public static class WorkflowBaseContextExtensions
    {
        public static void MapTo(this WorkflowBaseContext origin, WorkflowBaseContext target)
        {
            target.Exception = origin.Exception;
            target.IsStop = origin.IsStop;
        }
        
        public static void SetException(this WorkflowBaseContext context, Exception exception)
        {
            context.Exception = exception;
        }
        
        public static bool HasException(this WorkflowBaseContext context)
        {
            return context.Exception != null;
        }
        
        public static bool IsStopped(this WorkflowBaseContext context)
        {
            return context.IsStop;
        }
    }
}