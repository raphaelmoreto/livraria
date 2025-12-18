using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.CategoriaLivro
{
    public interface ICategoriaWriteRepository : IDelete<CategoriaLivroEntity>, IGetById<CategoriaLivroEntity>, IGetAll<CategoriaLivroEntity>, IInsert<CategoriaLivroEntity>, IUpdate<CategoriaLivroEntity>
    {
        //Task<bool> BuscarCategoriaPorNome(string nomeCategoria);
    }
}
