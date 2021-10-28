using System;
using System.Collections.Generic;
using System.Linq;

namespace Workflow
{
    public class WorkflowBaseContext
    {
        public Exception Exception { get; set; }
        public bool IsStop { get; set; }

        public bool ShouldExecute()
        {
            return !IsStop;
        }

        public string PropertiesToString<TContext>() where TContext : WorkflowBaseContext
        {
            var context = this as TContext;
            var properties = context?.GetType().GetProperties().Select(prop => (prop.Name, prop.GetValue(context))) ?? new List<(string, object)>();
            return string.Join(", ", properties.Select(prop => $"Name: {prop.Item1} Value: {prop.Item2}"));
        }
    }
}