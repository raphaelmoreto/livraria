using Livraria.Domain.Entities;
using Livraria.Infrastructure.Connection;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
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

            //AUTOR
            services.AddScoped<IBaseRead<Autor>, BaseRead<Autor>>();
            services.AddScoped<IBaseRead<Categoria>, BaseRead<Categoria>>();
            services.AddScoped<IBaseRead<Livro>, BaseRead<Livro>>();
            services.AddScoped<IBaseWrite<Autor>, BaseWrite<Autor>>();
            services.AddScoped<IBaseWrite<Categoria>, BaseWrite<Categoria>>();
            services.AddScoped<IBaseWrite<Livro>, BaseWrite<Livro>>();

            return services;
        }
    }
}
