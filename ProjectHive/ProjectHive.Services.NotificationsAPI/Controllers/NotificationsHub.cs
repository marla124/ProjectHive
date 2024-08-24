
using Microsoft.AspNetCore.SignalR;

namespace ProjectHive.Services.NotificationsAPI.Controllers
{
    public class NotificationsHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
