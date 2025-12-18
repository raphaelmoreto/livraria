using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.Autor
{
    public interface IAutorWriteRepository : IDelete<AutorEntity>, IGetAll<AutorEntity>, IGetById<AutorEntity>, IInsert<AutorEntity>, IUpdate<AutorEntity>
    {
        //Task<bool> BuscarAutorPorNome(string nomeAutor);
    }
}
