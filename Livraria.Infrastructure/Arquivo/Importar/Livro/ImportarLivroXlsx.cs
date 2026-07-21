using Livraria.Domain.Dtos.Arquivo;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Importar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using OfficeOpenXml;
using System.Globalization;

namespace Livraria.Infrastructure.Arquivo.Importar.Livro
{
    public class ImportarLivroXlsx : IImportarLivros
    {
        private readonly IAutorReadRepository autorReadRepository;

        private readonly ICategoriaReadRepository categoriaReadRepository;

        public ImportarLivroXlsx(IAutorReadRepository autorReadRepository, ICategoriaReadRepository categoriaReadRepository)
        {
            this.autorReadRepository = autorReadRepository;
            this.categoriaReadRepository = categoriaReadRepository;
        }

        public bool SuportaExtensao(string extensao) => extensao.Equals(".xlsx", StringComparison.OrdinalIgnoreCase);

        public async Task<IEnumerable<LivroEntity>> LerArquivo(ArquivoDto dto)
        {
            using var package = new ExcelPackage(dto.Stream);
            var planilha = package.Workbook.Worksheets[0];

            List<LivroEntity> livros = [];
            List<int> fk_categorias = [];

            var totalLinhas = planilha.Dimension.Rows;
            for (int linha = 2; linha <= totalLinhas; linha++)
            {
                /// <summary>
                /// • ".Value" RETORNA O VALOR BRUTO DA CÉLULA (int, double, DateTime, bool, stirng, etc)
                /// • ".Text" RETORNA O VALOR FORMATADO. SEMPRE UMA STRING
                /// </summary>
                string titulo = planilha.Cells[linha, 2].Text;
                string isbn = planilha.Cells[linha, 1].Text;
                DateTime dt_publicacao = DateTime.ParseExact(planilha.Cells[linha, 6].Text, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
                decimal preco = decimal.Parse(planilha.Cells[linha, 7].Text, NumberStyles.Number, CultureInfo.InvariantCulture);
                int qt_estoque = int.Parse(planilha.Cells[linha, 8].Text);
                string subtitulo = planilha.Cells[linha, 3].Text;

                int idAutor = await autorReadRepository.BuscarIdAutorPorNome(planilha.Cells[linha, 5].Text);

                string[] vectorCategoria = planilha.Cells[linha, 4].Text.Split(',');
                foreach (var categoria in vectorCategoria)
                {
                    int idCategoria = await categoriaReadRepository.BuscarIdCategoriaPorNome(categoria.Trim());
                    if (idCategoria == 0)
                        continue;

                    fk_categorias.Add(idCategoria);
                }

                livros.Add
                (
                    new LivroEntity(titulo, isbn, dt_publicacao, preco, qt_estoque, fk_categorias, subtitulo, idAutor)
                );
            }

            return livros;
        }
    }
}
