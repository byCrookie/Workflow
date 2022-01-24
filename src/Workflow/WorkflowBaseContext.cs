namespace Workflow
{
    public abstract class WorkflowBaseContext
    {
        public Exception? Exception { get; set; }
        public bool IsStop { get; set; }

        public Task<bool> ShouldExecuteAsync()
        {
            return Task.FromResult(!IsStop);
        }

        public string PropertiesToString<TContext>() where TContext : WorkflowBaseContext
        {
            var context = this as TContext;
            var properties = context?.GetType().GetProperties().Select(prop => new { prop.Name, Value = prop.GetValue(context) });
            return string.Join(", ", properties?.Select(prop => $"Name: {prop.Name} Value: {prop.Value}") ?? Enumerable.Empty<string>());
        }
    }
}