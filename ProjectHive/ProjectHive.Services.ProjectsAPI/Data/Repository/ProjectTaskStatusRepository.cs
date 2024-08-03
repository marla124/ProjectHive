using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class ProjectTaskStatusRepository : Repository<StatusTasks, ProjectHiveProjectDbContext>, IProjectTaskStatusRepository
    {
        public ProjectTaskStatusRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
        {

        }
    }
}
