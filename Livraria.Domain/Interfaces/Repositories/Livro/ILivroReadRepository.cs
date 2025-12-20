using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.Livro
{
    public interface ILivroReadRepository : IGetAll<LivroOutputDto>, IGetById<LivroOutputDto>
    {
        Task<IEnumerable<LivroOutputDto>> BuscaPorPaginacao(int page, int pageSize = 20);
    }
}
