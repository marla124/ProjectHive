using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.TasksAPI.Data.Entities;
using System.Collections.Generic;

namespace ProjectHive.Services.TasksAPI.Data
{
    public class ProjectHiveDbContext : DbContext
    {
        DbSet<Tasks> Tasks { get; set; }
        DbSet<StatusTasks> StatusGoals { get; set; }
        public ProjectHiveDbContext(DbContextOptions<ProjectHiveDbContext> options) : base(options) { }
    }
}
