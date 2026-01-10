using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using Livraria.Infrastructure.Arquivo.Exportar.Livro;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfInfrastructure
    {
        public static IServiceCollection AddInfrastructure(IServiceCollection services)
        {
            ExcelPackage.License.SetNonCommercialPersonal("BlaBlaBla");

            services.AddScoped<IExportarLivro, LivroXlsx>();
            services.AddScoped<IExportarLivro, LivroCsv>();

            return services;
        }
    }
}
