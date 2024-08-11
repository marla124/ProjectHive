using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.Core.Data.Repository;
using System.Linq.Expressions;

namespace ProjectHive.Services.AuthAPI.Data.Repository;

public class UserRepository : Repository<User, ProjectHiveAuthDbContext>, IUserRepository
{
    public UserRepository(ProjectHiveAuthDbContext dBContext) : base(dBContext)
    {

    }
    public async Task<User> GetByEmail(string email, CancellationToken cancellationToken,
                params Expression<Func<User, object>>[] includes)
    {
        var resultQuery = _dbSet.AsQueryable();
        if (includes.Any())
        {
            resultQuery = includes.Aggregate(resultQuery,
                (current, include)
                    => current.Include(include));
        }
        return await resultQuery.FirstOrDefaultAsync(entity => entity.Email.Equals(email), cancellationToken);
    }

    public async Task<User> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken)
    {
        var resultQuery = _dbSet.AsQueryable();
        return await resultQuery.FirstOrDefaultAsync(entity => entity.RefreshTokens.Any(rt => rt.Id == refreshToken), cancellationToken);
    }

    public async Task<List<User>> GetFriendlyUsers(Guid userId, CancellationToken cancellationToken)
    {
        var friends = await _dbSet
            .Where(u => u.Friends.Any(f => f.UserOneId == userId || f.UserTwoId == userId))
            .SelectMany(u => u.Friends)
            .Where(f => f.UserOneId == userId || f.UserTwoId == userId)
            .Select(f => f.UserOneId == userId ? f.UserTwo : f.UserOne)
            .ToListAsync(cancellationToken);

        return friends;
    }
}
