using ProjectHive.Services.AuthAPI.Data.Entities;

namespace ProjectHive.Services.AuthAPI.Data.Repository.Interface
{
    public interface IAuthRepository
    {
        public Task CreateRefreshToken(RefreshToken refToken, CancellationToken cancellationToken);
        public Task DeleteRefreshToken(Guid id, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshToken(Guid id, CancellationToken cancellationToken);
    }
}
