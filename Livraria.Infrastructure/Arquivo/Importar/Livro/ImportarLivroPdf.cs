using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
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

            //ToDo: INCOMPLETO
            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                string textos = PdfTextExtractor.GetTextFromPage(pdf.GetPage(i), new SimpleTextExtractionStrategy());
                string[] vectorTextos = textos.Split('|');
                
                bool lerTabela = false;
                List<string> lstLinhas = new List<string>();

                foreach (var texto in vectorTextos)
                {
                    if (!lerTabela)
                    {
                        if (texto.StartsWith("QTD"))
                            lerTabela = true;

                        continue;
                    }

                    //VERIFICA SE A TABELA CHEGOU AO FINAL
                    if (texto.StartsWith("Página"))
                    {
                        lerTabela = false;
                        break;
                    }

                    lstLinhas.Add(texto);

                    //if (!string.IsNullOrWhiteSpace(texto))
                    //    lstLinhas.Add(texto);
                }
            }

            return livros;
        }
    }
}
