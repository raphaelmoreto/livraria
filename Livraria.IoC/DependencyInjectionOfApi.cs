using Livraria.Application.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.IoC
{
    public static class DependencyInjectionOfApi
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            //'AddSingleton' É USADO PARA REGISTRAR UM SERVIÇO QUE SERÁ REUTILIZADO DURANTE TODA A VIDA DA APLICAÇÃO 
            services.AddSingleton
            (
                x => new TokenService(configuration["Jwt:SecretKey"]!)
            );

            return services;
        }
    }
}
