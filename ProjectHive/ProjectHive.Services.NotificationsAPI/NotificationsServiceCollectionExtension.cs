using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.NotificationsAPI.Data;
using System.Text;

namespace ProjectHive.Services.AuthAPI;

public static class NotificationsServiceCollectionExtension
{
    public static void RegisterServicesForNotifications
    (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ProjectHiveNotificationsDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSignalR();
    }
}
