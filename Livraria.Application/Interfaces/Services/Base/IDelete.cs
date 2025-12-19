using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Interfaces.Services.Base
{
    public interface IDelete
    {
        Task<IServiceResponse> Delete(int id);
    }
}
