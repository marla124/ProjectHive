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
        private readonly IMapper _mapper;
        public ProjectTaskStatusService(IProjectTaskStatusRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }

        public async Task<ProjectTaskStatusDto[]> GetStatusProjectTasks(CancellationToken cancellationToken)
        {
            var taskStatuses = await GetMany(cancellationToken);
            if (taskStatuses == null)
            {
                return Array.Empty<ProjectTaskStatusDto>();
            }
            return taskStatuses.Select(dto => _mapper.Map<ProjectTaskStatusDto>(dto)).ToArray();
        }
    }
}
