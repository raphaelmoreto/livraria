using Livraria.Application.Arquivo.Base;
using Livraria.Application.Interfaces.Services.Arquivo;
using Livraria.Domain.Dtos.Livro;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Livraria.Application.Arquivo.Exportar.Livro
{
    public class LivroXlsx : BaseExportar<LivroOutputDto>, ICriarBytes
    {
        public LivroXlsx(List<LivroOutputDto> dados) : base(dados) { }

        protected override byte[] FormatarDadosEmBytes()
        {
            using var package = new ExcelPackage();
            var planilha = package.Workbook.Worksheets.Add("LIVROS");

            #region [ALINHAMENTO]
            planilha.Cells["A1:H36"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            #endregion

            #region [CORES]
            planilha.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planilha.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(112, 48, 160));
            #endregion

            #region [LETRAS]
            planilha.Cells["A1:H1"].Style.Font.Color.SetColor(Color.White);
            #endregion

            #region [CABEÇALHO]
            planilha.Cells["A1"].Value = "ISBN";
            planilha.Cells["B1"].Value = "TÍTULO";
            planilha.Cells["C1"].Value = "SUBTITULO";
            planilha.Cells["D1"].Value = "CATEGORIA";
            planilha.Cells["E1"].Value = "AUTOR";
            planilha.Cells["F1"].Value = "DATA PUBLICAÇÃO";
            planilha.Cells["G1"].Value = "PREÇO";
            planilha.Cells["H1"].Value = "QUANTIDADE ESTOQUE";
            #endregion

            #region [DADOS]
            int contador = 2;
            for (int i = 0; i < Dados.Count; i++)
            {
                var livro = Dados[i];
                planilha.Cells[$"A{contador}"].Value = livro.Isbn;
                planilha.Cells[$"B{contador}"].Value = livro.Titulo;
                planilha.Cells[$"C{contador}"].Value = livro.Subtitulo;
                planilha.Cells[$"D{contador}"].Value = livro.Categoria;
                planilha.Cells[$"E{contador}"].Value = livro.Autor;
                planilha.Cells[$"F{contador}"].Value = livro.Dt_Publicacao.ToString("dd/MM/yyyy");
                planilha.Cells[$"G{contador}"].Value = livro.Preco;
                planilha.Cells[$"H{contador}"].Value = livro.Quantidade;

                contador++;
            }
            #endregion

            planilha.Cells.AutoFitColumns();
            return package.GetAsByteArray();
        }
    }
}
