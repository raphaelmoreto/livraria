namespace Livraria.Domain.Interfaces.Repositories.Base
{
    public interface IUpdate<T> where T : class
    {
        Task<bool> Update(T entity);
    }
}
