using Livraria.Domain.Dtos.CategoriaLivro;

namespace Livraria.Domain.Interfaces.Repositories.CategoriaLivro
{
    public interface ICategoriaReadRepository : IRepositoryRead<CategoriaLivroOutputDto>
    {
        Task<bool> VerificarIdDaCategoria(int numeroCategoria);
    }
}
