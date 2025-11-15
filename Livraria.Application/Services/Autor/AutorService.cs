using Livraria.Application.Interfaces.Autor;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories;

namespace Livraria.Application.Services.Autor
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IRepositoryWrite<AutorEntity> repositoryAutor;

        public AutorService
        (
            IServiceResponse serviceResponse,
            IRepositoryWrite<AutorEntity> repositoryAutor
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
                Response.Mensagem = $"ERRO! {insert}";
                return Response;
            }
            
            Response.Success = true;
            Response.Mensagem = "AUTOR INSERIDO COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, AutorInputDto dto)
        {
            if (id <= 0)
            {
                Response.Mensagem = "ID DO AUTOR NÃO INFORMADO!";
                return Response;
            }

            var autor = await repositoryAutor.Get(id);
            if (autor == null)
            {
                Response.Mensagem = "AUTOR NÃO CADASTRADO!";
                return Response;
            }

            autor.AtribuirNome(dto.Nome);
            autor.Validar();

            var autorAtualizado = await repositoryAutor.Update(autor);
            if (!autorAtualizado)
            {
                Response.Mensagem = $"ERRO! {autorAtualizado}";
                return Response;
            }

            return Response;
        }
    }
}
