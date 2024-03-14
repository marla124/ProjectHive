namespace ProjectHive.Services.AuthAPI.Data.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public List<User> Users { get; set; }
    }
}
