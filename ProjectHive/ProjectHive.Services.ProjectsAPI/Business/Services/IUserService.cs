using ProjectHive.Services.Core.Business;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services;

public interface IUserService : IService<UserDto>
{
    public Task<UserDto> CreateUser(CancellationToken cancellationToken);
}
