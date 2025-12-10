using Livraria.Domain.Dtos.Usuario;

namespace Livraria.Application.Interfaces.Token
{
    public interface ITokenService
    {
        string GerarToken(LoginDto usuario);
    }
}
