using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LooplyAPI.Repository;

public partial class LooplyDbContext : DbContext
{
    public LooplyDbContext()
    {
    }

    public LooplyDbContext(DbContextOptions<LooplyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<ExecutionLog> ExecutionLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowRun> WorkflowRuns { get; set; }

    public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }

    public virtual DbSet<WorkflowStepRun> WorkflowStepRuns { get; set; }

    public virtual DbSet<WorkflowTrigger> WorkflowTriggers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TROYLAPTOP\\SQLEXPRESS;Database=LooplyDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.ApiKeyId).HasName("PK__ApiKeys__2F1344F2CEEACD2D");

            entity.Property(e => e.ApiKeyId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.KeyHash).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApiKeys_Users");
        });

        modelBuilder.Entity<ExecutionLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Executio__5E548648A44A76E8");

            entity.Property(e => e.LogId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.LogLevel).HasMaxLength(20);

            entity.HasOne(d => d.Run).WithMany(p => p.ExecutionLogs)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Logs_Runs");

            entity.HasOne(d => d.Step).WithMany(p => p.ExecutionLogs)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("FK_Logs_Steps");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CCB3A50CB");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343975F3C0").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.WorkflowId).HasName("PK__Workflow__5704A66A3F8C4A6B");

            entity.Property(e => e.WorkflowId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Version).HasDefaultValue(1);

            entity.HasOne(d => d.User).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workflows_Users");
        });

        modelBuilder.Entity<WorkflowRun>(entity =>
        {
            entity.HasKey(e => e.RunId).HasName("PK__Workflow__A259D4DD39E9A302");

            entity.Property(e => e.RunId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.StartedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TriggeredBy).HasMaxLength(100);

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowRuns)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowRuns_Workflows");
        });

        modelBuilder.Entity<WorkflowStep>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("PK__Workflow__243433578410EABB");

            entity.Property(e => e.StepId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StepType).HasMaxLength(100);

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowSteps)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowSteps_Workflows");
        });

        modelBuilder.Entity<WorkflowStepRun>(entity =>
        {
            entity.HasKey(e => e.StepRunId).HasName("PK__Workflow__504389A985618DDC");

            entity.Property(e => e.StepRunId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.StartedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Run).WithMany(p => p.WorkflowStepRuns)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowStepRuns_Runs");

            entity.HasOne(d => d.Step).WithMany(p => p.WorkflowStepRuns)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowStepRuns_Steps");
        });

        modelBuilder.Entity<WorkflowTrigger>(entity =>
        {
            entity.HasKey(e => e.TriggerId).HasName("PK__Workflow__11321F62F71CD6FC");

            entity.Property(e => e.TriggerId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TriggerType).HasMaxLength(100);

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowTriggers)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowTriggers_Workflows");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
