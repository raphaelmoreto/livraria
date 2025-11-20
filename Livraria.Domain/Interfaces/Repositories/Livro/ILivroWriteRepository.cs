using Livraria.Domain.Entities.Livro;

namespace Livraria.Domain.Interfaces.Repositories.Livro
{
    public interface ILivroWriteRepository : IRepositoryWrite<LivroEntity>
    {
        Task<bool> BuscarLivroPorNome(string nomeLivro, string codigo_barras);
    }
}
