using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;

namespace Livraria.IoC
{
    public static class DependecyInjectionOfInfrastructure
    {
        public static IServiceCollection AddInfrastructure(IServiceCollection services)
        {
            ExcelPackage.License.SetNonCommercialPersonal("BlaBlaBla");

            return services;
        }
    }
}
