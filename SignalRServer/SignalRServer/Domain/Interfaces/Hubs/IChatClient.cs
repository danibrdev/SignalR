namespace SignalRServer.Domain.Interfaces.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessageAsync(string user, string message);
    }
}
