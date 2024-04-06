using ProjectHive.Service.Core.Data;
using ProjectHive.Service.Core.Data.Repository;
using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IUnitOfWorkProject<TEntity> where TEntity : BaseEntity, IUnitOfWork<TEntity>
    {
        public IRepository<Project> ProjectRepository { get; }
    }
}
