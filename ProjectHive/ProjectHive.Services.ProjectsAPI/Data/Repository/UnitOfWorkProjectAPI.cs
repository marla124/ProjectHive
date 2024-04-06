using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository;

public class UnitOfWorkProject(ProjectHiveProjectDbContext dbContext,
    IRepository<Project, ProjectHiveProjectDbContext> projectRepository,
    IRepository<ProjectTask, ProjectHiveProjectDbContext> projectTaskRepository) : IUnitOfWorkProject
{

    public IRepository<Project, ProjectHiveProjectDbContext> ProjectRepository => projectRepository;

    public IRepository<ProjectTask, ProjectHiveProjectDbContext> ProjectTaskRepository => projectTaskRepository;

    public async Task<int> Commit()
    {
        return await dbContext.SaveChangesAsync();
    }
}
