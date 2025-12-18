using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.Livro
{
    public interface ILivroReadRepository : IGetAll<LivroOutputDto>, IGetById<LivroOutputDto>
    {

    }
}
