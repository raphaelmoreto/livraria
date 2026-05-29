using Livraria.API.Controllers.Base;
using Livraria.Application.Enum.Response;
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
                var result = await repositoryRead.GetAll();
                return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaPorId([FromRoute] int id)
        {
            var result = await repositoryRead.GetById(id);
            if (result == null)
            {
                return NotFound("CATEGORIA NÃO ENCONTRADA");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoria([FromBody] CategoriaLivroInputDto categoria)
        {
            var result = await categoriaLivroService.Insert(categoria);
            if (!result.Success)
            {
                if (result.TipoErro == TipoErro.Conflict)
                    return Conflict(result);

                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] CategoriaLivroInputDto categoria)
        {
            var result = await categoriaLivroService.Update(id, categoria);
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
