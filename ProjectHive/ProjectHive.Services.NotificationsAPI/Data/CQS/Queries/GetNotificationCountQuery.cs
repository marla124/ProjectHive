using MediatR;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Queries
{
    public class GetNotificationCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}
