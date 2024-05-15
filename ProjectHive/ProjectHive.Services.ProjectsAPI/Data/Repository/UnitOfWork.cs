using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository;

public class UnitOfWork(ProjectHiveProjectDbContext dbContext,
    IRepository<Project, ProjectHiveProjectDbContext> projectRepository,
    IRepository<StatusTasks, ProjectHiveProjectDbContext> statusTaskRepository,
    IRepository<ProjectTask, ProjectHiveProjectDbContext> projectTaskRepository,
    IRepository<ProjectStatus, ProjectHiveProjectDbContext> projectStatusRepository,
    IRepository<User, ProjectHiveProjectDbContext> userRepository) : IUnitOfWork
{

    public IRepository<Project, ProjectHiveProjectDbContext> ProjectRepository => projectRepository;

    public IRepository<ProjectTask, ProjectHiveProjectDbContext> ProjectTaskRepository => projectTaskRepository;
    public IRepository<StatusTasks, ProjectHiveProjectDbContext> StatusTaskRepository => statusTaskRepository;
    public IRepository<ProjectStatus, ProjectHiveProjectDbContext> ProjectStatusRepository => projectStatusRepository;
    public IRepository<User, ProjectHiveProjectDbContext> UserRepository => userRepository;

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}