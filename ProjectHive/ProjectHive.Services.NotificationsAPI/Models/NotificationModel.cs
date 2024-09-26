namespace ProjectHive.Services.NotificationsAPI.Models
{
    public class NotificationModel
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public Guid NotificationStatusId { get; set; }
    }
}
