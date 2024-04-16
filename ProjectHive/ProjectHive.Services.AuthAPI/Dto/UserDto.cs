namespace ProjectHive.Services.AuthAPI.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
