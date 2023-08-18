namespace SignalRClient.Domain.Interfaces.Services
{
    public interface IRabbitMqService
    {
        Task EnviarMensagemRabbitAsync(string mensagem); 
    }
}
