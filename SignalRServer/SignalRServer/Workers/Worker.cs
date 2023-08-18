using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.Interfaces.Hubs;
using SignalRServer.Service.Hubs;

namespace SignalRServer.Workers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<ChatHub, IChatHub> _chatHub;

        public Worker(ILogger<Worker> logger, IHubContext<ChatHub, IChatHub> chatHub)
        {
            _logger = logger;
            _chatHub = chatHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                await _chatHub.Clients.All.ReceiveMessageAsync("teste", "message");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
