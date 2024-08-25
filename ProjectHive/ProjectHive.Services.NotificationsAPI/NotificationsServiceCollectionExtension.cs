using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.NotificationsAPI;
using ProjectHive.Services.NotificationsAPI.Data;
using ProjectHive.Services.NotificationsAPI.Services;
using System.Text;

namespace ProjectHive.Services.AuthAPI;

public static class NotificationsServiceCollectionExtension
{
    public static void RegisterServicesForNotifications
    (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ProjectHiveNotificationsDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddScoped<INotificationService, NotificationService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSignalR();
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<Program>()
        );
    }
}
