using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository;

public class ProjectRepository : Repository<Project, ProjectHiveProjectDbContext>, IProjectRepository
{
    private readonly ProjectHiveProjectDbContext _dBContext;
    public ProjectRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
    {
        _dBContext = dBContext;
    }

    public async Task<IEnumerable<Project>> GetProjectsForUser(Guid userId, CancellationToken cancellationToken)
    {
        var userProjects = await _dBContext.UserProjects
            .Where(up => up.UserId == userId)
            .Select(up => up.Project)
            .ToListAsync(cancellationToken);
        return userProjects;
    }
}