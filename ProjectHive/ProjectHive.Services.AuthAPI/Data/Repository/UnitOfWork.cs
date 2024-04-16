using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.AuthAPI.Data.Repository.Interface;

public class UnitOfWork(ProjectHiveAuthDbContext dbContext,
    IRepository<User, ProjectHiveAuthDbContext> userRepository,
    IRepository<UserRole, ProjectHiveAuthDbContext> userRoleRepository) : IUnitOfWork
{

    public IRepository<User, ProjectHiveAuthDbContext> UserRepository => userRepository;

    public IRepository<UserRole, ProjectHiveAuthDbContext> UserRoleRepository => userRoleRepository;

    public async Task<int> Commit()
    {
        return await dbContext.SaveChangesAsync();
    }
}
