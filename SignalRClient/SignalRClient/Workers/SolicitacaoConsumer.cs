using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using SignalRClient.Domain.DTOs.OutputModels;

namespace SignalRClient.Workers
{
    public class SolicitacaoConsumer : IHostedService
    {
        private readonly ILogger<SolicitacaoConsumer> _logger;
        private HubConnection _connection;
        private int _contador;

        public SolicitacaoConsumer(ILogger<SolicitacaoConsumer> logger)
        {
            _logger = logger;

            IniciarHub(); 
        }

        protected void IniciarHub()
        {
            _connection = new HubConnectionBuilder()
                   .WithUrl("https://localhost:7125/SolicitacaoHub")
                   .WithAutomaticReconnect()
                   .Build();

            _connection.On<RetornoMotorCreditoOutputModel>("EnviarRetornoMotorCreditoAsync", ProcessarRetornoMotorCredito);

            _contador = 1;
        }

        protected Task ProcessarRetornoMotorCredito(RetornoMotorCreditoOutputModel outputModel)
        {
            var json = JsonConvert.SerializeObject(outputModel); 
            _logger.LogInformation($"Retorno do motor de credito: {json}");
            _logger.LogInformation($"TOTAL: {_contador}");

            _contador++;

            return Task.CompletedTask; 
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);

                    break;
                }
                catch
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _connection.DisposeAsync();
        }
    }
}
 