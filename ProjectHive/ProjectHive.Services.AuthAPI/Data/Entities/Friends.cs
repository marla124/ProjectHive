namespace ProjectHive.Services.AuthAPI.Data.Entities
{
    public class Friends
    {
        public Guid UserOneId { get; set; }
        public User UserOne { get; set; }
        public Guid UserTwoId { get; set; }
        public User UserTwo { get; set; }
    }
}
