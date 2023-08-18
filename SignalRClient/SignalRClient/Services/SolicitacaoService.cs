using Newtonsoft.Json;
using SignalRClient.Domain.DTOs.InputModels;
using SignalRClient.Domain.Interfaces.Services;

namespace SignalRClient.Services
{
    public class SolicitacaoService : ISolicitacaoService
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;

        private IRabbitMqService _rabbitMqService;

        public SolicitacaoService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ConstruirEscopo(); 
        }

        private void ConstruirEscopo()
        {
            _scope = _serviceProvider.CreateScope();
            _rabbitMqService = _scope.ServiceProvider.GetRequiredService<IRabbitMqService>(); 
        }

        public async Task PublicarMensagemAsync(SolicitacaoInputModel inputModel)
        {
            var mensagemRabbitMq = JsonConvert.SerializeObject(inputModel);

            await _rabbitMqService.EnviarMensagemRabbitAsync(mensagemRabbitMq);
        }
    }
}
