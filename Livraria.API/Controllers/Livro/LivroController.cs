using Livraria.Application.Interfaces.Livro;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Livraria.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Livraria.API.Controllers.Livro
{
    [ApiController]
    [Route("api/livro")]
    [Authorize]
    public class LivroController : BaseController
    {
        private readonly ILivroService livroService;

        private readonly ILivroReadRepository livroRead;

        public LivroController(ILivroService livroService, ILivroReadRepository livroRead)
        {
            this.livroService = livroService;
            this.livroRead = livroRead;
        }

        [HttpGet]
        public async Task<IActionResult> GetLivros()
        {
            try
            {
                var result = await livroRead.GetAll();
                if (result == null)
                {
                    return NotFound("LISTAGEM DE LIVROS VÁZIA");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message} ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLivroPorId([FromRoute] int id)
        {
            try
            {
                var result = await livroRead.GetById(id);
                if (result == null)
                {
                    return NotFound("LIVRO NÃO ENCONTRADO");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostLivro([FromBody] LivroInputDto livro)
        {
            try
            {
                var result = await livroService.Insert(livro);
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
        public async Task<IActionResult> PutLivro([FromRoute] int id, [FromBody] LivroInputDto livro)
        {
            try
            {
                var result = await livroService.Update(id, livro);
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
