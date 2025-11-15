using Livraria.Application.Interfaces.CategoriaLivro;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.CategoriaLivro
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaLivroController : ControllerBase
    {
        private readonly ICategoriaLivroService categoriaLivroService;

        private readonly IRepositoryRead<CategoriaLivroOutputDto> repositoryRead;

        public CategoriaLivroController(ICategoriaLivroService categoriaLivroService, IRepositoryRead<CategoriaLivroOutputDto> repositoryRead)
        {
            this.categoriaLivroService = categoriaLivroService;
            this.repositoryRead = repositoryRead;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                var result = await repositoryRead.Listar();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaPorId([FromRoute] int id)
        {
            try
            {
                var result = await repositoryRead.SelecionarPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoria([FromBody] CategoriaLivroInputDto categoria)
        {
            try
            {
                var result = await categoriaLivroService.Insert(categoria);
                return Ok(result);
            }
            catch (AggregateException aggEx)
            {
                var excecoes = aggEx.InnerExceptions.Select(ex => ex.Message);
                return BadRequest(new { excecoes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] CategoriaLivroInputDto categoria)
        {
            try
            {
                var result = await categoriaLivroService.Update(id, categoria);
                return Ok(result);
            }
            catch (AggregateException aggEx)
            {
                var excecoes = aggEx.InnerExceptions.Select(ex => ex.Message);
                return BadRequest(new { excecoes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO {ex.Message}");
            }
        }
    }
}
