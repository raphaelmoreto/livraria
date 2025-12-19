namespace Livraria.Application.Interfaces.Services.Response
{
    public interface IServiceResponse
    {
        bool Success { get; set; }

        string Mensagem { get; set; }
    }
}
