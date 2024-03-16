using ProjectHive.Services.ProjectsAPI.Data.Entities;
using System.Linq.Expressions;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CreateOne(T entity);
        Task CreateMany(IEnumerable<T> entities);
        Task<T> GetById(Guid id,
                    params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsNoTracking(Guid id);
        Task Update(T entity);
        Task DeleteById(Guid id);
        Task DeleteMany(IEnumerable<T> entities);
    }

}
