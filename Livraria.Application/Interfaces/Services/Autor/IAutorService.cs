using Livraria.Application.Interfaces.Services.Base;
using Livraria.Domain.Dtos.Autor;

namespace Livraria.Application.Interfaces.Services.Autor
{
    public interface IAutorService : IDelete, IInsert<AutorInputDto>, IUpdate<AutorInputDto>
    {

    }
}
