using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using System.Reflection;

namespace ProjectHive.Services.ProjectsAPI
{
    public static class TaskServiceCollectionExtention
    {
        public static void RegisterServicesforProjectApi
                (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ProjectHiveProjectDbContext>(opt => opt.UseNpgsql(connectionString));

            services.AddAutoMapper(typeof(Mapping).GetTypeInfo().Assembly);

            services.AddScoped<IUnitOfWork<Project>, UnitOfWork<Project>>();
            services.AddScoped<IRepository<Project>, Repository<Project>>();
            services.AddScoped<IRepository<ProjectTask>, Repository<ProjectTask>>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
