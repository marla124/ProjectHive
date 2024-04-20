using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.AuthAPI.Data.Repository;

public class UnitOfWork(
    ProjectHiveAuthDbContext dbContext,
    IUserRepository userRepository,
    IRepository<UserRole, ProjectHiveAuthDbContext> userRoleRepository,
    IAuthRepository authRepository)
    : IUnitOfWork
{

    public IUserRepository UserRepository { get; } = userRepository;
    public IRepository<UserRole, ProjectHiveAuthDbContext> UserRoleRepository { get; } = userRoleRepository;
    public IAuthRepository AuthRepository { get; } = authRepository;

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
