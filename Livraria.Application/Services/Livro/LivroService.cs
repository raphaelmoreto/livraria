using Livraria.Application.Interfaces.Services.Arquivo;
using Livraria.Application.Interfaces.Services.Livro;
using Livraria.Application.Interfaces.Services.Response;
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

        private readonly IGerarArquivo<LivroOutputDto> gerarArquivo;

        private readonly ILerArquivo<LivroInputDto> lerArquivo;

        private readonly ILivroReadRepository livroReadRepository;

        private readonly ILivroWriteRepository repositoryLivro;

        public LivroService
        (
            IAutorReadRepository autorReadRepository,
            ICategoriaReadRepository categoriaReadRepository,
            IGerarArquivo<LivroOutputDto> gerarArquivo,
            ILerArquivo<LivroInputDto> lerArquivo,
            ILivroReadRepository livroReadRepository,
            ILivroWriteRepository repositoryLivro,
            IServiceResponse serviceResponse
        ) : base(serviceResponse)
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
            this.gerarArquivo = gerarArquivo;
            this.lerArquivo = lerArquivo;
            this.livroReadRepository = livroReadRepository;
            this.repositoryLivro = repositoryLivro;
        }

        public async Task<byte[]> DownloadLivros(string extensao)
        {
            if (string.IsNullOrEmpty(extensao))
                throw new ArgumentNullException("EXTENSÃO NÃO DECLARADA");

            var lstLivros = (await livroReadRepository.GetAll()).ToList();

            var arquivo = gerarArquivo.CriarBytes(extensao, lstLivros);
            
            return arquivo.CriarBytes();
        }

        public async Task<IServiceResponse> Insert(LivroInputDto dto, string usuarioLogado)
        {
            var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(dto.Categoria);
            if (!idCategoria)
            {
                Response.Mensagem = "CATEGORIA NÃO ENCONTRADA NO BANCO!";
                return Response;
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
                dto.Categoria,
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

        public async Task<IServiceResponse> Update(int id, LivroInputDto dto)
        {
            if (id < 0)
            {
                Response.Mensagem = "ID DO AUTOR NÃO INFORMADO!";
                return Response;
            }

            var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(dto.Categoria);
            if (!idCategoria)
            {
                Response.Mensagem = "CATEGORIA NÃO ENCONTRADA NO BANCO!";
                return Response;
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
            livro.AtribuirCategoria(dto.Categoria);
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
