using Livraria.Domain.Entities.CategoriaLivro;

namespace Livraria.Domain.Interfaces.Repositories.CategoriaLivro
{
    public interface ICategoriaRepository : IRepositoryWrite<CategoriaLivroEntity>
    {
        Task<bool> BuscarCategoriaPorNome(string nomeCategoria);
    }
}
