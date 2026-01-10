using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Livraria.Domain.Interfaces.Repositories.Login;
using Livraria.Domain.Interfaces.Repositories.Usuario;
using Livraria.Infrastructure.Arquivo.Exportar.Livro;
using Livraria.Infrastructure.Connection;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.AutorRepository;
using Livraria.Infrastructure.Repositories.CategoriaRepository;
using Livraria.Infrastructure.Repositories.LivroRepository;
using Livraria.Infrastructure.Repositories.LoginRepository;
using Livraria.Infrastructure.Repositories.UsuarioRepository;
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
            services.AddScoped<IAutorWriteRepository, AutorWriteRepository>();
            services.AddScoped<ICategoriaWriteRepository, CategoriaWriteRepository>();
            services.AddScoped<ILivroWriteRepository, LivroWriteRepository>();
            services.AddScoped<IUsuarioWriteRepository, UsuarioWriteRepository>();

            services.AddScoped<IAutorReadRepository, AutorReadRepository>();
            services.AddScoped<ICategoriaReadRepository, CategoriaReadRepository>();
            services.AddScoped<ILivroReadRepository, LivroReadRepository>();
            services.AddScoped<ILoginReadRepository, LoginReadRepository>();

            services.AddScoped<IExportarLivro, LivroXlsx>();

            return services;
        }
    }
}
