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
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;
    public ProjectService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor, IUserService userService) : base(projectRepository, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _projectRepository = projectRepository;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }
    public async Task<ProjectDto> CreateProject(ProjectDto dto, CancellationToken cancellationToken)
    {
        var user = _userService.CreateUser(cancellationToken);
        var status = await _unitOfWork.ProjectStatusRepository.FindBy(s => s.Name == "Processing").FirstOrDefaultAsync(cancellationToken);
        if (status != null)
        {
            var project = new Project()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                CreatedAt = DateTime.Now,
                Description = dto.Description,
                StatusProjectId = status.Id,
                UpdatedAt = DateTime.Now,
                CreatorUserId = user.Result.Id,
            };
            var createdTask = _mapper.Map<ProjectDto>(await _unitOfWork.ProjectRepository.CreateOne(project, cancellationToken));
            await _unitOfWork.Commit(cancellationToken);
            return createdTask;
        }
        else
        {
            throw new Exception("statusId not found");
        }
    }
}