using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Autor;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;

namespace Livraria.Application.Services.Autor
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorWriteRepository repositoryAutor;

        public AutorService(IAutorWriteRepository repositoryAutor)
        {
            this.repositoryAutor = repositoryAutor;
        }

        public Task<IServiceResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResponse> Insert(AutorInputDto dto)
        {
            var autor = new AutorEntity
            (
                dto.Nome
            );
            if (!autor.Validar())
                return ServiceResponse.Error(TipoRetorno.Validation, "ERRO DE VALIDAÇÃO", autor.Notifications.Select(x => x.Message));

            var insert = await repositoryAutor.Insert(autor);
            if (!insert)
                return ServiceResponse.Error(TipoRetorno.Conflict, $"AUTOR JÁ CADASTRADO NA BASE");

            return ServiceResponse.Ok("AUTOR INSERIDO COM SUCESSO");
        }

        public async Task<IServiceResponse> Update(int id, AutorInputDto dto)
        {
            if (id <= 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "ID DO AUTOR NÃO INFORMADO!");

            var autor = await repositoryAutor.GetById(id);
            if (autor == null)
                return ServiceResponse.Error(TipoRetorno.NotFound, "AUTOR NÃO ENCONTRADO NO BASE!");

            autor.AtribuirNome(dto.Nome);
            if (!autor.Validar())
                return ServiceResponse.Error(TipoRetorno.Validation, "ERRO DE VALIDAÇÃO", autor.Notifications.Select(x => x.Message));

            var autorAtualizado = await repositoryAutor.Update(autor);
            if (!autorAtualizado)
                return ServiceResponse.Error(TipoRetorno.Conflict, $"AUTOR JÁ CADASTRADO NA BASE");

            return ServiceResponse.Ok("AUTOR ATUALIZADO COM SUCESSO");
        }
    }
}
