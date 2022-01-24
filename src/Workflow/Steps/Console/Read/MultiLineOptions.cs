namespace Workflow.Steps.Console.Read;

public class WorkflowMultiLineOptions
{
    public string EndOfInput { get; set; } = ":q";
    public bool RemoveEndOfInput { get; set; } = true;
    public bool ShouldTrimLines { get; set; } = false;
}