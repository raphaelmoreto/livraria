using Livraria.Application.Interfaces.Services.Base;
using Livraria.Domain.Dtos.CategoriaLivro;

namespace Livraria.Application.Interfaces.Services.CategoriaLivro
{
    public interface ICategoriaLivroService : IDelete, IInsert<CategoriaLivroInputDto>, IUpdate<CategoriaLivroInputDto>
    {

    }
}
