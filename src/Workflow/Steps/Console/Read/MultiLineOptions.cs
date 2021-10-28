namespace Workflow.Steps.Console.Read
{
    public class MultiLineOptions
    {
        public string EndOfInput { get; set; } = ":q";
        public bool RemoveEndOfInput { get; set; } = true;
        public bool ShouldTrimLines { get; set; } = false;
    }
}