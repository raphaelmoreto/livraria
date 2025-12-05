using Livraria.Domain.Dtos.Autor;

namespace Livraria.Domain.Interfaces.Repositories.Autor
{
    public interface IAutorReadRepository : IRepositoryRead<AutorOutputDto>
    {
        Task<bool> VerificarIdDoAutor(int numeroAutor);
    }
}
