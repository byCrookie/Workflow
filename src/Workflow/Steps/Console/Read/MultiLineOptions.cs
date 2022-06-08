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
    
    /// <summary>
    /// Define the last character to mark end of input.
    /// </summary>
    [UsedImplicitly]
    public string EndOfInput { get; set; }
    
    /// <summary>
    /// Define if the last character to mark end of input should be included in output.
    /// </summary>
    [UsedImplicitly]
    public bool RemoveEndOfInput { get; set; }
    
    /// <summary>
    /// Define if every line is trimmed.
    /// Removes all leading and trailing white-space characters from every line.
    /// </summary>
    [UsedImplicitly]
    public bool ShouldTrimLines { get; set; }
}