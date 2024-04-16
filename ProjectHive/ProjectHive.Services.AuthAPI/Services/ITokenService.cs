using ProjectHive.Services.AuthAPI.Dto;

namespace ProjectHive.Services.AuthAPI.Services
{
    public interface ITokenService
    {
        Task<Guid> AddRefreshToken(string email, string v);
        public Task<string> GenerateJwtToken(UserDto userDto);
    }
}
