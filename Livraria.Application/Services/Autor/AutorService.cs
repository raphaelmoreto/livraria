using Livraria.Application.Interfaces.Services.Autor;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;

namespace Livraria.Application.Services.Autor
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorWriteRepository repositoryAutor;

        public AutorService
        (
            IServiceResponse serviceResponse,
            IAutorWriteRepository repositoryAutor
        ) : base(serviceResponse)
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

            var insert = await repositoryAutor.Insert(autor);
            if (!insert)
            {
                Response.SetError($"ERRO! {insert}");
                return Response;
            }

            Response.SetSuccess("AUTOR INSERIDO COM SUCESSO");
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, AutorInputDto dto)
        {
            if (id <= 0)
            {
                Response.SetError("ID DO AUTOR NÃO INFORMADO!");
                return Response;
            }

            var autor = await repositoryAutor.GetById(id);
            if (autor == null)
            {
                Response.SetError("AUTOR NÃO ENCONTRADO NO BANCO!");
                return Response;
            }

            autor.AtribuirNome(dto.Nome);
            autor.Validar();

            var autorAtualizado = await repositoryAutor.Update(autor);
            if (!autorAtualizado)
            {
                Response.SetError($"ERRO! {autorAtualizado}");
                return Response;
            }

            Response.SetSuccess("AUTOR ATUALIZADO COM SUCESSO");
            return Response;
        }
    }
}
