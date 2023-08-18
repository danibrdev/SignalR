using SignalRClient.Domain.DTOs.InputModels;

namespace SignalRClient.Domain.Interfaces.Services
{
    public interface ISolicitacaoService
    {
        Task PublicarMensagemAsync(SolicitacaoInputModel inputModel);
    }
}