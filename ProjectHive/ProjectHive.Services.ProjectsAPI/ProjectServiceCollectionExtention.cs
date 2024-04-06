using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI
{
    public static class TaskServiceCollectionExtention
    {
        public static void RegisterServicesforProjectApi
                (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ProjectHiveProjectDbContext>(opt => opt.UseNpgsql(connectionString));
            var mapperConfig = Mapping.RegisterMaps();
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper); //It doesn't work without it
            services.AddScoped<IUnitOfWork<Project>, UnitOfWork<Project>>();
            services.AddScoped<IRepository<Project>, Repository<Project>>();
            services.AddScoped<IRepository<ProjectTask>, Repository<ProjectTask>>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
