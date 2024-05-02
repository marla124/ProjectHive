using ProjectHive.Services.Core.Data;

namespace ProjectHive.Services.AuthAPI.Data.Entities;

public class UserRole : BaseEntity
{
    public string Role { get; set; }
    public List<User> Users { get; set; }
}
