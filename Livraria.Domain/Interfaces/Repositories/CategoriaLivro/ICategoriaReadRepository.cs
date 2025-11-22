using Livraria.Domain.Dtos.CategoriaLivro;

namespace Livraria.Domain.Interfaces.Repositories.CategoriaLivro
{
    public interface ICategoriaReadRepository : IRepositoryRead<CategoriaLivroOutputDto>
    {
        Task<int> BuscarIdDaCategoria(string nomeCategoria);
    }
}
