using Microsoft.EntityFrameworkCore;
using ProjectHive.Service.Core.Data;
using System.Linq.Expressions;

namespace ProjectHive.Services.Core.Data.Repository
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity, TDbContext> where TEntity : BaseEntity where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public async Task<TEntity> CreateOne(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteById(Guid id, CancellationToken cancellationToken)
        {
            var delEntity = await GetById(id, cancellationToken: cancellationToken);
            if (delEntity != null)
            {
                _dbSet.Remove(delEntity);
            }
            else
            {
                throw new ArgumentException("Incorrect id for delete", nameof(id));
            }
        }

        public async Task DeleteMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            if (entities.Any())
            {
                var deleteEntities = entities.Where(entity => _dbSet.Any(dbEn => dbEn.Id.Equals(entity.Id))).ToList();
                _dbSet.RemoveRange(entities);
            }
        }

        public async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken,
                    params Expression<Func<TEntity, object>>[] includes)
        {
            var resultQuery = _dbSet.AsQueryable();
            if (includes.Any())
            {
                resultQuery = includes.Aggregate(resultQuery,
                    (current, include)
                        => current.Include(include));
            }
            return await resultQuery.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async Task<TEntity> GetByIdAsNoTracking(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(entities => entities.Id.Equals(id));
        }


        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Update(entity);
            return entity;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
