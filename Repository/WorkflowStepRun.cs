using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class WorkflowStepRun
{
    public Guid StepRunId { get; set; }

    public Guid RunId { get; set; }

    public Guid StepId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Output { get; set; }

    public virtual WorkflowRun Run { get; set; } = null!;

    public virtual WorkflowStep Step { get; set; } = null!;
}
