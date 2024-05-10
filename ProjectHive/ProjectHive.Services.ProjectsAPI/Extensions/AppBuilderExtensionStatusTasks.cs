using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectAPI.Extensions;

public static class AppBuilderExtensionStatusTasks
{
    public static async Task<IApplicationBuilder> PrepareDatabaseTasks(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<ProjectHiveProjectDbContext>();

        if (!await dbContext.ProjectStatuses.AnyAsync())
        {
            var status = new List<StatusTasks>
            {
                new StatusTasks { Name = "Open" },
                new StatusTasks { Name = "In Progress" },
                new StatusTasks { Name = "Done" },
                new StatusTasks { Name = "Cancelled" },
            };

            await dbContext.TasksStatuses.AddRangeAsync(status);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
