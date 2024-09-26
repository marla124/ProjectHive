using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Commands;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Queries;
using ProjectHive.Services.NotificationsAPI.Data.Entities;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMediator _mediator;

        public NotificationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddNotification(NotificationDto dto, CancellationToken cancellationToken)
        {
            var command = new AddNotificationCommand() { Notification = dto};
            await _mediator.Send(command);
        }

        public async Task<NotificationDto?> GetNotificationById(Guid id, CancellationToken cancellationToken)
        {
            var notificationDto= await _mediator.Send(new GetNotificationByIdQuery() {  Id = id }); 
            return notificationDto;
        }

        public async Task<List<NotificationDto?>> GetNotifications(Guid userId, CancellationToken cancellationToken)
        {
            var notificationDtos = await _mediator.Send(new GetNotificationsQuery() { UserId = userId });
            return notificationDtos;
        }

        public async Task<int> GetNotificationCount(Guid userId, CancellationToken cancellationToken)
        {
            var notificationCount = await _mediator.Send(new GetNotificationCountQuery() { UserId = userId});
            return notificationCount;
        }
    }
}
