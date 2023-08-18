using SignalRServer.Domain.DTOs.RequestModel;

namespace SignalRServer.Domain.Interfaces.Services
{
    public interface ISolicitacaoService
    {
        Task<bool> EnviarSolicitacao(RetornoMotorCreditoRequestModel model); 
    }
}
