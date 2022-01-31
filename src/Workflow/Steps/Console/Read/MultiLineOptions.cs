using JetBrains.Annotations;

namespace Workflow.Steps.Console.Read;

public class WorkflowMultiLineOptions
{
    public WorkflowMultiLineOptions()
    {
        EndOfInput = ":q";
        RemoveEndOfInput = true;
        ShouldTrimLines = false;
    }
    
    [UsedImplicitly]
    public string EndOfInput { get; set; }
    
    [UsedImplicitly]
    public bool RemoveEndOfInput { get; set; }
    
    [UsedImplicitly]
    public bool ShouldTrimLines { get; set; }
}