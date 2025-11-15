using Livraria.Application.Services.Interfaces;
using Livraria.Application.Services.Response;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceResponse, ServiceResponse>();

            return services;
        }
    }
}
