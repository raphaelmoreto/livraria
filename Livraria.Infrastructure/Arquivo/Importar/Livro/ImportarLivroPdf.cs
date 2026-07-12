using Livraria.Domain.Dtos.Arquivo;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Importar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;

namespace Livraria.Infrastructure.Arquivo.Importar.Livro
{
    public class ImportarLivroPdf : IImportarLivros
    {
        private readonly IAutorReadRepository autorReadRepository;

        private readonly ICategoriaReadRepository categoriaReadRepository;

        public ImportarLivroPdf(IAutorReadRepository autorReadRepository, ICategoriaReadRepository categoriaReadRepository)
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
        }

        public bool SuportaExtensao(string extensao) => extensao.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

        Task<IEnumerable<LivroEntity>> IImportarLivros.LerArquivo(ArquivoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
