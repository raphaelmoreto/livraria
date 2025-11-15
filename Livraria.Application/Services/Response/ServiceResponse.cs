using Livraria.Application.Services.Interfaces;

namespace Livraria.Application.Services.Response
{
    public class ServiceResponse : IServiceResponse
    {
        public bool Success { get; set; } = false;
        public string Mensagem { get; set; }

        public ServiceResponse() { }
    }
}
