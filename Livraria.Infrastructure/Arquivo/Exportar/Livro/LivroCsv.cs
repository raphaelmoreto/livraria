using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using System.Globalization;
using System.Text;

namespace Livraria.Infrastructure.Arquivo.Exportar.Livro
{
    public class LivroCsv : IExportarLivro
    {
        public bool SuportaExtensao(string extensao) => extensao.Equals(".csv", StringComparison.OrdinalIgnoreCase);

        public byte[] CriarBytes(List<LivroOutputDto> dados)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, new UTF8Encoding(true));

            writer.WriteLine("ISBN;TITULO;SUBTITULO;CATEGORIA;AUTOR;DATA PUBLICACAO;PRECO;QUANTIDADE");
            foreach (var livro in dados)
            {
                writer.WriteLine(
                    $"{livro.Isbn};" +
                    $"{livro.Titulo};" +
                    $"{livro.Subtitulo};" +
                    $"{livro.Categoria};" +
                    $"{livro.Autor};" +
                    $"{livro.Dt_Publicacao.ToString("dd/MM/yyyy")};" +
                    $"{livro.Preco.ToString("F2", CultureInfo.InvariantCulture)};" +
                    $"{livro.Quantidade}"
                );
            }
            writer.Flush();
            return ms.ToArray();
        }
    }
}
