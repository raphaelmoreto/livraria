using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Infrastructure.Connection;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.AutorRepositories;
using Livraria.Infrastructure.Repositories.AutorRepository;
using Livraria.Infrastructure.Repositories.CategoriaRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfRepositories
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //CONNECTION
            services.AddScoped<IDatabaseConnection, DatabaseConnection>();

            //REPOSITORIES
            services.AddScoped<IAutorRepository, AutorWriteRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaWriteRepository>();

            services.AddScoped<IRepositoryRead<AutorOutputDto>, AutorReadRepository>();
            services.AddScoped<IRepositoryRead<CategoriaLivroOutputDto>, CategoriaReadRepository>();

            return services;
        }
    }
}
