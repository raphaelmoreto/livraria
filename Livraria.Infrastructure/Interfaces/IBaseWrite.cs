
namespace Livraria.Infrastructure.Interfaces
{
    public interface IBaseWrite<T> where T : class
    {
        Task<bool> Delete(T entity);

        Task<bool> Insert(T entity);

        Task<bool> Update(T entity);
    }
}
