using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Livraria.API.Controllers.Base
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string UsuarioLogado
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("ID NÃO INFORMADO");
            }
        }
    }
}
