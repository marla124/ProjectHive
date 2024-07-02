using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectAPI.Extensions;

public static class AppBuilderExtensionStatusProject
{
    public static async Task<IApplicationBuilder> PrepareDatabaseProject(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<ProjectHiveProjectDbContext>();

        if (!await dbContext.ProjectStatuses.AnyAsync())
        {
            var status = new List<ProjectStatus>
            {
                new ProjectStatus { Name = "Processing" },
                new ProjectStatus { Name = "Distributing" },
                new ProjectStatus { Name = "Abandoned" }
            };

            await dbContext.ProjectStatuses.AddRangeAsync(status);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
