using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SignalRClient.Domain.Config;
using SignalRServer.Domain.DTOs.Common;
using SignalRServer.Domain.DTOs.RequestModel;
using SignalRServer.Domain.DTOs.ResponseModel;
using SignalRServer.Domain.Interfaces.Hubs;
using SignalRServer.Service.Hubs;
using System.Text;

namespace SignalRServer.Workers
{
    public class RabbitMqWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;

        private IModel _channel;
        private IConnection _connection;
        
        private IHubContext<SolicitacaoHub, ISolicitacaoHub> _solicitacaoHub;
        private ILogger<RabbitMqWorker> _logger;
        private RabbitMqConfig _rabbitMqConfig; 

        public RabbitMqWorker(
            IServiceProvider serviceProvider, IHubContext<SolicitacaoHub, ISolicitacaoHub> solicitacaoHub)
        {
            _serviceProvider = serviceProvider;
            _solicitacaoHub = solicitacaoHub; 

            ConstruirEscopo();
            IniciarRabbitMq();
        }

        public void ConstruirEscopo()
        {
            _scope = _serviceProvider.CreateScope();
            _logger = _scope.ServiceProvider.GetRequiredService<ILogger<RabbitMqWorker>>();
            _rabbitMqConfig = _scope.ServiceProvider.GetRequiredService<IOptions<RabbitMqConfig>>().Value; 
            _solicitacaoHub = _scope.ServiceProvider.GetRequiredService<IHubContext<SolicitacaoHub, ISolicitacaoHub>>(); 
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

            //Determinando a entrega de 2 mensagems por consumidor, a fim de balancear a entrega das mensagens 
            _channel.BasicQos(0, 2, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (_, ea) =>
            {
                try
                {
                    var responseModel = new RetornoSolicitacaoResponseModel();
                    var corpo = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(corpo);
                    
                    responseModel = JsonConvert.DeserializeObject<RetornoSolicitacaoResponseModel>(mensagem);

                    /* Validações do motor de crédito */

                    _logger.LogDebug("Confirmado recebimento da mensagem");
                    _channel.BasicAck(ea.DeliveryTag, false);

                    _logger.LogDebug("Enviado response para o signalR");


                    await _solicitacaoHub.Clients.Group("Solicitacao").EnviarRetornoMotorCreditoAsync(
                        new RetornoMotorCreditoRequestModel(responseModel.Usuario, responseModel.Mensagem, responseModel.Status));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro durante o consumo da fila");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(
                queue: Constantes.RABBITMQ_FILA_POC_SIGNALR,
                autoAck: false,
                consumer: consumer
            );

            return Task.CompletedTask;
        }
    }
}
