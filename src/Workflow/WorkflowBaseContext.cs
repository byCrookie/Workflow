namespace Workflow;

/// <summary>
/// Context to exchange data between different steps. Custom implementation allows for additional properties. 
/// </summary>
public abstract class WorkflowBaseContext
{
    internal Exception? Exception { get; set; }
    internal bool IsStop { get; set; }

    public Task<bool> ShouldExecuteAsync()
    {
        return Task.FromResult(!IsStop);
    }

    internal string? PropertiesToString<TContext>() where TContext : WorkflowBaseContext
    {
        var context = this as TContext;
        var properties = context?
            .GetType()
            .GetProperties()
            .Select(prop => new { prop.Name, Value = prop.GetValue(context) })
            .ToList();

        return properties is not null
            ? string.Join(", ", properties.Select(prop => $"Name: {prop.Name} Value: {prop.Value}"))
            : null;
    }
}