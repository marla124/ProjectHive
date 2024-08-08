using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.NotificationsAPI.Dto;
using ProjectHive.Services.NotificationsAPI.Models;
using ProjectHive.Services.NotificationsAPI.Services;
using System.Net.WebSockets;

namespace ProjectHive.Services.NotificationsAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IWebSocketHandler _webSocketHandler;
        private readonly IMapper _mapper;
        public NotificationController(IMapper mapper)
        {
            _webSocketHandler = new WebSocketHandler();
            _mapper = mapper;
        }
        [HttpPost]
        public async Task SendNotification(NotificationModel notification)
        {
            await _webSocketHandler.SendNotificationAsync(_mapper.Map<NotificationDto>(notification));
            return;
        }
        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
