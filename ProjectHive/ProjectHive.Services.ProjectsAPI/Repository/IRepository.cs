using ProjectHive.Services.ProjectsAPI.Data.Entities;
using System.Linq.Expressions;

namespace ProjectHive.Services.ProjectsAPI.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task CreateOne(T entity);
        public Task CreateMany(IEnumerable<T> entities);
        public Task<T> GetById(Guid id,
                    params Expression<Func<T, object>>[] includes);
        public Task<T> GetByIdAsNoTracking(Guid id);
        public Task Update(T entity);
        public Task DeleteById(Guid id);
        public Task DeleteMany(IEnumerable<T> entities);
    }

}
