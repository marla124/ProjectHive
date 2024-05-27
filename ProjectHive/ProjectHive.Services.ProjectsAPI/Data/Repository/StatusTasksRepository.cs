using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class StatusTasksRepository : Repository<StatusTasks, ProjectHiveProjectDbContext>, IStatusTasksRepository
    {
        public StatusTasksRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
        {

        }
    }
}
