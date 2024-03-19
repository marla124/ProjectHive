using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
    }
}
