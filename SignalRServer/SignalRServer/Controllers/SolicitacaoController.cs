using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.DTOs.RequestModel;
using SignalRServer.Domain.Interfaces.Hubs;
using SignalRServer.Service.Hubs;

namespace SignalRServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoController : ControllerBase
    {
        public IHubContext<SolicitacaoHub, ISolicitacaoHub> _solicitacaoHubContext { get; }

        public SolicitacaoController(IHubContext<SolicitacaoHub, ISolicitacaoHub> solicitacaoHubContext)
        {
            _solicitacaoHubContext = solicitacaoHubContext;
        }

        [HttpPost("ProcessarSolicitacao")]
        public async Task ProcessarSolicitacao(RetornoMotorCreditoRequestModel requestModel)
        {
            await _solicitacaoHubContext.Clients.All.EnviarRetornoMotorCreditoAsync(requestModel);
        }
    }
}
