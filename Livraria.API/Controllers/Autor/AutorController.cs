using Livraria.Application.Interfaces.Autor;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.Autor
{
    [Route("api/autor")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService autorService;

        private readonly IAutorReadRepository autorRead;

        public AutorController(IAutorService autorService, IAutorReadRepository autorRead)
        {
            this.autorService = autorService;
            this.autorRead = autorRead;
        }

        [HttpGet]
        public async Task<IActionResult> GetAutores()
        {
            try
            {
                var result = await autorRead.Listar();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutorPorId([FromRoute] int id)
        {
            try
            {
                var result = await autorRead.SelecionarPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAutor([FromBody] AutorInputDto autor)
        {
            try
            {
                var result = await autorService.Insert(autor);
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
        public async Task<IActionResult> PutAutor([FromRoute] int id, [FromBody] AutorInputDto autor)
        {
            try
            {
                var result = await autorService.Update(id, autor);
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
