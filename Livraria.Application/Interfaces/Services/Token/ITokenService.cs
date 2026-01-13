using Livraria.Domain.Dtos.Login;

namespace Livraria.Application.Interfaces.Services.Token
{
    public interface ITokenService
    {
        string GerarToken(LoginOutputDto usuario);
    }
}
