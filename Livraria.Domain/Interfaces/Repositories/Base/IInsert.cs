
namespace Livraria.Domain.Interfaces.Repositories.Base
{
    public interface IInsert<T> where T : class
    {
        Task<bool> Insert(T entity);
    }
}
