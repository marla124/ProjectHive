using ProjectHive.Services.Core.Data;

namespace ProjectHive.Services.NotificationsAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public List<Notification>? Notifications { get; set; }
    }
}
