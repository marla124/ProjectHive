using AutoMapper;
using MediatR;
using ProjectHive.Services.NotificationsAPI.Data.CQS.Commands;
using ProjectHive.Services.NotificationsAPI.Data.Entities;
using ProjectHive.Services.NotificationsAPI.Dto;

namespace ProjectHive.Services.NotificationsAPI.Data.CQS.Handlers.CommandHandlers;

public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand>
{
    private readonly ProjectHiveNotificationsDbContext _context;
    private readonly IMapper _mapper;

    public AddNotificationCommandHandler(ProjectHiveNotificationsDbContext context, IMapper mapper) 
    {  
        _context = context; 
        _mapper = mapper;
    }

    public async Task Handle(AddNotificationCommand command, CancellationToken cancellationToken)
    {
        var notification = _mapper.Map<Notification>(command.Notification);
        await _context.Notifications.AddAsync(notification, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
