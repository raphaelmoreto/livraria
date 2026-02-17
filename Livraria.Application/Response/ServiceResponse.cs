using Livraria.Application.Enums.Response;
using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Response
{
    public class ServiceResponse : IServiceResponse
    {
        public string Tipo { get; private set; }

        public bool Success { get; private set; } = false;

        public string Mensagem { get; private set; } = string.Empty;

        public void SetError(string mensagem)
        {
            Tipo = ResponseEnum.Error.ToString();
            SetMensagem(mensagem);
        }

        private void SetMensagem(string mensagem)
        {
            Mensagem = mensagem;
        }

        public void SetSuccess(string mensagem)
        {
            Tipo = ResponseEnum.Success.ToString();
            Success = true;
            SetMensagem(mensagem);
        }

        public void SetWarning(string mensagem)
        {
            Tipo = ResponseEnum.Warning.ToString();
            SetMensagem(mensagem);
        }
    }
}
