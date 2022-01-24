using System.Collections.Generic;

namespace Workflow.Tests;

public class WorkflowTestContext : WorkflowBaseContext
{
    public WorkflowTestContext()
    {
        Set1 = false;
        Set2 = false;
        Set3 = false;
        Flow = new List<int>();
        Counter = 0;
    }

    public List<int> Flow { get; }
    public bool Set1 { get; set; }
    public bool Set2 { get; set; }
    public bool Set3 { get; set; }
    public int Counter { get; set; }
}