
namespace Livraria.Infrastructure.Interfaces
{
    public interface IBaseRead<T> where T : class
    {
        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();
    }
}
