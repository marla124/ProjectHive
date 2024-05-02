using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;

namespace ProjectHive.Services.AuthAPI.Data.Repository;

public class AuthRepository(ProjectHiveAuthDbContext dBContext) : IAuthRepository
{
    public async Task CreateRefreshToken(RefreshToken refToken, CancellationToken cancellationToken)
    {
        await dBContext.RefreshTokens.AddAsync(refToken, cancellationToken);
        await dBContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRefreshToken(Guid id, CancellationToken cancellationToken)
    {
        var delEntity = await GetRefreshToken(id, cancellationToken: cancellationToken);
        if (delEntity != null)
        {
            dBContext.RefreshTokens.Remove(delEntity);
        }
        else
        {
            throw new ArgumentException("Incorrect id for delete", nameof(id));
        }
    }

    public async Task<RefreshToken> GetRefreshToken(Guid id, CancellationToken cancellationToken)
    {
        return await dBContext.RefreshTokens.FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException();
    }
}
