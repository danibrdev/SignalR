using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SignalRClient.Domain.Common;
using SignalRClient.Domain.Config;
using SignalRClient.Domain.Interfaces.Services;
using System.Text;

namespace SignalRClient.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;

        private IModel _channel;
        private IConnection _connection;

        private RabbitMqConfig _rabbitMqConfig;
        private ILogger<RabbitMqService> _logger;

        public RabbitMqService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            ConstruirEscopo();
            IniciarRabbitMq(); 
        }

        public void ConstruirEscopo()
        {
            _scope = _serviceProvider.CreateScope();
            _logger = _scope.ServiceProvider.GetRequiredService<ILogger<RabbitMqService>>();
            _rabbitMqConfig = _scope.ServiceProvider.GetRequiredService<IOptions<RabbitMqConfig>>().Value;
        }

        private void IniciarRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfig.Host,
                UserName = _rabbitMqConfig.Username,
                Password = _rabbitMqConfig.Password,
                VirtualHost = _rabbitMqConfig.Environment
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclarePassive(Constantes.RABBITMQ_FILA_POC_SIGNALR);

            _channel.BasicQos(0, 2, false);
        }

        public async Task EnviarMensagemRabbitAsync(string mensagem)
        {
            await Task.Run(() =>
            {
                try
                {
                    var body = Encoding.UTF8.GetBytes(mensagem);

                    _channel.BasicPublish(
                        exchange: "",
                        routingKey: Constantes.RABBITMQ_FILA_POC_SIGNALR,
                        basicProperties: null,
                        body: body
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erro ao enviar para a fila {Constantes.RABBITMQ_FILA_POC_SIGNALR}");
                }
            });
        }
    }
}
