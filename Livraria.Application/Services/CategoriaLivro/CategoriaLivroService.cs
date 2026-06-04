using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.CategoriaLivro;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;

namespace Livraria.Application.Services.CategoriaLivro
{
    public class CategoriaLivroService : BaseService, ICategoriaLivroService
    {
        private readonly ICategoriaWriteRepository repositoryCategoria;

        public CategoriaLivroService(ICategoriaWriteRepository repositoryCategoria)
        {
            this.repositoryCategoria = repositoryCategoria;
        }

        public Task<IServiceResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResponse> Insert(CategoriaLivroInputDto dto)
        {
            var categoria = new CategoriaLivroEntity
            (
                dto.Nome
            );
            if (!categoria.Validar())
                return ServiceResponse.Error
                (
                    TipoRetorno.Validation,
                    "ERRO DE VALIDAÇÃO",
                    categoria.Notifications.Select(x => x.Message)
                );

            var insert = await repositoryCategoria.Insert(categoria);
            if (!insert)
                return ServiceResponse.Error(TipoRetorno.Conflict, "CATEGORIA JÁ CADASTRADA NA BASE");

            return ServiceResponse.Ok("CATEGORIA CADASTRADA COM SUCESSO");
        }

        public async Task<IServiceResponse> Update(int id, CategoriaLivroInputDto dto)
        {
            if (id <= 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "ID DA CATEGORIA NÃO INFORMADA");

            var categoria = await repositoryCategoria.GetById(id);
            if (categoria == null)
                return ServiceResponse.Error(TipoRetorno.NotFound, "CATEGORIA NÃO ENCONTRADA NO BANCO!");

            categoria.AtribuirNome(dto.Nome);
            if (!categoria.Validar())
                return ServiceResponse.Error
                (
                    TipoRetorno.Validation,
                    "ERRO DE VALIDAÇÃO",
                    categoria.Notifications.Select(x => x.Message)
                );

            var categoriaAtualizada = await repositoryCategoria.Update(categoria);
            if (!categoriaAtualizada)
                return ServiceResponse.Error(TipoRetorno.Conflict, "CATEGORIA JÁ CADASTRADA NA BASE");

            return ServiceResponse.Ok("CATEGORIA ATUALIZADA COM SUCESSO");
        }
    }
}
