using ProjectHive.Services.Core.Data;

namespace ProjectHive.Services.AuthAPI.Data.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Guid UserRoleId { get; set; }
    public UserRole UserRole { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public List<UserFriend>? Friends { get; set; }
}
