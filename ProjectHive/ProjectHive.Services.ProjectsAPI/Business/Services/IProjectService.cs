
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IProjectService
    {
        public Task DeleteManyProject(IEnumerable<ProjectDto> dtos, CancellationToken cancellationToken);
        public Task CreateManyProject(IEnumerable<ProjectDto> dtos, CancellationToken cancellationToken);
        public Task CreateProject(ProjectDto dto, CancellationToken cancellationToken);
        public Task DeleteProjectById(Guid id, CancellationToken cancellationToken);
        public Task<ProjectDto?> GetProjectById(Guid Id, CancellationToken cancellationToken);
        public Task UpdateProject(ProjectDto dto, CancellationToken cancellationToken);
    }
}
