
namespace Livraria.Domain.Interfaces.Repositories
{
    public interface IRepositoryRead<T> where T : class
    {
        Task<IEnumerable<T>> Listar();

        Task<T?> SelecionarPorId(int id);
    }
}
