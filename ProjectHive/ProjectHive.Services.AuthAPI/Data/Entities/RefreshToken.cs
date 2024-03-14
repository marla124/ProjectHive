namespace ProjectHive.Services.AuthAPI.Data.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiringAt { get; set; }
        public string AssociateDeviceName { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
