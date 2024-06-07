using ProjectHive.Services.Core.Data;

namespace ProjectHive.Services.NotificationsAPI.Data.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }  
        public Guid NotificationStatusId { get; set; }
        public NotificationStatus NotificationStatus { get; set; }

    }
}
