using Livraria.Application.Interfaces.Autor;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;

namespace Livraria.Application.Services.Autor
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorRepository repositoryAutor;

        public AutorService
        (
            IServiceResponse serviceResponse,
            IAutorRepository repositoryAutor
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
            var verificarSeExiste = await repositoryAutor.BuscarAutorPorNome(dto.Nome);
            if (verificarSeExiste)
            {
                Response.Mensagem = "AUTOR JÁ CADASTRADO";
                return Response;
            }

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
                Response.Mensagem = "AUTOR NÃO ENCONTRADO NO BANCO!";
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

            Response.Success = true;
            Response.Mensagem = "AUTOR ATUALIZADO COM SUCESSO";
            return Response;
        }
    }
}
