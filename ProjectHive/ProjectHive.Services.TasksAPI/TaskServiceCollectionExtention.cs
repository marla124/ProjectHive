using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.TasksAPI.Data;

namespace ProjectHive.Services.TasksAPI
{
    public static class TaskServiceCollectionExtention
    {
        public static void RegisterServices
                (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ProjectHiveDbContext>(opt => opt.UseNpgsql(connectionString));
        }
    }
}
