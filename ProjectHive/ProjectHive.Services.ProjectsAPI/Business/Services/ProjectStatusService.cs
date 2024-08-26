using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Dto;
using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectStatusService : Service<ProjectStatusDto, ProjectStatus, ProjectHiveProjectDbContext>, IProjectStatusService
    {
        public ProjectStatusService(IProjectStatusRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
