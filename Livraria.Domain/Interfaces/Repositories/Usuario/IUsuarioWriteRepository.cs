using Livraria.Domain.Entities.Usuario;

namespace Livraria.Domain.Interfaces.Repositories.Usuario
{
    public interface IUsuarioWriteRepository : IRepositoryWrite<UsuarioEntity>
    {
        Task<bool> DeletarCadastro(int id);
    }
}
