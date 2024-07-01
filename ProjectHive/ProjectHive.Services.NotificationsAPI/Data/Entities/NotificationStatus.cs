using ProjectHive.Services.Core.Data;

namespace ProjectHive.Services.NotificationsAPI.Data.Entities
{
    public class NotificationStatus : BaseEntity
    {
        public string Name { get; set; }
        public List<Notification>? Notifications { get; set; }
    }
}
