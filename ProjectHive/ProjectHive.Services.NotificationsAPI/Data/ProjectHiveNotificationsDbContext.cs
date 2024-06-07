using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.NotificationsAPI.Data.Entities;

namespace ProjectHive.Services.NotificationsAPI.Data
{
    public class ProjectHiveNotificationsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public ProjectHiveNotificationsDbContext
            (DbContextOptions<ProjectHiveNotificationsDbContext> options) : base(options) { }
    }
}
