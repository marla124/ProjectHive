using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.Core.Business;

namespace ProjectHive.Services.AuthAPI.Services
{
    public interface IUserService : IService<UserDto>
    {
        Task<bool> CheckPasswordCorrect(string email, string password);
        Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken);
        Task<int> RegisterUser(UserDto dto, CancellationToken cancellationToken);
    }
}
