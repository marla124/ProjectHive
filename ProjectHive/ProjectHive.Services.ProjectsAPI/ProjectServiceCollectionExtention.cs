using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI;

public static class TaskServiceCollectionExtention
{
    public static void RegisterServicesforProjectApi
            (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ProjectHiveProjectDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
        services.AddScoped<IRepository<User, ProjectHiveProjectDbContext>, Repository<User, ProjectHiveProjectDbContext>>();
        services.AddScoped<IRepository<ProjectStatus, ProjectHiveProjectDbContext>, Repository<ProjectStatus, ProjectHiveProjectDbContext>>();
        services.AddScoped<IRepository<Project, ProjectHiveProjectDbContext>, Repository<Project, ProjectHiveProjectDbContext>>();
        services.AddScoped<IRepository<StatusTasks, ProjectHiveProjectDbContext>, Repository<StatusTasks, ProjectHiveProjectDbContext>>();
        services.AddScoped<IRepository<ProjectTask, ProjectHiveProjectDbContext>, Repository<ProjectTask, ProjectHiveProjectDbContext>>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectTaskService, ProjectTaskService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpContextAccessor();

    }
}