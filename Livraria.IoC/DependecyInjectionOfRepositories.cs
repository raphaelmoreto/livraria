using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrasctructure.Connection;
using Livraria.Infrasctructure.Interfaces;
using Livraria.Infrasctructure.Repositories.Base;
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
            services.AddScoped<IBaseWrite<AutorEntity>, BaseWrite<AutorEntity>>();
            services.AddScoped<IBaseWrite<CategoriaLivroEntity>, BaseWrite<CategoriaLivroEntity>>();
            services.AddScoped<IBaseWrite<LivroEntity>, BaseWrite<LivroEntity>>();

            return services;
        }
    }
}
