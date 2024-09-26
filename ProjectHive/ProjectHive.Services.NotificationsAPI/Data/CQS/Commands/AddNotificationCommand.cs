using MediatR;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Commands
{
    public class AddNotificationCommand : IRequest
    {
        public NotificationDto Notification { get; set; }
    }
}
