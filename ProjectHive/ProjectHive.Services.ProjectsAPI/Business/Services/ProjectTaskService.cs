using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public ProjectTaskService(IUserService userService, IProjectTaskRepository projectTaskRepository, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, IProjectRepository projectRepository) : base(projectTaskRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _configuration = configuration;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectTaskDto> CreateTask(ProjectTaskDto dto, Guid userId, CancellationToken cancellationToken)
        {
                var task = new ProjectTask()
                {
                    Deadline = dto.Deadline,
                    Description = dto.Description,
                    Name = dto.Name,
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    ProjectId = dto.ProjectId,
                    UserCreatorId = dto.UserCreatorId,
                    StatusTaskId = dto.StatusTaskId,
                    UserExecutorId = dto.UserExecutorId
                };
                var createdTask = _mapper.Map<ProjectTaskDto>(await _unitOfWork.ProjectTaskRepository.CreateOne(task, cancellationToken));
                await _unitOfWork.Commit(cancellationToken);
                return createdTask;
        }

        public async Task<ProjectTaskDto[]> GetProjectTasksForUser(Guid userId, CancellationToken cancellationToken)
        {
            var tasks = await GetMany(cancellationToken);
            if (tasks == null)
            {
                return Array.Empty<ProjectTaskDto>();
            }
            return tasks.Where(dto => dto.UserExecutorId == userId)
                        .Select(dto => _mapper.Map<ProjectTaskDto>(dto))
                        .ToArray();
        }
    }
}
