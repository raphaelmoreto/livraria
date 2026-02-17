
namespace Livraria.Application.Interfaces.Services.Response
{
    public interface IServiceResponse
    {
        string Tipo { get; }

        bool Success { get; }

        string Mensagem { get; }

        void SetError(string mensagem);

        void SetSuccess(string mensagem);

        void SetWarning(string mensagem);
    }
}
