using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Queries;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Handlers.QueryHandlers
{
    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto>
    {
        private readonly ProjectHiveNotificationsDbContext _context;
        private readonly IMapper _mapper;

        public GetNotificationByIdQueryHandler(ProjectHiveNotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<NotificationDto> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id.Equals(request.Id), cancellationToken);
            if (notification == null)
            {
                throw new Exception("Not found Id");
            }
            return _mapper.Map<NotificationDto>(notification);
        }
    }
}
