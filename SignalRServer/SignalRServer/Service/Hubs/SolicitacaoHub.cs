using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.DTOs.RequestModel;
using SignalRServer.Domain.Interfaces.Hubs;

namespace SignalRServer.Service.Hubs
{
    public class SolicitacaoHub : Hub<ISolicitacaoHub>
    {
        public async Task<RetornoMotorCreditoRequestModel> ReceberRetornoMotorCreditoAsync(RetornoMotorCreditoRequestModel retornoMotorCredito)
        {
            await Clients.All.EnviarRetornoMotorCreditoAsync(retornoMotorCredito);

            return retornoMotorCredito;
        }

        //Controlar as conexoes
        public override async Task OnConnectedAsync()
        {
            //Adicionar o usuario ao grupo
            //await Groups.AddToGroupAsync(Context.ConnectionId, "SolicitacaoHub");
            await base.OnConnectedAsync();
        }

        //Executar ações quando um cliente se desconectar
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //RemoveFromGroupAsync não precisa ser chamado OnDisconnectedAsync, ele é tratado automaticamente para você.
            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SolicitacaoHub");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
