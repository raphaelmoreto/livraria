using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.Autor
{
    public interface IAutorReadRepository : IGetAll<AutorOutputDto>, IGetById<AutorOutputDto>
    {
        Task<bool> VerificarIdDoAutor(int numeroAutor);
    }
}
