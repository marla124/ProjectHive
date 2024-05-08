using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IUnitOfWork
    {
        public IRepository<Project, ProjectHiveProjectDbContext> ProjectRepository { get; }
        public IRepository<ProjectTask, ProjectHiveProjectDbContext> ProjectTaskRepository { get; }
        public IRepository<StatusTasks, ProjectHiveProjectDbContext> StatusTaskRepository { get; }

        Task<int> Commit(CancellationToken cancellationToken);
    }
}