using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectAPI.Extensions;

public static class AppBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<ProjectHiveProjectDbContext>();

        if (!await dbContext.ProjectStatuses.AnyAsync())
        {
            var status = new List<ProjectStatus>
            {
                new ProjectStatus { Name = "Open" },
                new ProjectStatus { Name = "In Progress" },
                new ProjectStatus { Name = "Done" },
                new ProjectStatus { Name = "Cancelled" },
            };

            await dbContext.ProjectStatuses.AddRangeAsync(status);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
