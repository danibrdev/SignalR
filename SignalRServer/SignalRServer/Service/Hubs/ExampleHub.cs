using Microsoft.AspNetCore.SignalR;
using SignalRServer.Domain.Interfaces.Hubs;

namespace SignalRServer.Service.Hubs
{
    public class ExampleHub : Hub<IChatClient>
    {
        //Envia uma mensagem para todos os clientes conectados, usando Clients.All.
        public async Task SendMessage(string user, string message)
            => await Clients.All.ReceiveMessageAsync(user, message);

        //Envia uma mensagem de volta para o chamador, usando Clients.Caller
        public async Task SendMessageToCaller(string user, string message)
            => await Clients.Caller.ReceiveMessageAsync(user, message);

        //Envia uma mensagem a todos os clientes do SignalR Users grupo
        public async Task SendMessageToGroup(string user, string message)
            => await Clients.Group("SignalR Users").ReceiveMessageAsync(user, message);

        [HubMethodName("SendMessageToUser")]
        public async Task DirectMessage(string user, string message)
            => await Clients.User(user).ReceiveMessageAsync(user, message);


        //Controlar as conexoes
        public override async Task OnConnectedAsync()
        {
            //Adicionar o usuario ao grupo
            //await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        //Executar ações quando um cliente se desconectar
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //RemoveFromGroupAsync não precisa ser chamado OnDisconnectedAsync, ele é tratado automaticamente para você.
            await base.OnDisconnectedAsync(exception);
        }
    }
}
