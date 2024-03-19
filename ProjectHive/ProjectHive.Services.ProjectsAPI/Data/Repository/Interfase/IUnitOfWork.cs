using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IUnitOfWork
    {
        public IRepository<Project> ProjectRepository { get; }
        public IRepository<ProjectTask> ProjectTaskRepository { get; }

        Task<int> Commit();
    }
}
