using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using System.Globalization;
using System.Text;

namespace Livraria.Infrastructure.Arquivo.Exportar.Livro
{
    public class ExportarLivroTxt : IExportarLivros
    {
        public bool SuportaExtensao(string extensao) => extensao.Equals(".txt", StringComparison.OrdinalIgnoreCase);

        public byte[] CriarBytes(List<LivroOutputDto> dados)
        {
            var lstLivros = dados.Select(
                l => $"{l.Isbn} | " +
                        $"{l.Titulo} | " +
                        $"{l.Subtitulo} | " +
                        $"{l.Categorias} | " +
                        $"{l.Autor} | " +
                        $"{l.Dt_Publicacao:dd/MM/yyyy} | " +
                        $"{l.Preco.ToString("F2", CultureInfo.InvariantCulture)} | " +
                        $"{l.Quantidade}"
            );

            var content = string.Join(Environment.NewLine, lstLivros);
            return Encoding.UTF8.GetBytes(content);
        }
    }
}
