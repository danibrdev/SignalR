using SignalRServer.Domain.DTOs.RequestModel;

namespace SignalRServer.Domain.Interfaces.Hubs
{
    public interface ISolicitacaoHub
    {
        Task EnviarRetornoMotorCreditoAsync(RetornoMotorCreditoRequestModel requestModel);
    }
}
