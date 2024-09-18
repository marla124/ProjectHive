using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IProjectTaskStatusService : IService<ProjectTaskStatusDto>
    {
        Task<ProjectTaskStatusDto[]> GetStatusProjectTasks(CancellationToken cancellationToken);
    }
}
