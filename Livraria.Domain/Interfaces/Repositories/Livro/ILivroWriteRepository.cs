using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.Livro
{
    public interface ILivroWriteRepository : IDelete<LivroEntity>, IGetAll<LivroEntity>, IGetById<LivroEntity>, IUpdate<LivroEntity>
    {
        //Task<bool> BuscarLivroPorNome(string nomeLivro, string isbn);

        Task<bool> Insert(LivroEntity livro, string usuarioLogado);
    }
}
