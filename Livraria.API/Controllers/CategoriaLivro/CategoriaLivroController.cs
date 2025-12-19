using Livraria.API.Controllers.Base;
using Livraria.Application.Interfaces.Services.CategoriaLivro;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.CategoriaLivro
{
    [ApiController]
    [Route("api/categoria-livro")]
    [Authorize]
    public class CategoriaLivroController : BaseController
    {
        private readonly ICategoriaLivroService categoriaLivroService;

        private readonly ICategoriaReadRepository repositoryRead;

        public CategoriaLivroController
        (
            ICategoriaLivroService categoriaLivroService,
            ICategoriaReadRepository repositoryRead
        )
        {
            this.categoriaLivroService = categoriaLivroService;
            this.repositoryRead = repositoryRead;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                var result = await repositoryRead.GetAll();
                if (result == null)
                {
                    return NotFound("LISTAGEM DE CATEGORIAS VÁZIA");
                }
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
                var result = await repositoryRead.GetById(id);
                if (result == null)
                {
                    return NotFound("CATEGORIA NÃO ENCONTRADA");
                }
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
                return StatusCode(500, $"ERRO INTERNO {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] CategoriaLivroInputDto categoria)
        {
            try
            {
                var result = await categoriaLivroService.Update(id, categoria);
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
                return StatusCode(500, $"ERRO INTERNO {ex.Message}");
            }
        }
    }
}
