using Livraria.Domain.Entities.Autor;

namespace Livraria.Domain.Interfaces.Repositories.Autor
{
    public interface IAutorRepository : IRepositoryWrite<AutorEntity>
    {
        Task<bool> BuscarAutorPorNome(string nomeAutor);
    }
}
