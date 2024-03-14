using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data.Entities;


namespace ProjectHive.Services.ProjectsAPI.Data;

public class ProjectHiveProjectDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectStatus> ProjectStatuses { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<StatusTasks> TasksStatuses { get; set; }


    public ProjectHiveProjectDbContext(DbContextOptions<ProjectHiveProjectDbContext> options) : base(options) { }
}
