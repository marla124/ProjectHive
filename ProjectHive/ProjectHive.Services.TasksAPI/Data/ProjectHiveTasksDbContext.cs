using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.TasksAPI.Data.Entities;
using System.Collections.Generic;

namespace ProjectHive.Services.TasksAPI.Data
{
    public class ProjectHiveTasksDbContext : DbContext
    {
        DbSet<Tasks> Tasks { get; set; }
        DbSet<StatusTasks> StatusesTask { get; set; }
        DbSet<Comment> Comments { get; set; }
        public ProjectHiveTasksDbContext(DbContextOptions<ProjectHiveTasksDbContext> options) : base(options) { }
    }
}
