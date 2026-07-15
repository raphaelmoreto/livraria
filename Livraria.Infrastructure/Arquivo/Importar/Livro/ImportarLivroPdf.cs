using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
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

        public Task<IEnumerable<LivroEntity>> LerArquivo(ArquivoDto dto)
        {
            using var pdf = new PdfDocument(new PdfReader(dto.Stream));

            //ToDo:INCOMPLETO
            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                var pagina = pdf.GetPage(i);
                string textos = PdfTextExtractor.GetTextFromPage(pagina);
                string[] vextorTextos = textos.Split('\n');
            }

            throw new NotImplementedException();
        }
    }
}
