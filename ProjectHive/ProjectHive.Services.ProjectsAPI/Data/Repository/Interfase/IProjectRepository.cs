﻿using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

public interface IProjectRepository : IRepository<Project, ProjectHiveProjectDbContext>
{
    public Task<IEnumerable<Project>> GetProjectsForUser(Guid userId, CancellationToken cancellationToken);
}