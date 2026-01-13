using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using Livraria.Domain.Dtos.Livro;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;

namespace Livraria.Infrastructure.Arquivo.Exportar.Livro
{
    public class LivroXlsx : IExportarLivro
    {
        public bool SuportaExtensao(string extensao) => extensao.Equals(".xlsx", StringComparison.OrdinalIgnoreCase);

        public byte[] CriarBytes(List<LivroOutputDto> dados)
        {
            using var package = new ExcelPackage();
            var planilha = package.Workbook.Worksheets.Add("LIVROS");

            #region [ALINHAMENTO]
            planilha.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            #endregion

            #region [CABEÇALHO]
            planilha.Cells["A1"].Value = "ISBN";
            planilha.Cells["B1"].Value = "TÍTULO";
            planilha.Cells["C1"].Value = "SUBTITULO";
            planilha.Cells["D1"].Value = "CATEGORIA";
            planilha.Cells["E1"].Value = "AUTOR";
            planilha.Cells["F1"].Value = "DATA PUBLICAÇÃO";
            planilha.Cells["G1"].Value = "PREÇO UN.";
            planilha.Cells["H1"].Value = "QUANTIDADE ESTOQUE";
            #endregion

            #region [CORES]
            planilha.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planilha.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(112, 48, 160));
            #endregion

            #region [LETRAS]
            planilha.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);
            #endregion

            #region [DADOS]
            int contador = 2;
            for (int i = 0; i < dados.Count; i++)
            {
                var livro = dados[i];
                planilha.Cells[$"A{contador}"].Value = livro.Isbn;
                planilha.Cells[$"B{contador}"].Value = livro.Titulo;
                planilha.Cells[$"C{contador}"].Value = livro.Subtitulo;
                planilha.Cells[$"D{contador}"].Value = livro.Categorias;
                planilha.Cells[$"E{contador}"].Value = livro.Autor;
                planilha.Cells[$"F{contador}"].Value = livro.Dt_Publicacao.ToString("dd/MM/yyyy");
                planilha.Cells[$"G{contador}"].Value = livro.Preco.ToString("F2", CultureInfo.InvariantCulture);
                planilha.Cells[$"H{contador}"].Value = livro.Quantidade;

                planilha.Cells[$"A{contador}:H{contador}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                contador++;
            }
            #endregion

            planilha.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
