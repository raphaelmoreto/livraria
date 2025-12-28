using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Interfaces.Services.Arquivo
{
    public interface IArquivoService
    {
        Task<IServiceResponse> GerarArquivo(string extensao);

        Task<IServiceResponse> ImportarArquivo(string extensao, byte[] dados);
    }
}
