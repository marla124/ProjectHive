using ProjectHive.Services.ProjectsAPI.Data.Entities;
using System.Linq.Expressions;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateOne(TEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        Task<TEntity> GetById(Guid id, CancellationToken cancellationToken,
                    params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsNoTracking(Guid id, CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
        Task DeleteById(Guid id, CancellationToken cancellationToken);
        Task DeleteMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    }

}
