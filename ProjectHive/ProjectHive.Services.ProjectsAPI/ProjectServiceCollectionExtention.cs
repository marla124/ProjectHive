using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data;

namespace ProjectHive.Services.TasksAPI
{
    public static class TaskServiceCollectionExtention
    {
        public static void RegisterServices
                (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ProjectHiveProjectDbContext>(opt => opt.UseNpgsql(connectionString));
        }
    }
}
