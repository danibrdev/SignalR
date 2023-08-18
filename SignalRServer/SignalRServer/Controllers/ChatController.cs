using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.Interfaces.Hubs;
using SignalRServer.Service.Hubs;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public IHubContext<ExampleHub, IChatClient> _strongChatHubContext { get; }

        public ChatController(IHubContext<ExampleHub, IChatClient> chatHubContext)
        {
            _strongChatHubContext = chatHubContext;
        }

        [HttpGet]
        public async Task SendMessage(string user, string message)
        {
            await _strongChatHubContext.Clients.All.ReceiveMessageAsync(user, message);
        }
    }
}
