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

        //Status HTTP: 409 Conflict(data);

        //Status HTTP: 400 BadRequest(erro = mensagem);

        //Status HTTP: 401 Unauthorized(erro = mensagem);

        //Status HTTP: 404 NotFound(erro = mensagem);

        //Status HTTP: 403 Forbid();

        //Status HTTP: 422 | USADO EM REGRAS DE NEGÓCIO/VALIDAÇÕES
        //UnprocessableEntity(data);

        //Status HTTP: 200Ok(data);
    }
}
