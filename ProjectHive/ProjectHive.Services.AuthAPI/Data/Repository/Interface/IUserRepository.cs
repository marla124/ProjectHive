using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.Core.Data.Repository;
using System.Linq.Expressions;

namespace ProjectHive.Services.AuthAPI.Data.Repository.Interface;

public interface IUserRepository : IRepository<User, ProjectHiveAuthDbContext>
{
    public Task<User> AddFriendlyUser(Guid friendlyUserId, Guid userId, CancellationToken cancellationToken);
    public Task<User> GetByEmail(string email, CancellationToken cancellationToken,
            params Expression<Func<User, object>>[] includes);

    public Task<User> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
    public Task<List<User>> GetFriendlyUsers(Guid userId, CancellationToken cancellationToken);

}
