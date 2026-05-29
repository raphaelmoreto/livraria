using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Response;
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

        //IDEIA FUTURA
        //protected IActionResult CustomReponse(IServiceResponse result)
        //{
        //    if (result.TipoErro == TipoErro.Ok)
        //        return Ok(result);

        //    return result.TipoErro switch
        //    {
        //        TipoErro.BadRequest => BadRequest(result.Mensagem),
        //        TipoErro.Conflict => Conflict(result),
        //        TipoErro.Forbidden => Forbid(),
        //        TipoErro.NotFound => NotFound(result.Mensagem),
        //        TipoErro.Unauthorized => Unauthorized(result.Mensagem),
        //        TipoErro.Validation => UnprocessableEntity(result)
        //    };
        //}

        //Status HTTP: 400 BadRequest(erro = mensagem);

        //Status HTTP: 409 Conflict(data);

        //Status HTTP: 403 Forbid();

        //Status HTTP: 404 NotFound(erro = mensagem);

        //Status HTTP: 200Ok(data);

        //Status HTTP: 401 Unauthorized(erro = mensagem);

        //Status HTTP: 422 | USADO EM REGRAS DE NEGÓCIO/VALIDAÇÕES
        //UnprocessableEntity(data);
    }
}
