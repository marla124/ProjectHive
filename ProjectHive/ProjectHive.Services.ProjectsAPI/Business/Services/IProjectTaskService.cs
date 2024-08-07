using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IProjectTaskService : IService<ProjectTaskDto>
    {
        Task<ProjectTaskDto> CreateTask(ProjectTaskDto dto, Guid userId, CancellationToken cancellationToken);
    }
}
