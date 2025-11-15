
namespace Livraria.Application.Services.Interfaces
{
    public interface IServiceResponse
    {
        bool Success { get; set; }

        string Mensagem { get; set; }
    }
}
