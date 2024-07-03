using System.Linq.Expressions;

namespace ProjectHive.Services.Core.Data.Repository;
public interface IRepository<TEntity, TDbContext> where TEntity : BaseEntity
{
    Task<TEntity> CreateOne(TEntity entity, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken,
                params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetByIdAsNoTracking(Guid id, CancellationToken cancellationToken);
    public IQueryable<TEntity> GetAsQueryable();
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
    Task DeleteById(Guid id, CancellationToken cancellationToken);
    Task DeleteMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task Commit(CancellationToken cancellationToken);
    public Task<List<TEntity>> FindBy(CancellationToken cancellationToken);
    public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression,
        params Expression<Func<TEntity, object>>[] includes);
}
