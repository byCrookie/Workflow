﻿namespace Workflow;

public abstract class WorkflowBaseContext
{
    internal Exception? Exception { get; set; }
    internal bool IsStop { get; set; }

    public Task<bool> ShouldExecuteAsync()
    {
        return Task.FromResult(!IsStop);
    }

    internal string PropertiesToString<TContext>() where TContext : WorkflowBaseContext
    {
        var context = this as TContext;
        var properties = context?.GetType().GetProperties().Select(prop => new { prop.Name, Value = prop.GetValue(context) });
        return string.Join(", ", properties?.Select(prop => $"Name: {prop.Name} Value: {prop.Value}") ?? Enumerable.Empty<string>());
    }
}