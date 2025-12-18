using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.CategoriaLivro
{
    public interface ICategoriaReadRepository : IGetAll<CategoriaLivroOutputDto>, IGetById<CategoriaLivroOutputDto>
    {
        Task<bool> VerificarIdDaCategoria(int numeroCategoria);
    }
}
