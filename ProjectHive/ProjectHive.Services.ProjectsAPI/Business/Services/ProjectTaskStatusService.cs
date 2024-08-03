using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.Core.Data.Repository;
using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectTaskStatusService : Service<ProjectTaskStatusDto, StatusTasks, ProjectHiveProjectDbContext>, IProjectTaskStatusService
    {
        public ProjectTaskStatusService(IProjectTaskStatusRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
