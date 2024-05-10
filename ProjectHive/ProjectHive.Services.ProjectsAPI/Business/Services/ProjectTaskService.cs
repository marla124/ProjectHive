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
        private readonly IMapper _mapper;
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, IProjectRepository projectRepository) : base(projectTaskRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _configuration = configuration;
            _projectRepository = projectRepository;
        }

        public async Task<int> CreateTask(ProjectTaskDto dto, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindBy(p => p.Name == dto.ProjectName).FirstOrDefaultAsync(cancellationToken);
            var status = await _unitOfWork.StatusTaskRepository.FindBy(s => s.Name == "Open").FirstOrDefaultAsync(cancellationToken);
            if (status != null)
            {
                var task = new ProjectTask()
                {
                    Deadline = dto.Deadline,
                    Description = dto.Description,
                    Name = dto.Name,
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    ProjectId = project.Id,
                    StatusTaskId = status.Id,
                };
                await _unitOfWork.ProjectTaskRepository.CreateOne(task, cancellationToken);

                return await _unitOfWork.Commit(cancellationToken);
            }
            else
            {
                throw new Exception("statusId not found");
            }

        }
    }
}
