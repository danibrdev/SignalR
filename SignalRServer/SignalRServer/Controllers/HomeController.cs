using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.DTOs.RequestModel;
using SignalRServer.Domain.Interfaces.Services;
using SignalRServer.Service.Hubs;

namespace SignalRServer.Controllers
{
    /// <summary>
    /// Enviar mensagems de fora de um hub
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IHubContext<ExampleHub> _hubContext;
        private readonly ISolicitacaoService _solicitacaoService;

        public HomeController(IHubContext<ExampleHub> hubContext, ISolicitacaoService solicitacaoService)
        {
            _hubContext = hubContext;
            _solicitacaoService = solicitacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _hubContext.Clients.All.SendAsync("Notify", $"Home page loaded at: {DateTime.Now}");
            return View();
        }


        [HttpPost ("TesteSolicitacao")]
        public async Task<IActionResult> TesteSolicitacao([FromBody] RetornoMotorCreditoRequestModel model)
        {
            var retorno = await _solicitacaoService.EnviarSolicitacao(model);
            return Ok(retorno);
        }
    }
}
