using Livraria.API.Controllers.Base;
using Livraria.API.Helpers.MimeType;
using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Livro;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("remover/categorias/{idLivro}")]
        public async Task<IActionResult> RemoverCategorias([FromRoute] int idLivro, List<int> categorias)
        {
            var result = await livroService.RemoverCategorias(idLivro, categorias);
            if (!result.Success)
            {
                if (result.TipoRetorno == TipoRetorno.BadRequest)
                    return BadRequest(result.Mensagem);

                else if (result.TipoRetorno == TipoRetorno.Conflict)
                    return Conflict(result);

                else if (result.TipoRetorno == TipoRetorno.NotFound)
                    return NotFound(result.Mensagem);

                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        [HttpGet("download/livros")]
        public async Task<IActionResult> DownloadLivros([FromQuery] string extensao)
        {
                var result = await livroService.DownloadLivros(extensao);
                return File(result, MimeTypeHelper.GetMimeType(extensao), $"download-livros{extensao}");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLivros()
        {
            var result = await livroRead.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLivroPorId([FromRoute] int id)
        {
            var result = await livroRead.GetById(id);
            if (result == null)
            {
                return NotFound("LIVRO NÃO ENCONTRADO");
            }

            return Ok(result);
        }

        [HttpGet("busca/paginacao")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPorPaginacao(int page = 1, int pageSize = 20)
        {
            var result = await livroRead.BuscaPorPaginacao(page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostLivro([FromBody] LivroInputDto livro)
        {
            var result = await livroService.Insert(livro, UsuarioLogado);
            if (!result.Success)
            {
                if (result.TipoRetorno == TipoRetorno.Conflict)
                    return Conflict(result);

                else if (result.TipoRetorno == TipoRetorno.NotFound)
                    return NotFound(result.Mensagem);

                return UnprocessableEntity(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro([FromRoute] int id, [FromBody] LivroInputDto livro)
        {
            var result = await livroService.Update(id, livro);
            if (!result.Success)
            {
                if (result.TipoRetorno == TipoRetorno.Conflict)
                    return Conflict(result);

                else if (result.TipoRetorno == TipoRetorno.NotFound)
                    return NotFound(result.Mensagem);

                return UnprocessableEntity(result);
            }
            return Ok(result);
        }
    }
}
