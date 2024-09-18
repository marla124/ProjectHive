using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Dto;
using AutoMapper;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Data.Repository;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectStatusService : Service<ProjectStatusDto, ProjectStatus, ProjectHiveProjectDbContext>, IProjectStatusService
    {
        private readonly IMapper _mapper;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectStatusService(IProjectStatusRepository repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper)
        {
            _projectStatusRepository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectStatusDto[]> GetProjectStatuses(CancellationToken cancellationToken)
        {
            var projectStatuses = await GetMany(cancellationToken);
            if (projectStatuses == null)
            {
                return Array.Empty<ProjectStatusDto>();
            }
            return projectStatuses.Select(dto => _mapper.Map<ProjectStatusDto>(dto)).ToArray();
        }
    }
}
