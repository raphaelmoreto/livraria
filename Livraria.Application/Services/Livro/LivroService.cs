using Livraria.Application.Interfaces.Services.Livro;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.Livro;

namespace Livraria.Application.Services.Livro
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly IAutorReadRepository autorReadRepository;

        private readonly ICategoriaReadRepository categoriaReadRepository;

        private readonly IEnumerable<IExportarLivro> exportador;

        private readonly ILivroReadRepository livroReadRepository;

        private readonly ILivroWriteRepository repositoryLivro;

        public LivroService
        (
            IAutorReadRepository autorReadRepository,
            ICategoriaReadRepository categoriaReadRepository,
            IEnumerable<IExportarLivro> exportador,
            ILivroReadRepository livroReadRepository,
            ILivroWriteRepository repositoryLivro,
            IServiceResponse serviceResponse
        ) : base(serviceResponse)
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
            this.exportador = exportador;
            this.livroReadRepository = livroReadRepository;
            this.repositoryLivro = repositoryLivro;
        }

        public async Task<byte[]> DownloadLivros(string extensao)
        {
            if (string.IsNullOrEmpty(extensao))
                throw new ArgumentNullException("EXTENSÃO NÃO DECLARADA");

            var lstLivros = (await livroReadRepository.GetAll()).ToList();

            var exportar = exportador.FirstOrDefault(e => e.SuportaExtensao(extensao));
            if (exportar is null)
                throw new NotSupportedException("EXTENSÃO NÃO SUPORTADA");

            return exportar.CriarBytes(lstLivros);
        }

        public async Task<IServiceResponse> Insert(LivroInputDto dto, string usuarioLogado)
        {
            foreach (var categoria in dto.Fk_Categoria)
            {
                var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(categoria);
                if (!idCategoria)
                {
                    Response.Mensagem = $"CATEGORIA DE Nº{categoria} NÃO ENCONTRADA NO BANCO!";
                    return Response;
                }
            }

            int idAutor = 0;
            if (dto.Autor != 0 && dto.Autor != null)
            {
                var idAutorBanco = await autorReadRepository.VerificarIdDoAutor(dto.Autor.Value);
                if (!idAutorBanco)
                {
                    Response.Mensagem = "AUTOR NÃO ENCONTRADO NO BANCO!";
                    return Response;
                }
                else
                {
                    idAutor = dto.Autor.Value;
                }
            }

            var livro = new LivroEntity
            (
                dto.Titulo,
                dto.Isbn,
                dto.Dt_Publicacao,
                dto.Preco,
                dto.Quantidade,
                dto.Fk_Categoria,
                dto.Subtitulo,
                idAutor
            );

            var insert = await repositoryLivro.Insert(livro, usuarioLogado);
            if (!insert)
            {
                Response.Mensagem = $"ERRO! {insert}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "LIVRO CADASTRADO COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> RemoverCategorias(int idLivro, List<int> categorias)
        {
            if (idLivro <= 0)
            {
                Response.Mensagem = "ID DO AUTOR NÃO INFORMADO!";
                return Response;
            }

            if (categorias.Count <= 0)
            {
                Response.Mensagem = "LISTA DE CATEGORIAS VÁZIA";
                return Response;
            }

            foreach (var categoria in categorias)
            {
                var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(categoria);
                if (!idCategoria)
                {
                    Response.Mensagem = $"CATEGORIA DE Nº{categoria} NÃO ENCONTRADA NO BANCO!";
                    return Response;
                }
            }

            foreach (var categoria in categorias)
            {
                bool exclusaoCategoria = await repositoryLivro.RemoverCategorias(idLivro, categoria);
                if (!exclusaoCategoria)
                {
                    Response.Mensagem = $"ERRO AO EXCLUIR CATEGORIA Nº{categoria}";
                    return Response;
                }
            }

            Response.Success = true;
            Response.Mensagem = "CATEGORIAS REMOVIDAS COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, LivroInputDto dto)
        {
            if (id <= 0)
            {
                Response.Mensagem = "ID DO AUTOR NÃO INFORMADO!";
                return Response;
            }

            foreach (var categoria in dto.Fk_Categoria)
            {
                var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(categoria);
                if (!idCategoria)
                {
                    Response.Mensagem = $"CATEGORIA DE Nº{categoria} NÃO ENCONTRADA NO BANCO!";
                    return Response;
                }
            }

            int idAutor = 0;
            if (dto.Autor != 0 && dto.Autor != null)
            {
                var idAutorBanco = await autorReadRepository.VerificarIdDoAutor(dto.Autor.Value);
                if (!idAutorBanco)
                {
                    Response.Mensagem = "AUTOR NÃO ENCONTRADO NO BANCO!";
                    return Response;
                }
                else
                {
                    idAutor = dto.Autor.Value;
                }
            }

            var livro = await repositoryLivro.GetById(id);
            if (livro == null)
            {
                Response.Mensagem = "LIVRO NÃO ENCONTRADO NO BANCO!";
                return Response;
            }

            livro.AtribuirTitulo(dto.Titulo);
            livro.AtribuirIsbn(dto.Isbn);
            livro.AtribuirDataPublicacao(dto.Dt_Publicacao);
            livro.AtribuirPreco(dto.Preco);
            livro.AtribuirCategoria(dto.Fk_Categoria);
            livro.AtribuirSubtitulo(dto.Subtitulo);
            livro.AtribuirAutor(idAutor);
            livro.Validar();

            var update = await repositoryLivro.Update(livro); //ToDo: FAZER UPDATE PARA O LIVRO
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
