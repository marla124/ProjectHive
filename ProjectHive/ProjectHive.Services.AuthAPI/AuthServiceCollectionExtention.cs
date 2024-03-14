using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data;

namespace ProjectHive.Services.TasksAPI
{
    public static class AuthServiceCollectionExtention
    {
        public static void RegisterServices
                (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ProjectHiveAuthDbContext>(opt => opt.UseNpgsql(connectionString));
        }
    }
}
