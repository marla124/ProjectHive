using ProjectHive.Services.NotificationsAPI.Dto;
using System.Net.WebSockets;

namespace ProjectHive.Services.NotificationsAPI.Services
{
    public interface IWebSocketHandler
    {
        Task SendNotificationAsync(NotificationDto message);
        void AddSocket(WebSocket socket);
    }
}
