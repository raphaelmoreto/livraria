using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Interfaces.Services.Base
{
    public interface IInsert<T> where T : class
    {
        Task<IServiceResponse> Insert(T dto);
    }
}
