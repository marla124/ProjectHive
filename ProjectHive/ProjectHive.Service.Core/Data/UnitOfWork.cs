using Microsoft.EntityFrameworkCore;
using ProjectHive.Service.Core.Data;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository
{
    public class UnitOfWork<TEntity, TDbContext> where TEntity : BaseEntity where TDbContext : DbContext
    {
        private readonly IRepository<TEntity> _repository;
        private readonly TDbContext _dbContext;
        public UnitOfWork(TDbContext dbContext, IRepository<TEntity> repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }
        public IRepository<TEntity> Repository => _repository;
        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
