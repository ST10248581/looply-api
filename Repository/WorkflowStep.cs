using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class WorkflowStep
{
    public Guid StepId { get; set; }

    public Guid WorkflowId { get; set; }

    public int StepOrder { get; set; }

    public string StepType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Config { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ExecutionLog> ExecutionLogs { get; set; } = new List<ExecutionLog>();

    public virtual Workflow Workflow { get; set; } = null!;

    public virtual ICollection<WorkflowStepRun> WorkflowStepRuns { get; set; } = new List<WorkflowStepRun>();
}
