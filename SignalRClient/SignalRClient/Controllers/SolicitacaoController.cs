using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClient.Domain.DTOs.InputModels;

namespace SignalRClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolicitacaoController : ControllerBase
    {
        //private readonly ISolicitacaoService _solicitacaoService;
        private readonly ILogger<SolicitacaoController> _logger;

        public SolicitacaoController(ILogger<SolicitacaoController> logger/*, ISolicitacaoService solicitacaoService*/)
        {
            _logger = logger;
            //_solicitacaoService = solicitacaoService;
        }

        //[HttpPost("EnviarSolicitacao")]
        //public async Task EnviarSolicitacao([FromBody] SolicitacaoInputModel inputModel)
        //    => await _solicitacaoService.PublicarMensagemAsync(inputModel);

        [HttpPost("EnviarSolicitacaoHub")]
        public async Task<string> SendExampleAsync([FromBody] SolicitacaoInputModel inputModel)
        {
            var uri = "https://localhost:7125/SolicitacaoHub";
            var hubConnection = new HubConnectionBuilder()
                 .WithUrl(uri)
                 .WithAutomaticReconnect()
                 .Build();

            await hubConnection.StartAsync();

            try
            {
                await hubConnection.InvokeAsync("ReceberRetornoMotorCreditoAsync", inputModel);
            }
            catch (Exception)
            {
                throw;
            }

            return string.Empty;
        }
    }
}