using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.AuthAPI.Data.Repository.Interface;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRepository<UserRole, ProjectHiveAuthDbContext> UserRoleRepository { get; }
    public IAuthRepository AuthRepository { get; }

    Task<int> Commit();
}
