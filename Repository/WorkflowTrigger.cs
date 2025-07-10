using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class WorkflowTrigger
{
    public Guid TriggerId { get; set; }

    public Guid WorkflowId { get; set; }

    public string TriggerType { get; set; } = null!;

    public string Config { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Workflow Workflow { get; set; } = null!;
}
