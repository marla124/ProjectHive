using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.Core.Data.Repository;
using System.Linq.Expressions;

namespace ProjectHive.Services.AuthAPI.Data.Repository;

public class UserRepository : Repository<User, ProjectHiveAuthDbContext>, IUserRepository
{
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
        return await resultQuery.FirstOrDefaultAsync(entity => entity.Email.Equals(email));
    }
    public UserRepository(ProjectHiveAuthDbContext dBContext) : base(dBContext)
    {

    }

}
