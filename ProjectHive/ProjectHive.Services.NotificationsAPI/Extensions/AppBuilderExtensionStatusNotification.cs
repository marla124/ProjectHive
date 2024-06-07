using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.NotificationsAPI.Data;
using ProjectHive.Services.NotificationsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectAPI.Extensions;

public static class AppBuilderExtensionStatusNotification
{
    public static async Task<IApplicationBuilder> PrepareDatabaseNotification(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<ProjectHiveNotificationsDbContext>();

        if (!await dbContext.NotificationStatuses.AnyAsync())
        {
            var status = new List<NotificationStatus>
            {
                new NotificationStatus { Name = "Sent" },
                new NotificationStatus { Name = "Delivered" },
                new NotificationStatus { Name = "Read" },
                new NotificationStatus { Name = "Error" }
            };

            await dbContext.NotificationStatuses.AddRangeAsync(status);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
