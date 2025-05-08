using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OkalaExchange.ApplicationService.Exchange.Queries;
using OkalaExchange.Contracts.Exchange.ExternalServices;
using OkalaExchange.Infra.ExternalServices.CryptoService;

namespace OkalaExchange.ApplicationService.Common.Extensions
{
    public static class ServiceExtension
    {

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetLatestQuoteQueryHandler).Assembly));
            services.AddHttpClient<IExchangeService, ExchangeService>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()); return services;
        }
    }
}
