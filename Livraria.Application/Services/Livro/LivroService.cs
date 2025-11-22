using Livraria.Application.Interfaces.Livro;
using Livraria.Application.Interfaces.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.Livro;

namespace Livraria.Application.Services.Livro
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly IAutorReadRepository autorReadRepository;

        private readonly ICategoriaReadRepository categoriaReadRepository;

        private readonly ILivroWriteRepository repositoryLivro;

        public LivroService
        (
            IAutorReadRepository autorReadRepository,
            ICategoriaReadRepository categoriaReadRepository,
            ILivroWriteRepository repositoryLivro,
            IServiceResponse serviceResponse
        ) : base(serviceResponse)
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
            this.repositoryLivro = repositoryLivro;
        }

        public Task<IServiceResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResponse> Insert(LivroInputDto dto)
        {
            var idCategoria = await categoriaReadRepository.BuscarIdDaCategoria(dto.Categoria);
            if (idCategoria <= 0)
            {
                Response.Mensagem = "CATEGORIA NÃO ENCONTRADA NO BANCO!";
                return Response;
            }

            int idAutor = 0;

            if (!string.IsNullOrWhiteSpace(dto.Autor))
            {
                idAutor = await autorReadRepository.BuscarIdDoAutor(dto.Autor);
                if (idAutor <= 0)
                {
                    Response.Mensagem = "AUTOR NÃO ENCONTRADO NO BANCO!";
                    return Response;
                }
            }

            var livro = new LivroEntity
            (
                dto.Titulo,
                dto.Isbn,
                dto.dtPublicacao,
                dto.Preco,
                idCategoria,
                dto.Quantidade,
                dto.Subtitulo,
                idAutor
            );

            var insert = await repositoryLivro.Insert(livro);
            if (!insert)
            {
                Response.Mensagem = $"ERRO! {insert}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "LIVRO CADASTRADO COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, LivroInputDto dto)
        {
            if (id < 0)
            {
                Response.Mensagem = "ID DO AUTOR NÃO INFORMADO!";
                return Response;
            }

            var idCategoria = await categoriaReadRepository.BuscarIdDaCategoria(dto.Categoria);
            if (idCategoria <= 0)
            {
                Response.Mensagem = "CATEGORIA NÃO ENCONTRADA NO BANCO!";
                return Response;
            }

            int idAutor = 0;

            if (!string.IsNullOrWhiteSpace(dto.Autor))
            {
                idAutor = await autorReadRepository.BuscarIdDoAutor(dto.Autor);
                if (idAutor <= 0)
                {
                    Response.Mensagem = "AUTOR NÃO ENCONTRADO NO BANCO!";
                    return Response;
                }
            }

            var livro = await repositoryLivro.Get(id);
            if (livro == null)
            {
                Response.Mensagem = "LIVRO NÃO ENCONTRADO NO BANCO!";
                return Response;
            }

            livro.AtribuirTitulo(dto.Titulo);
            livro.AtribuirIsbn(dto.Isbn);
            livro.AtribuirDataPublicacao(dto.dtPublicacao);
            livro.AtribuirPreco(dto.Preco);
            livro.AtribuirCategoria(idCategoria);
            livro.AtribuirSubtitulo(dto.Subtitulo);
            livro.AtribuirAutor(idAutor);
            livro.Validar();

            var update = await repositoryLivro.Update(livro);
            if (!update)
            {
                Response.Mensagem = $"ERRO! {update}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "LIVRO ATUALIZADO COM SUCESSO";
            return Response;
        }
    }
}
