namespace ProjectHive.Services.AuthAPI.Data.Entities
{
    public class UserFriend
    {
        public Guid UserAId { get; set; }
        public User UserA { get; set; }
        public Guid UserBId { get; set; }
        public User UserB { get; set; }
        public DateTime DateStartFriendship { get; set; }
    }
}
