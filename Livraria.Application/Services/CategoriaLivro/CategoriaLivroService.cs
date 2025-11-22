using Livraria.Application.Interfaces.CategoriaLivro;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;

namespace Livraria.Application.Services.CategoriaLivro
{
    public class CategoriaLivroService : BaseService, ICategoriaLivroService
    {
        private readonly ICategoriaWriteRepository repositoryCategoria;

        public CategoriaLivroService
        (
            IServiceResponse serviceResponse,
            ICategoriaWriteRepository repositoryCategoria
        ) : base(serviceResponse)
        {
            this.Response = serviceResponse;
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

            var insert = await repositoryCategoria.Insert(categoria);
            if (!insert)
            {
                Response.Mensagem = $"ERRO! {insert}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "CATEGORIA CADASTRADA COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, CategoriaLivroInputDto dto)
        {
            if (id <= 0)
            {
                Response.Mensagem = "ID DA CATEGORIA NÃO INFORMADA";
                return Response;
            }

            var categoria = await repositoryCategoria.Get(id);
            if (categoria == null)
            {
                Response.Mensagem = "CATEGORIA NÃO ENCONTRADA NO BANCO!";
                return Response;
            }

            categoria.AtribuirNome(dto.Nome);
            categoria.Validar();

            var categoriaAtualizada = await repositoryCategoria.Update(categoria);
            if (!categoriaAtualizada)
            {
                Response.Mensagem = $"ERRO! {categoriaAtualizada}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "CATEGORIA ATUALIZADA COM SUCESSO";
            return Response;
        }
    }
}
