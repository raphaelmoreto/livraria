using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Livro;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Services.Base;
using Livraria.Application.Response;
using Livraria.Domain.Dtos.Arquivo;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Importar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.Livro;

namespace Livraria.Application.Services.Livro
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly IAutorReadRepository autorReadRepository;

        private readonly ICategoriaReadRepository categoriaReadRepository;

        private readonly IEnumerable<IExportarLivros> exportador;

        private readonly IEnumerable<IImportarLivros> importador;

        private readonly ILivroReadRepository livroReadRepository;

        private readonly ILivroWriteRepository livroWriteRepository;

        public LivroService
        (
            IAutorReadRepository autorReadRepository,
            ICategoriaReadRepository categoriaReadRepository,
            IEnumerable<IExportarLivros> exportador,
            IEnumerable<IImportarLivros> importador,
            ILivroReadRepository livroReadRepository,
            ILivroWriteRepository livroWriteRepository
        )
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
            this.exportador = exportador;
            this.importador = importador;
            this.livroReadRepository = livroReadRepository;
            this.livroWriteRepository = livroWriteRepository;
        }

        public async Task<byte[]> DownloadLivros(string extensao)
        {
            if (string.IsNullOrEmpty(extensao))
                throw new ArgumentNullException("EXTENSÃO NÃO DECLARADA");

            var lstLivros = (await livroReadRepository.GetAll()).ToList();

            var exportar = exportador.FirstOrDefault(e => e.SuportaExtensao(extensao));

            return exportar is null ? throw new NotSupportedException("EXTENSÃO NÃO SUPORTADA") : exportar.CriarBytes(lstLivros);
        }

        public async Task<IServiceResponse> Insert(LivroInputDto dto, string usuarioLogado)
        {
            foreach (var categoria in dto.Fk_Categoria)
            {
                var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(categoria);
                if (!idCategoria)
                    return ServiceResponse.Error(TipoRetorno.NotFound, $"CATEGORIA DE Nº{categoria} NÃO ENCONTRADA NO BANCO!");
            }

            int idAutor = 0;
            if (dto.Autor != 0 && dto.Autor != null)
            {
                var idAutorBanco = await autorReadRepository.VerificarIdDoAutor(dto.Autor.Value);
                if (!idAutorBanco)
                    return ServiceResponse.Error(TipoRetorno.NotFound, "AUTOR NÃO ENCONTRADO NO BANCO!");

                else
                    idAutor = dto.Autor.Value;
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
            if (!livro.Validar())
                return ServiceResponse.Error(TipoRetorno.Validation, "ERRO DE VALIDAÇÃO", livro.Notifications.Select(x => x.Message));

            var insert = await livroWriteRepository.Insert(livro, usuarioLogado);
            if (!insert)
                return ServiceResponse.Error(TipoRetorno.Conflict, $"ERRO! LIVRO NÃO PODE SER CADASTRADO");

            return ServiceResponse.Ok("LIVRO CADASTRADO COM SUCESSO");
        }

        public async Task<IServiceResponse> RemoverCategorias(int idLivro, List<int> categorias)
        {
            if (idLivro <= 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "ID DO LIVRO NÃO INFORMADO!");

            if (categorias.Count <= 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "LISTA DE CATEGORIAS VÁZIA");

            foreach (var categoria in categorias)
            {
                var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(categoria);
                if (!idCategoria)
                    return ServiceResponse.Error(TipoRetorno.NotFound, $"CATEGORIA DE Nº{categoria} NÃO ENCONTRADA NO BANCO!");
            }

            foreach (var categoria in categorias)
            {
                bool exclusaoCategoria = await livroWriteRepository.RemoverCategorias(idLivro, categoria);
                if (!exclusaoCategoria)
                    return ServiceResponse.Error(TipoRetorno.Conflict, $"ERRO AO EXCLUIR CATEGORIA Nº{categoria}");
            }

            return ServiceResponse.Ok("CATEGORIAS REMOVIDAS COM SUCESSO");
        }

        public async Task<IServiceResponse> Update(int id, LivroInputDto dto)
        {
            if (id <= 0)
                return ServiceResponse.Error(TipoRetorno.NotFound, "ID DO AUTOR NÃO INFORMADO!");

            foreach (var categoria in dto.Fk_Categoria)
            {
                var idCategoria = await categoriaReadRepository.VerificarIdDaCategoria(categoria);
                if (!idCategoria)
                    return ServiceResponse.Error(TipoRetorno.NotFound, $"CATEGORIA DE Nº{categoria} NÃO ENCONTRADA NO BANCO!");
            }

            int idAutor = 0;
            if (dto.Autor != 0 && dto.Autor != null)
            {
                var idAutorBanco = await autorReadRepository.VerificarIdDoAutor(dto.Autor.Value);
                if (!idAutorBanco)
                    return ServiceResponse.Error(TipoRetorno.NotFound, "AUTOR NÃO ENCONTRADO NO BANCO!");

                else
                    idAutor = dto.Autor.Value;
            }

            var livro = await livroWriteRepository.GetById(id);
            if (livro == null)
                return ServiceResponse.Error(TipoRetorno.NotFound, "LIVRO NÃO ENCONTRADO NO BANCO!");

            livro.AtribuirTitulo(dto.Titulo);
            livro.AtribuirIsbn(dto.Isbn);
            livro.AtribuirDataPublicacao(dto.Dt_Publicacao);
            livro.AtribuirPreco(dto.Preco);
            livro.AtribuirCategoria(dto.Fk_Categoria);
            livro.AtribuirSubtitulo(dto.Subtitulo);
            livro.AtribuirAutor(idAutor);
            if (!livro.Validar())
                return ServiceResponse.Error(TipoRetorno.Validation, "ERRO DE VALIDAÇÃO", livro.Notifications.Select(x => x.Message));

            var update = await livroWriteRepository.Update(livro); //ToDo: FAZER UPDATE PARA O LIVRO
            if (!update)
                return ServiceResponse.Error(TipoRetorno.Conflict, $"ERRO! LIVRO NÃO PODE SER ATUALIZADO");

            return ServiceResponse.Ok("LIVRO ATUALIZADO COM SUCESSO");
        }

        public async Task<IServiceResponse> UploadLivros(ArquivoDto dto, string usuarioLogado)
        {
            if (dto.Stream is null || dto.Stream.Length == 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, $"ARQUIVO NÃO INFORMADO!");

            var importar = importador.FirstOrDefault(i => i.SuportaExtensao(dto.Extensao));
            if (importar is null)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "FORMATO DE ARQUIVO NÃO SUPORTADO!");

            var lstLivros = await importar.LerArquivo(dto);

            int livrosInseridos = 0;
            foreach (var livro in lstLivros)
            {
                if (!livro.Validar())
                    continue;

                var insert = await livroWriteRepository.Insert(livro, usuarioLogado);
                if (insert)
                    livrosInseridos++;
            }

            return ServiceResponse.Ok($"{livrosInseridos}/{lstLivros.Count()} LIVROS IMPORTADOS COM SUCESSO");
        }
    }
}
