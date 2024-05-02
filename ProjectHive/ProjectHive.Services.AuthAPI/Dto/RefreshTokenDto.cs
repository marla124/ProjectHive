namespace ProjectHive.Services.AuthAPI.Dto
{
    public class RefreshTokenDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiringAt { get; set; }
        public string AssociateDeviceName { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
