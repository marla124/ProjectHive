using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository;

public class ProjectRepository : Repository<Project, ProjectHiveProjectDbContext>, IProjectRepository
{
    public ProjectRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
    {

    }
}