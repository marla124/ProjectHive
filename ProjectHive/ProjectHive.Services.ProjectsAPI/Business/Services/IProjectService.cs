
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IProjectService
    {
        public Task DeleteManyProject(IEnumerable<ProjectDto> dtos);
        public Task CreateManyProject(IEnumerable<ProjectDto> dtos);
        public Task CreateProject(ProjectDto dto);
        public Task DeleteProjectById(Guid id);
        public Task<ProjectDto?> GetProjectById(Guid Id);
        public Task UpdateProject(ProjectDto dto);
    }
}
