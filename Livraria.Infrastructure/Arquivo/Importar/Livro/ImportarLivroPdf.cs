using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Livraria.Domain.Dtos.Arquivo;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Importar;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using System.Globalization;

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

        public async Task<IEnumerable<LivroEntity>> LerArquivo(ArquivoDto dto)
        {
            ///<summary>
            /// • PdfReader() - RESPONSÁVEL POR ABRIR O ARQUIVO PDF EXISTENTE (APENAS LÊ)
            /// • PdfDocument() - REPRESENTAÇÃO DE UM DOCUMENTO PDF NA MEMÓRIA. ELE MONTA A ESTRUTURA DO PDF PARA QUE POSSAMOS NEVEGAR PELAS PÁGINAS
            /// • PdfTextExtractor() - É QUEM EXTRAI O TEXTO DA PÁGINA QUE ESTAMOS LENDO NO MOMENTO
            /// • SimpleTextExtractionStrategy() - É A ESTRATÉGIA DE EXTRAÇÃO. DEFINE UMA FORMA SIMPLES DE ORGANIZAR O TEXTO EXTRAÍDO DURANTE A LEITURA
            ///</summary>
            using var pdf = new PdfDocument(new PdfReader(dto.Stream));
            var livros = new List<LivroEntity>();
            List<string> lstLinhas = new List<string>();

            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                string textos = PdfTextExtractor.GetTextFromPage(pdf.GetPage(i), new SimpleTextExtractionStrategy());
                string[] vectorTextos = textos.Split('|');
                
                bool lerTabela = false;

                foreach (var texto in vectorTextos)
                {
                    if (!lerTabela)
                    {
                        if (
                                texto.ToUpper().StartsWith("RELATÓRIO DE LIVROS") ||
                                texto.ToUpper().StartsWith("ISBN TÍTULO SUBTITULO CATEGORIAS AUTOR PUBLICAÇÃO PREÇO QTD")
                           )
                            lerTabela = true;

                        continue;
                    }

                    //VERIFICA SE A TABELA CHEGOU AO FINAL
                    if (texto.ToLower().StartsWith("\nPágina".ToLower()))
                    {
                        lerTabela = false;
                        break;
                    }

                    if (!string.IsNullOrWhiteSpace(texto))
                        lstLinhas.Add(texto);
                }
            }

            for (int j = 0; j < lstLinhas.Count; j += 8)
            {
                string isbn = lstLinhas[j];
                string titulo = lstLinhas[j + 1].Replace("\n", " ").Trim();
                string? subtitulo = lstLinhas[j + 2] == "-" ? null : lstLinhas[j + 2].Replace("\n", " ").Trim();
                string autor = lstLinhas[j + 4].Replace("\n", " ").Trim();
                DateTime dt_publicacao = DateTime.ParseExact(lstLinhas[j + 5], "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
                decimal preco = decimal.Parse(lstLinhas[j + 6], CultureInfo.InvariantCulture);
                int quantidade = int.Parse(lstLinhas[j + 7]);

                int idAutor = await autorReadRepository.BuscarIdAutorPorNome(autor);

                List<int> fk_Categorias = [];
                foreach (var categoria in lstLinhas[j + 3].Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    var nome = categoria.Replace("\n", " ").Trim();
                    int id = await categoriaReadRepository.BuscarIdCategoriaPorNome(nome);
                    if (id != 0)
                        fk_Categorias.Add(id);
                }

                livros.Add
                (
                    new LivroEntity(titulo, isbn, dt_publicacao, preco, quantidade, fk_Categorias, subtitulo, idAutor)
                );
            }

            return livros;
        }
    }
}
