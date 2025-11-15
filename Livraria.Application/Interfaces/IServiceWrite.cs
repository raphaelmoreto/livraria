using Livraria.Application.Interfaces.Response;

namespace Livraria.Application.Interfaces
{
    public interface IServiceWrite<T> where T : class
    {
        Task<IServiceResponse> Delete(int id);

        Task<IServiceResponse> Insert(T dto);

        Task<IServiceResponse> Update(int id, T dto);
    }
}
