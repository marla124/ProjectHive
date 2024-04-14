using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.Core.Data.Repository;

namespace ProjectHive.Services.AuthAPI.Data.Repository;

public class UserRepository : Repository<User, ProjectHiveAuthDbContext>, IUserRepository
{
    public UserRepository(ProjectHiveAuthDbContext dBContext) : base(dBContext)
    {

    }
}
