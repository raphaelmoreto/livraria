using Livraria.API.Controllers.Base;
using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Autor;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.Autor
{
    [ApiController]
    [Route("api/autor")]
    [Authorize]
    public class AutorController : BaseController
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
            var result = await autorRead.GetAll();
            return Sucesso(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutorPorId([FromRoute] int id)
        {
            var result = await autorRead.GetById(id);
            if (result == null)
            {
                return NaoEncontrado("AUTOR NÃO ENCONTRADO");
            }
            return Sucesso(result);            
        }

        [HttpPost]
        public async Task<IActionResult> PostAutor([FromBody] AutorInputDto autor)
        {
            var result = await autorService.Insert(autor);
            if (!result.Success)
            {
                if (result.TipoErro == TipoErro.Conflict)
                    return Conflito(result);

                return RegraNegocio(result);
            }

            return Sucesso(result);           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor([FromRoute] int id, [FromBody] AutorInputDto autor)
        {
            var result = await autorService.Update(id, autor);
            if (!result.Success)
            {
                if (result.TipoErro == TipoErro.Conflict)
                    return Conflito(result);

                return RegraNegocio(result);
            }

            return Ok(result);
        }
    }
}
