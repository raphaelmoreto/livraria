using Livraria.Domain.Entities;
using Livraria.Infrastructure.Connection;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositorys;
using Microsoft.Extensions.DependencyInjection;


namespace Livraria.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //API

            //APPLICATION

            //INFRASTRUCTURE
            services.AddScoped<IDatabaseConnection, DatabaseConnectionSqlServer>();
            services.AddScoped<IBaseRepository<Autor>, BaseRepository<Autor>>();
            services.AddScoped<IBaseRepository<Categoria>, BaseRepository<Categoria>>();
            services.AddScoped<IBaseRepository<Livro>, BaseRepository<Livro>>();

            return services;
        }
    }
}
