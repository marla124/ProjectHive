using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using System.Linq.Expressions;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ProjectHiveProjectDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(ProjectHiveProjectDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task CreateMany(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task CreateOne(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteById(Guid id)
        {
            var delEntity = await GetById(id);
            if (delEntity != null)
            {
                _dbSet.Remove(delEntity);
            }
            else
            {
                throw new ArgumentException("Incorrect id for delete", nameof(id));
            }
        }

        public async Task DeleteMany(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                var deleteEntities = entities.Where(entity => _dbSet.Any(dbEn => dbEn.Id.Equals(entity.Id))).ToList();
                _dbSet.RemoveRange(entities);
            }
        }

        public async Task<T> GetById(Guid id,
                    params Expression<Func<T, object>>[] includes)
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

        public async Task<T> GetByIdAsNoTracking(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(entities => entities.Id.Equals(id));
        }


        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
        }

    }
}
