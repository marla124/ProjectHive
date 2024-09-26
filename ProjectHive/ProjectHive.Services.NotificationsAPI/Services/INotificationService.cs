using ProjectHive.Services.NotificationsAPI.Data.CQS.Commands;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Queries;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Services
{
    public interface INotificationService
    {
        public Task AddNotification(NotificationDto dto, CancellationToken cancellationToken);
        public Task<NotificationDto?> GetNotificationById(Guid id, CancellationToken cancellationToken);
        public Task<List<NotificationDto?>> GetNotifications(Guid userId, CancellationToken cancellationToken);
        public Task<int> GetNotificationCount(Guid userId, CancellationToken cancellationToken);
    }
}
