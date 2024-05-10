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
    public async Task<int> CreateProject(ProjectDto dto, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            await _unitOfWork.ProjectRepository.CreateOne(project, cancellationToken);

            return await _unitOfWork.Commit(cancellationToken);
        }
        else
        {
            throw new Exception("statusId not found");
        }
    }
}