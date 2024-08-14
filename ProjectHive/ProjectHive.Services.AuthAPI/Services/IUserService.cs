using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.Core.Business;

namespace ProjectHive.Services.AuthAPI.Services
{
    public interface IUserService : IService<UserDto>
    {
        Task AddFriendlyUser(Guid friendlyUserId, Guid userId, CancellationToken cancellationToken);
        Task<bool> CheckPasswordCorrect(string email, string password, CancellationToken cancellationToken);
        Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken);
        Task<IEnumerable<UserDto>> GetFriendlyUsers(Guid userId, CancellationToken cancellationToken);
        Task<UserDto> GetUserByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
        Task<UserDto> RegisterUser(UserDto dto, CancellationToken cancellationToken);
    }
}
