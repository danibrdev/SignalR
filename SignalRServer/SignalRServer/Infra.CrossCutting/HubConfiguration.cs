using SignalRServer.Service.Hubs;

namespace SignalRServer.Infra.CrossCutting
{
    public static class HubConfiguration
    {
        public static IEndpointRouteBuilder UseHubs(this IEndpointRouteBuilder app)
        {
            app.MapHub<SolicitacaoHub>("/SolicitacaoHub");

            return app;
        }
    }
}
