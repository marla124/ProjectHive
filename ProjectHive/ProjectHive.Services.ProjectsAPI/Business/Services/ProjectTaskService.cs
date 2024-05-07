using AutoMapper;
using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectTaskService : Service<ProjectTaskDto, ProjectTask, ProjectHiveProjectDbContext>, IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration) : base(projectTaskRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ProjectTaskDto> CreateTask(ProjectTaskDto dto, CancellationToken cancellationToken)
        {
            var task = new ProjectTask()
            {
                Deadline = dto.Deadline,
                Description = dto.Description,
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow,
                Id = dto.Id,
                ProjectId = dto.ProjectId,
                StartExecution = dto.StartExecution,
                StatusTaskId = dto.StatusTaskId,
            };
            await _unitOfWork.ProjectTaskRepository.CreateOne(task, cancellationToken);

            return await _unitOfWork.Commit(cancellationToken);
        }
    }
}
