using Livraria.Application.Interfaces.Services.Base;
using Livraria.Domain.Dtos.Usuario;

namespace Livraria.Application.Interfaces.Services.Usuario
{
    public interface IUsuarioService : IDelete, IInsert<UsuarioInputDto>, IUpdate<UsuarioInputDto>
    {

    }
}
