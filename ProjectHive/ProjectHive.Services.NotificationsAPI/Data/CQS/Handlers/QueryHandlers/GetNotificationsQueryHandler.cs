using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Queries;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Handlers.QueryHandlers
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
    {
        private readonly ProjectHiveNotificationsDbContext _context;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(ProjectHiveNotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications= _mapper.Map<List<NotificationDto>>(await _context.Notifications.AsQueryable().Where(n => n.UserId == request.UserId)
                .ToListAsync(cancellationToken));
            return notifications;
        }   
    }
}
