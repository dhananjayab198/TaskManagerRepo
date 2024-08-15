
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskNote> TaskNotes { get; set; }
    public DbSet<TaskAttachment> TaskAttachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
         
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.TeamMembers)
            .WithOne(e => e.Manager)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Task>()
            .HasOne(t => t.Employee)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.AssignedTo)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskNote>()
            .HasOne(n => n.Task)
            .WithMany(t => t.Notes)
            .HasForeignKey(n => n.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskNote>()
           .HasOne(n => n.Employee)
           .WithMany()
           .HasForeignKey(n => n.CreatedBy)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskAttachment>()
            .HasOne(a => a.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(a => a.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskAttachment>()
            .HasOne(a => a.Employee)
            .WithMany()
            .HasForeignKey(a => a.UploadedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
