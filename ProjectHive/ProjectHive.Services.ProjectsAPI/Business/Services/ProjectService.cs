using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services;

public class ProjectService : Service<ProjectDto, Project, ProjectHiveProjectDbContext>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    public ProjectService(IMapper mapper, IUnitOfWork unitOfWork, IProjectRepository projectRepository, IUserService userService) : base(projectRepository, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _projectRepository = projectRepository;
        _userService = userService;
    }
        public async Task<ProjectDto> CreateProject(ProjectDto dto, CancellationToken cancellationToken)
        {
            var user = await _userService.CreateUser(dto, cancellationToken);
            var status = await _unitOfWork.ProjectStatusRepository.FindBy(s => s.Name == "Processing").FirstOrDefaultAsync(cancellationToken);
            if (status != null)
            {
                var project = new Project()
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    CreatedAt = DateTime.UtcNow,
                    Description = dto.Description,
                    StatusProjectId = status.Id,
                    UpdatedAt = DateTime.UtcNow,
                    CreatorUserId = user.Id,
                };
                project.UserProjects = new List<UserProject>();
                project.UserProjects.Add(new UserProject { UserId = user.Id });
                var createdProject = _mapper.Map<ProjectDto>(await _unitOfWork.ProjectRepository.CreateOne(project, cancellationToken));
                await _unitOfWork.Commit(cancellationToken);
                return createdProject;
            }
            else
            {
                throw new Exception("statusId not found");
            }
        }

    public async Task<IEnumerable<ProjectDto>> GetProjectsForUser(Guid userId, CancellationToken cancellationToken)
    {
        var projects = await _unitOfWork.ProjectRepository.GetProjectsForUser(userId, cancellationToken);
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

}