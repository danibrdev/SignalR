namespace SignalRServer.Domain.Interfaces.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessageAsync(string user, string message);
    }
}
