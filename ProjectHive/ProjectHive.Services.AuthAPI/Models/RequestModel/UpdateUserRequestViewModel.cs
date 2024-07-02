namespace ProjectHive.Services.AuthAPI.Models.RequestModel
{
    public class UpdateUserRequestViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserRoleId { get; set; }
    }
}
