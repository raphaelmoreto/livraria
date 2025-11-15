using Livraria.Application.Interfaces.Autor;
using Livraria.Application.Interfaces.CategoriaLivro;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Response;
using Livraria.Application.Services.Autor;
using Livraria.Application.Services.CategoriaLivro;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceResponse, ServiceResponse>();

            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ICategoriaLivroService, CategoriaLivroService>();

            return services;
        }
    }
}
