using Newtonsoft.Json;
using ProjectHive.Services.NotificationsAPI.Dto;
using ProjectHive.Services.NotificationsAPI.Models;
using System.Net.WebSockets;
using System.Text;

namespace ProjectHive.Services.NotificationsAPI.Services
{
    public class WebSocketHandler :IWebSocketHandler
    {
        private readonly List<WebSocket> _sockets = new List<WebSocket>();

        public async Task SendNotificationAsync(NotificationDto notification)
        {
            var message = JsonConvert.SerializeObject(notification);
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            foreach (var socket in _sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        public void AddSocket(WebSocket socket)
        {
            _sockets.Add(socket);
        }
    }
}
