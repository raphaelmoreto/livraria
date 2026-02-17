using Livraria.Application.Interfaces.Services.CategoriaLivro;
using Livraria.Application.Interfaces.Services.Response;
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
                Response.SetError($"ERRO! {insert}");
                return Response;
            }

            Response.SetSuccess("CATEGORIA CADASTRADA COM SUCESSO");
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, CategoriaLivroInputDto dto)
        {
            if (id <= 0)
            {
                Response.SetError("ID DA CATEGORIA NÃO INFORMADA");
                return Response;
            }

            var categoria = await repositoryCategoria.GetById(id);
            if (categoria == null)
            {
                Response.SetError("CATEGORIA NÃO ENCONTRADA NO BANCO!");
                return Response;
            }

            categoria.AtribuirNome(dto.Nome);
            categoria.Validar();

            var categoriaAtualizada = await repositoryCategoria.Update(categoria);
            if (!categoriaAtualizada)
            {
                Response.SetError($"ERRO! {categoriaAtualizada}");
                return Response;
            }

            Response.SetSuccess("CATEGORIA ATUALIZADA COM SUCESSO");
            return Response;
        }
    }
}
