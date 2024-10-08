﻿using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public interface IProjectStatusService : IService<ProjectStatusDto>
    {
        Task<ProjectStatusDto[]> GetProjectStatuses(CancellationToken cancellationToken);
    }
}
