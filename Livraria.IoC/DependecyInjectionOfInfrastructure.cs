using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using Livraria.Infrastructure.Arquivo.Exportar.Livro;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using QuestPDF.Infrastructure;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfInfrastructure
    {
        public static IServiceCollection AddInfrastructure(IServiceCollection services)
        {
            ExcelPackage.License.SetNonCommercialPersonal("BlaBlaBla");
            QuestPDF.Settings.License = LicenseType.Community;

            services.AddScoped<IExportarLivro, LivroCsv>();
            services.AddScoped<IExportarLivro, LivroPdf>();
            services.AddScoped<IExportarLivro, LivroXlsx>();

            return services;
        }
    }
}
