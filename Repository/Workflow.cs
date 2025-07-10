using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class Workflow
{
    public Guid WorkflowId { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WorkflowRun> WorkflowRuns { get; set; } = new List<WorkflowRun>();

    public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; } = new List<WorkflowStep>();

    public virtual ICollection<WorkflowTrigger> WorkflowTriggers { get; set; } = new List<WorkflowTrigger>();
}
