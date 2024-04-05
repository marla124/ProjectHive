using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Service.Core.Data.Repository
{
    public interface IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        public IRepository<TEntity> Repository { get; }
        Task<int> Commit();
    }
}
