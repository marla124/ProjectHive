using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.AuthAPI.Data.Repository.Interface;

public interface IUserRepository : IRepository<User, ProjectHiveAuthDbContext>
{
}
