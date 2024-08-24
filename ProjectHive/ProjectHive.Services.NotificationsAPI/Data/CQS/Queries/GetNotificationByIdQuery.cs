using MediatR;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Queries
{
    public class GetNotificationByIdQuery : IRequest<NotificationDto>
    {
        public Guid Id { get; set; }
    }
}
