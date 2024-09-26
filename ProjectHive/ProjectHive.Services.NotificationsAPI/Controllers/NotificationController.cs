using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectHive.Services.NotificationsAPI.Dto;
using ProjectHive.Services.NotificationsAPI.Hubs;
using ProjectHive.Services.NotificationsAPI.Models;
using ProjectHive.Services.NotificationsAPI.Services;


namespace ProjectHive.Services.NotificationsAPI.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationController(IMapper mapper, INotificationService notificationService, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddNotification(NotificationModel request, CancellationToken cancellationToken)
        {
            var dto= _mapper.Map<NotificationDto>(request);
            await _notificationService.AddNotification(dto, cancellationToken);
            var message = $"New notification: {dto.Title}";
            await _hubContext.Clients.All.SendAsync(message);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<NotificationModel>(await _notificationService.GetNotificationById(id, cancellationToken));
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(GetUserId());
            var notifications = _mapper.Map<NotificationModel>(await _notificationService.GetNotifications(userId, cancellationToken));
            if (notifications == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<NotificationModel>>(notifications));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotificationCount(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(GetUserId());
            var count = await _notificationService.GetNotificationCount(userId, cancellationToken);
            return Ok(count);
        }
    }
}
