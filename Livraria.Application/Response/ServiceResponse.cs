using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Response
{
    public class ServiceResponse : IServiceResponse
    {
        public bool Success { get; private set; } = false;

        public string Mensagem { get; private set; } = string.Empty;

        public List<string> Notificacoes { get; private set; } = [];

        private ServiceResponse() { }

        public static ServiceResponse Error(string mensagem, IEnumerable<string>? notificacoes = null)
        {
            return new ServiceResponse
            {
                Success = false,
                Mensagem = mensagem,
                Notificacoes = notificacoes?.ToList() ?? []
            };
        }

        public static ServiceResponse Ok(string mensagem)
        {
            return new ServiceResponse
            {
                Success = true,
                Mensagem = mensagem
            };
        }
    }
}
