using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.NotificationsAPI.Handlers;

namespace ProjectHive.Services.NotificationsAPI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly WebSocketHandler _webSocketHandler;
        public NotificationController()
        {
            _webSocketHandler = new WebSocketHandler();
        }
        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
