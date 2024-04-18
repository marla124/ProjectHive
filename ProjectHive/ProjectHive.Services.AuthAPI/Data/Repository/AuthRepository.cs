using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;

namespace ProjectHive.Services.AuthAPI.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ProjectHiveAuthDbContext _dbContext;
        private readonly DbSet<RefreshToken> _dbSet;

        public AuthRepository(ProjectHiveAuthDbContext dBContext, DbSet<RefreshToken> dbSet)
        {
            _dbContext = dBContext;
            _dbSet = dbSet;
        }
        public async Task CreateRefreshToken(RefreshToken refToken, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(refToken);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteRefreshToken(Guid id, CancellationToken cancellationToken)
        {
            var delEntity = await GetRefreshToken(id, cancellationToken: cancellationToken);
            if (delEntity != null)
            {
                _dbSet.Remove(delEntity);
            }
            else
            {
                throw new ArgumentException("Incorrect id for delete", nameof(id));
            }
        }
        public async Task<RefreshToken> GetRefreshToken(Guid id, CancellationToken cancellationToken)
        {
            var resultQuery = _dbSet.AsQueryable();
            return await resultQuery.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }
    }
}
