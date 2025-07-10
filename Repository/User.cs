using System;
using System.Collections.Generic;

namespace LooplyAPI.Repository;

public partial class User
{
    public Guid UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
