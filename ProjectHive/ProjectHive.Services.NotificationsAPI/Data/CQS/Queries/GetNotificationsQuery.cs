using MediatR;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Queries
{
    public class GetNotificationsQuery : IRequest<List<NotificationDto>>
    {
        public Guid UserId { get; set; }
    }
}
