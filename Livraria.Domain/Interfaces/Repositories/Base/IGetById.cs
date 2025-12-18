
namespace Livraria.Domain.Interfaces.Repositories.Base
{
    public interface IGetById<T> where T : class
    {
        Task<T?> GetById(int id);
    }
}
