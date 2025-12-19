using Livraria.Domain.Dtos.Login;

namespace Livraria.Domain.Interfaces.Repositories.Login
{
    public interface ILoginReadRepository
    {
        Task<int> ValidarLogin(LoginDto login);
    }
}
