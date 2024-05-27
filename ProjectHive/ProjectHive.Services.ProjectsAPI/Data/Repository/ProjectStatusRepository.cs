using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class ProjectStatusRepository : Repository<ProjectStatus, ProjectHiveProjectDbContext>, IProjectStatusRepository
    {
        public ProjectStatusRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
        {

        }
    }
}
