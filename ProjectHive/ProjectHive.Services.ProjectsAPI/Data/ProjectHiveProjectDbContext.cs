using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data.Entities;


namespace ProjectHive.Services.ProjectsAPI.Data
{
    public class ProjectHiveProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public ProjectHiveProjectDbContext(DbContextOptions<ProjectHiveProjectDbContext> options) : base(options) { }
    }
}
