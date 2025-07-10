using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class ExecutionLog
{
    public Guid LogId { get; set; }

    public Guid RunId { get; set; }

    public Guid? StepId { get; set; }

    public string LogLevel { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual WorkflowRun Run { get; set; } = null!;

    public virtual WorkflowStep? Step { get; set; }
}
