using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class ProjectTaskRepository : Repository<ProjectTask, ProjectHiveProjectDbContext>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
        {

        }
    }
}
