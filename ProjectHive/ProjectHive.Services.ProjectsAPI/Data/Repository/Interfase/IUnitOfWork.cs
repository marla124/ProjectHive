using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IUnitOfWork
    {
        public IRepository<Project, ProjectHiveProjectDbContext> ProjectRepository { get; }
        public IRepository<ProjectTask, ProjectHiveProjectDbContext> ProjectTaskRepository { get; }

        Task<int> Commit();
    }
}