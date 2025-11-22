using Livraria.Domain.Entities.Autor;

namespace Livraria.Domain.Interfaces.Repositories.Autor
{
    public interface IAutorWriteRepository : IRepositoryWrite<AutorEntity>
    {
        //Task<bool> BuscarAutorPorNome(string nomeAutor);
    }
}
