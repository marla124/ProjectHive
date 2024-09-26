using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Queries;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Handlers.QueryHandlers
{
    public class GetNotificationCountQueryHandler : IRequestHandler<GetNotificationCountQuery, int>
    {
        private readonly ProjectHiveNotificationsDbContext _context;
        private readonly IMapper _mapper;

        public GetNotificationCountQueryHandler(ProjectHiveNotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNotificationCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _context.Notifications
                .Where(n => n.UserId == request.UserId)
                .CountAsync(cancellationToken);
            return count;
        }
    }
}
