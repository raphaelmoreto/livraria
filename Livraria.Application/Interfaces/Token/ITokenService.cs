using Livraria.Domain.Dtos.Login;

namespace Livraria.Application.Interfaces.Token
{
    public interface ITokenService
    {
        string GerarToken(LoginDto login);
    }
}
