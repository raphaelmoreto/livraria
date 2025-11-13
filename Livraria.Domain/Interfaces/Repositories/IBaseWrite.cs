
namespace Livraria.Domain.Interfaces.Repositories
{
    public interface IBaseWrite<T> where T : class
    {
        Task<bool> Delete(T entity);

        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();

        Task<bool> Insert(T entity);

        Task<bool> Update(T entity);
    }
}
