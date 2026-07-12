using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Response
{
    public class ServiceResponse : IServiceResponse
    {
        public TipoRetorno TipoRetorno { get; private set; }

        public bool Success { get; private set; } = false;

        public string Mensagem { get; private set; } = string.Empty;

        public List<string> Notificacoes { get; private set; } = [];

        private ServiceResponse() { }

        public static ServiceResponse Error(TipoRetorno tipoRetorno, string mensagem, IEnumerable<string>? notificacoes = null)
        {
            return new ServiceResponse
            {
                TipoRetorno = tipoRetorno,
                Success = false,
                Mensagem = mensagem,
                Notificacoes = notificacoes?.ToList() ?? []
            };
        }

        public static ServiceResponse Ok(string mensagem)
        {
            return new ServiceResponse
            {
                TipoRetorno = TipoRetorno.Ok,
                Success = true,
                Mensagem = mensagem
            };
        }
    }
}
