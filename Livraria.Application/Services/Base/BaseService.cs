using Livraria.Application.Services.Interfaces;

namespace Livraria.Application.Services.Base
{
    public class BaseService
    {
        protected IServiceResponse Response;

        public BaseService(IServiceResponse serviceResponse)
        {
            Response = serviceResponse;
        }
    }
}
