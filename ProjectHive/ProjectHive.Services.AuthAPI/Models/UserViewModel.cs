using ProjectHive.Services.AuthAPI.Models;

namespace ProjectHive.Services.AuthAPI.Model
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
