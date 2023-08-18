using SignalRClient.Domain.Config;
using SignalRClient.Domain.Interfaces.Services;
using SignalRClient.Services;

namespace SignalRClient.Infra.CrossCutting
{
    public static class CrossDependency
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureServices();
            services.ConfigureServicesOptions(configuration);

            return services;
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IRabbitMqService, RabbitMqService>();
            services.AddTransient<ISolicitacaoService, SolicitacaoService>();
        }

        private static void ConfigureServicesOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMqConfig"));
        }
    }
}
