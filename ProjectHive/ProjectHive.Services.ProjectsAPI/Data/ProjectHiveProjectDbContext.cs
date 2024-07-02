using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data.Entities;


namespace ProjectHive.Services.ProjectsAPI.Data;

public class ProjectHiveProjectDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectStatus> ProjectStatuses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<StatusTasks> TasksStatuses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public ProjectHiveProjectDbContext(DbContextOptions<ProjectHiveProjectDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProject>()
            .HasKey(up => new { up.UserId, up.ProjectId });
        modelBuilder.Entity<UserProject>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserProjects)
            .HasForeignKey(up => up.UserId);
        modelBuilder.Entity<UserProject>()
            .HasOne(up => up.Project)
            .WithMany(p => p.UserProjects)
            .HasForeignKey(up => up.ProjectId);
    }
}

