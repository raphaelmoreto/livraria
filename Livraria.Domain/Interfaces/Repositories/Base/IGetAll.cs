namespace Livraria.Domain.Interfaces.Repositories.Base
{
    public interface IGetAll<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
    }
}
