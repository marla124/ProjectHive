namespace ProjectHive.Services.NotificationsAPI.Dto
{
    public class NotificationDto
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public Guid UserId { get; set; }
        public Guid NotificationStatusId { get; set; }
    }
}
