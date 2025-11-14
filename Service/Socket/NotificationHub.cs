
using Microsoft.AspNetCore.SignalR;

namespace Application
{
    public class NotificationHub : Hub
    {
        public async Task SendToUser(string userId,string Message,string title)
        {
            await Clients.User(userId).SendAsync("RecievedNotification",Message,title);
        }
    }

}
