using ProjectHive.Services.ProjectsAPI.Data.Entities;
using System.Linq.Expressions;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateOne(T entity, CancellationToken cancellationToken);
        Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task<T> GetById(Guid id, CancellationToken cancellationToken,
                    params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsNoTracking(Guid id, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task DeleteById(Guid id, CancellationToken cancellationToken);
        Task DeleteMany(IEnumerable<T> entities, CancellationToken cancellationToken);
    }

}
