using Livraria.Domain.Entities.CategoriaLivro;

namespace Livraria.Domain.Interfaces.Repositories.CategoriaLivro
{
    public interface ICategoriaWriteRepository : IRepositoryWrite<CategoriaLivroEntity>
    {
        Task<bool> BuscarCategoriaPorNome(string nomeCategoria);
    }
}
