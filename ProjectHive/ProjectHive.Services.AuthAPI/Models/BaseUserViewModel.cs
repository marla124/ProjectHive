namespace ProjectHive.Services.AuthAPI.Models
{
    public class BaseUserViewModel
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
