using ProjectHive.Services.AuthAPI.Dto;

namespace ProjectHive.Services.AuthAPI.Services
{
    public interface ITokenService
    {
        Task<Guid> AddRefreshToken(string email, string userAgent, Guid userId, CancellationToken cancellationToken);
        public Task<string> GenerateJwtToken(UserDto userDto, CancellationToken cancellationToken);
        Task<bool> CheckRefreshToken(Guid requestRefreshToken, CancellationToken cancellationToken);
        Task RemoveRefreshToken(Guid requestRefreshToken, CancellationToken cancellationToken);
    }
}
