using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Data;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;
using System.Security.Claims;

namespace ProjectHive.Services.ProjectsAPI.Business.Services;

public class ProjectService : Service<ProjectDto, Project, ProjectHiveProjectDbContext>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ProjectService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor) : base(projectRepository, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _projectRepository = projectRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ProjectDto> CreateProject(ProjectDto dto, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue("userId");
        var user = await _unitOfWork.UserRepository.FindBy(u => u.Id == Guid.Parse(userId)).FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            user = new User { Id = Guid.Parse(userId) };
            await _unitOfWork.UserRepository.CreateOne(user, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
        }
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
                CreatorUserId = Guid.Parse(userId)
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