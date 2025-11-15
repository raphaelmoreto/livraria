using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Connection;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.AutorRepository;
using Livraria.Infrastructure.Repositories.Base;
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
            services.AddScoped<IRepositoryWrite<AutorEntity>, BaseWrite<AutorEntity>>();
            services.AddScoped<IRepositoryWrite<CategoriaLivroEntity>, BaseWrite<CategoriaLivroEntity>>();
            services.AddScoped<IRepositoryWrite<LivroEntity>, BaseWrite<LivroEntity>>();

            services.AddScoped<IRepositoryRead<AutorOutputDto>, AutorReadRepository>();

            return services;
        }
    }
}
