using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Interfaces.Services.Base
{
    public interface IUpdate<T> where T : class
    {
        Task<IServiceResponse> Update(int id, T dto);
    }
}
