namespace Livraria.Application.Interfaces.Response
{
    public interface IServiceResponse
    {
        bool Success { get; set; }

        string Mensagem { get; set; }
    }
}
