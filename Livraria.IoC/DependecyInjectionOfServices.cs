using Livraria.Application.Interfaces.Autor;
using Livraria.Application.Interfaces.CategoriaLivro;
using Livraria.Application.Interfaces.Livro;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Interfaces.Token;
using Livraria.Application.Response;
using Livraria.Application.Services.Autor;
using Livraria.Application.Services.CategoriaLivro;
using Livraria.Application.Services.Livro;
using Livraria.Application.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ICategoriaLivroService, CategoriaLivroService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IServiceResponse, ServiceResponse>();

            services.AddSingleton<ITokenService>(token => { var secret = configuration["Jwt:SecretKey"]; return new TokenService(secret!); });

            return services;
        }
    }
}
