using ProjectHive.Services.Core.Data.Repository;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;

namespace ProjectHive.Services.ProjectsAPI.Data.Repository;

public class UserRepository : Repository<User, ProjectHiveProjectDbContext>, IUserRepository
{
    public UserRepository(ProjectHiveProjectDbContext dBContext) : base(dBContext)
    {

    }
}
