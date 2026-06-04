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
        //    if (result.TipoRetorno == TipoRetorno.Ok)
        //        return Ok(result);

        //    return result.TipoRetorno switch
        //    {
        //        TipoRetorno.BadRequest => BadRequest(result.Mensagem),
        //        TipoRetorno.Conflict => Conflict(result),
        //        TipoRetorno.Forbidden => Forbid(),
        //        TipoRetorno.NotFound => NotFound(result.Mensagem),
        //        TipoRetorno.Unauthorized => Unauthorized(result.Mensagem),
        //        TipoRetorno.Validation => UnprocessableEntity(result)
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
