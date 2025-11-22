using Livraria.Application.Interfaces.Response;

namespace Livraria.Application.Response
{
    public class ServiceResponse : IServiceResponse
    {
        public bool Success { get; set; } = false;
        public string Mensagem { get; set; } = string.Empty;
    }
}
