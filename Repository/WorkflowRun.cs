using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class WorkflowRun
{
    public Guid RunId { get; set; }

    public Guid WorkflowId { get; set; }

    public string TriggeredBy { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual ICollection<ExecutionLog> ExecutionLogs { get; set; } = new List<ExecutionLog>();

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual ICollection<WorkflowStepRun> WorkflowStepRuns { get; set; } = new List<WorkflowStepRun>();
}
