
namespace Livraria.Application.Interfaces.Services.Response
{
    public interface IServiceResponse
    {
        bool Success { get; }

        string Mensagem { get; }

        public List<string> Notificacoes { get; }
    }
}
