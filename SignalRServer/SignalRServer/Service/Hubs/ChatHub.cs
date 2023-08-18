using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.Interfaces.Hubs;

namespace SignalRServer.Service.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessageAsync(user, message);
        }
    }
}
