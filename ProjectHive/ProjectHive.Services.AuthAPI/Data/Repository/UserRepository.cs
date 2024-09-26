using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.Core.Data.Repository;
using System.Linq.Expressions;

namespace ProjectHive.Services.AuthAPI.Data.Repository;

public class UserRepository : Repository<User, ProjectHiveAuthDbContext>, IUserRepository
{
    private readonly ProjectHiveAuthDbContext _dbContext;
    public UserRepository(ProjectHiveAuthDbContext dbContext) : base(dbContext)
    {
        _dbContext= dbContext;
    }

    public async Task<User> AddFriendlyUser(Guid friendlyUserId, Guid userId, CancellationToken cancellationToken)
    {
        var userA = await _dbSet.FindAsync(userId, cancellationToken);
        var userB = await _dbSet.FindAsync(friendlyUserId, cancellationToken);

        if (userA == null || userB == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var friendship = new UserFriend
        {
            UserAId = userA.Id,
            UserA = userA,
            UserBId = userB.Id,
            UserB = userB
        };

        _dbContext.Friends.Add(friendship);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return userA;

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
            .Where(u => u.Friends.Any(f => f.UserAId == userId || f.UserBId == userId))
            .SelectMany(u => u.Friends)
            .Where(f => f.UserAId == userId || f.UserBId == userId)
            .Select(f => f.UserAId == userId ? f.UserB : f.UserA)
            .ToListAsync(cancellationToken);

        return friends ?? new List<User>();
    }
}
