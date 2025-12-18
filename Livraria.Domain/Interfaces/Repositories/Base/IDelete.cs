
namespace Livraria.Domain.Interfaces.Repositories.Base
{
    public interface IDelete<T> where T : class
    {
        Task<bool> Delete(T entity);
    }
}
