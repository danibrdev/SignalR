using SignalRServer.Domain.DTOs.RequestModel;
using SignalRServer.Domain.Interfaces.Services;

namespace SignalRServer.Service
{
    public class SolicitacaoService : ISolicitacaoService
    {

        public SolicitacaoService(HttpClient httpClient)
        {
        }

        public async Task<bool> EnviarSolicitacao(RetornoMotorCreditoRequestModel model)
        {
            using var httpClient = new HttpClient();

            var retorno = await httpClient.PostAsJsonAsync("https://localhost:44387/api/SolicitacaoCartao/Teste", model);

            return retorno.IsSuccessStatusCode; 
        }
    }
}
