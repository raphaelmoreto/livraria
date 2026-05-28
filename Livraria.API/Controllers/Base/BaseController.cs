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

        //Status HTTP: 409
        protected IActionResult Conflito(object data) => Conflict(new { data });

        //Status HTTP: 400
        protected IActionResult Erro(string mensagem) => BadRequest(new { erro = mensagem });

        //Status HTTP: 401
        protected IActionResult NaoAutorizado(string mensagem) => Unauthorized(new { erro = mensagem });

        //Status HTTP: 404
        protected IActionResult NaoEncontrado(string mensagem) => NotFound(new { erro = mensagem });

        //Status HTTP: 403
        protected IActionResult Proibido() => Forbid();

        //Status HTTP: 422 | USADO EM REGRAS DE NEGÓCIO/VALIDAÇÕES
        protected IActionResult RegraNegocio(object data) => UnprocessableEntity(new { data });

        //Status HTTP: 200
        protected IActionResult Sucesso(object? data = null) => Ok(new { data });
    }
}
