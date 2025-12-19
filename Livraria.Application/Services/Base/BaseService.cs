using Livraria.Application.Interfaces.Services.Response;

namespace Livraria.Application.Services.Base
{
    public abstract class BaseService
    {
        protected IServiceResponse Response;

        public BaseService(IServiceResponse serviceResponse)
        {
            Response = serviceResponse;
        }
    }
}
