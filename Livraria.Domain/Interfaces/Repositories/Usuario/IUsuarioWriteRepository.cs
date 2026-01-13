using Livraria.Domain.Entities.Usuario;
using Livraria.Domain.Interfaces.Repositories.Base;

namespace Livraria.Domain.Interfaces.Repositories.Usuario
{
    public interface IUsuarioWriteRepository : IDelete<UsuarioEntity>, IGetAll<UsuarioEntity>, IGetById<UsuarioEntity>, IInsert<UsuarioEntity>, IUpdate<UsuarioEntity>
    {
        Task<bool> DeletarCadastro(int id);

        Task<bool> VerificarIdDoPerfil(int idPerfil);
    }
}
