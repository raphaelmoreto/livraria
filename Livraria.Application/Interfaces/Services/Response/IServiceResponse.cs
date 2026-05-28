using Livraria.Application.Enum.Response;

namespace Livraria.Application.Interfaces.Services.Response
{
    public interface IServiceResponse
    {
        TipoErro TipoErro { get; }

        bool Success { get; }

        string Mensagem { get; }

        public List<string> Notificacoes { get; }
    }
}
