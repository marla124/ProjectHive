using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IProjectService : IService<ProjectDto>
    {
        public Task<ProjectDto> CreateProject(ProjectDto project, CancellationToken cancellationToken);
        public Task<IEnumerable<ProjectDto>> GetProjectsForUser(Guid userId, CancellationToken cancellationToken);
    }
}
