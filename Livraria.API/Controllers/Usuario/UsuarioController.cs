using Livraria.API.Controllers.Base;
using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Usuario;
using Livraria.Domain.Dtos.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.Usuario
{
    [ApiController]
    [Route("api/usuario")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] int id)
        {
            var result = await usuarioService.Delete(id);
            if (!result.Success)
            {
                if (result.TipoErro == TipoErro.Conflict)
                    return Conflict(result);

                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioInputDto usuario)
        {
            var result = await usuarioService.Insert(usuario);
            if (!result.Success)
            {
                if (result.TipoErro == TipoErro.Conflict)
                    return Conflict(result);

                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] UsuarioInputDto usuario)
        {
            var result = await usuarioService.Update(id, usuario);
            if (!result.Success)
            {
                if (result.TipoErro == TipoErro.Conflict)
                    return Conflict(result);

                return UnprocessableEntity(result);
            }

            return Ok(result);
        }
    }
}
