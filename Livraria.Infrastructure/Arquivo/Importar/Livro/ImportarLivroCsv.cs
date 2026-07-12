using Livraria.Domain.Dtos.Arquivo;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Importar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;

namespace Livraria.Infrastructure.Arquivo.Importar.Livro
{
    public class ImportarLivroCsv : IImportarLivros
    {
        private readonly IAutorReadRepository autorReadRepository;

        private readonly ICategoriaReadRepository categoriaReadRepository;

        public ImportarLivroCsv(IAutorReadRepository autorReadRepository, ICategoriaReadRepository categoriaReadRepository)
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
        }

        public bool SuportaExtensao(string extensao) => extensao.Equals(".csv", StringComparison.OrdinalIgnoreCase);

        Task<IEnumerable<LivroEntity>> IImportarLivros.LerArquivo(ArquivoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
