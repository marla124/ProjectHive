
namespace ProjectHive.Services.AuthAPI.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid UserStatusId { get; set; }
        public UserRole UserStatus { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
