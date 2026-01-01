using Livraria.Application.Factory.Livro;
using Livraria.Application.Interfaces.Services.Arquivo;
using Livraria.Application.Interfaces.Services.Autor;
using Livraria.Application.Interfaces.Services.CategoriaLivro;
using Livraria.Application.Interfaces.Services.Livro;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Interfaces.Services.Token;
using Livraria.Application.Interfaces.Services.Usuario;
using Livraria.Application.Response;
using Livraria.Application.Services.Autor;
using Livraria.Application.Services.CategoriaLivro;
using Livraria.Application.Services.Livro;
using Livraria.Application.Services.Token;
using Livraria.Application.Services.Usuario;
using Livraria.Domain.Dtos.Livro;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            ExcelPackage.License.SetNonCommercialPersonal("BlaBlaBla");

            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ICategoriaLivroService, CategoriaLivroService>();
            services.AddScoped<IGerarArquivo<LivroOutputDto>, FabricaArquivoLivro>();
            services.AddScoped<ILerArquivo<LivroInputDto>, FabricaArquivoLivro>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IServiceResponse, ServiceResponse>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            //'AddSingleton' É USADO PARA REGISTRAR UM SERVIÇO QUE SERÁ REUTILIZADO DURANTE TODA A VIDA DA APLICAÇÃO 
            services.AddSingleton<ITokenService>(token => { var secret = configuration["Jwt:SecretKey"]; return new TokenService(secret!); });

            return services;
        }
    }
}
