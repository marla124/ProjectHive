using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.TasksAPI
{
    public static class TaskServiceCollectionExtention
    {
        public static void RegisterServicesforProjectApi
                (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ProjectHiveProjectDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

        }
    }
}
