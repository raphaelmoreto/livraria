using Livraria.Application.Interfaces;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Response;
using Livraria.Application.Services.Autor;
using Livraria.Domain.Dtos.Autor;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceResponse, ServiceResponse>();

            services.AddScoped<IServiceWrite<AutorInputDto>, AutorService>();

            return services;
        }
    }
}
