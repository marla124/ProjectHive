using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data;
using ProjectHive.Services.AuthAPI.Data.Entities;

namespace ProjectHive.Services.AuthAPI.Extensions;

public static class AppBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<ProjectHiveAuthDbContext>();

        if (!await dbContext.UserRoles.AnyAsync())
        {
            var roles = new List<UserRole>
            {
                new UserRole { Role = "Admin" },
                new UserRole { Role = "User" },
                // Add more default roles as needed
            };

            await dbContext.UserRoles.AddRangeAsync(roles);
            await dbContext.SaveChangesAsync();
        }

        return app;
    }
}
