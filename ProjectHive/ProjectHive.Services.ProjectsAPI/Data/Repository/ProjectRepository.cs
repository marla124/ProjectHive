using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository;

public class ProjectRepository : Repository<Project, ProjectHiveProjectDbContext>, IProjectRepository
{
    private readonly ProjectHiveProjectDbContext _dbContext;
    public ProjectRepository(ProjectHiveProjectDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetProjectsForUser(Guid userId, CancellationToken cancellationToken)
    {
        var userProjects = await _dbContext.UserProjects
            .Where(up => up.UserId == userId)
            .Select(up => up.Project)
            .ToListAsync(cancellationToken);
        return userProjects;
    }
}