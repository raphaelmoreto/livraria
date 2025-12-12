using Livraria.API.Controllers.Base;
using Livraria.Application.Interfaces.Usuario;
using Livraria.Domain.Dtos.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.Usuario
{
    [Route("api/usuario")]
    [ApiController]
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
            try
            {
                var result = await usuarioService.Delete(id);
                if (!result.Success)
                {
                    return Conflict(result);
                }

                return Ok(result);
            }
            catch (AggregateException aggEx)
            {
                var excecoes = aggEx.InnerExceptions.Select(ex => ex.Message);
                return BadRequest(new { excecoes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioInputDto usuario)
        {
            try
            {
                var result = await usuarioService.Insert(usuario);
                if (!result.Success)
                {
                    return Conflict(result);
                }

                return Ok(result);
            }
            catch (AggregateException aggEx)
            {
                var excecoes = aggEx.InnerExceptions.Select(ex => ex.Message);
                return BadRequest(new { excecoes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] UsuarioInputDto usuario)
        {
            try
            {
                var result = await usuarioService.Update(id, usuario);
                if (!result.Success)
                {
                    return Conflict(result);
                }

                return Ok(result);
            }
            catch (AggregateException aggEx)
            {
                var excecoes = aggEx.InnerExceptions.Select(ex => ex.Message);
                return BadRequest(new { excecoes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }
    }
}
