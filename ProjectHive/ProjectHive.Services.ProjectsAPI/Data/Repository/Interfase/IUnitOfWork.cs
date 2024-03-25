using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        public IRepository<Project> ProjectRepository { get; }
        public IRepository<ProjectTask> ProjectTaskRepository { get; }
        public IRepository<TEntity> Repository { get; }
        Task<int> Commit();
    }
}
