using iText.StyledXmlParser.Jsoup.Safety;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Livraria.Infrastructure.Arquivo.Exportar.Livro
{
    public class ExportarLivroPdf : IExportarLivros
    {
        public bool SuportaExtensao(string extensao) => extensao.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

        private const string Roxo = "512da8";
        private const string RoxoClaro = "5c6bc0";
        private const string Preto = "#0f0f0f";

        public byte[] CriarBytes(List<LivroOutputDto> dados)
        {
            return Document.Create(container => {
                container.Page(page => {

                    page.DefaultTextStyle(x => x.FontSize(9).FontColor(Preto));
                    page.Margin(20);
                    page.PageColor(Colors.White);

                    //"PageSizes.A4" COMANDO UTILIZADO NA GERAÇÃO DE RELATÓRIOS E DOCUMENTOS PDF PARA CONFIGURAR O TAMANHO DA PÁGINA COMO A4
                    //".Landscape()" ORIENTAÇÃO PARA PÁGINAS EM MODO PAISAGEM
                    page.Size(PageSizes.A4.Landscape());

                    //CABEÇALHO
                    page.Header()
                        .PaddingBottom(15)
                        .Column(column => {
                            column.Item()
                                .ShowOnce() //MOSTRA UMA ÚNICA VEZ
                                .AlignCenter()
                                .Padding(15)
                                .Text("RELATÓRIO DE LIVROS")
                                .Bold()
                                .FontColor(Roxo)
                                .FontSize(22);

                            column.Item()
                                .ShowOnce()
                                .AlignRight()
                                .PaddingTop(5)
                                .Text($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                .FontColor(Colors.Grey.Darken1)
                                .FontSize(8);
                        });

                    page.Content()
                        .Container()
                        .PaddingVertical(10)
                        //.Column(column => //FORMATAÇÃO NOVA DO LAYOUT
                        //{
                        //    //DEFINIÇÃO DAS COLUNAS
                        //    column.Item()
                        //        .ShowOnce()
                        //        .Table(table =>
                        //        {
                        //            table.ColumnsDefinition(columns =>
                        //            {
                        //                columns.RelativeColumn(18);    // ISBN
                        //                columns.RelativeColumn(30); //TÍTULO
                        //                columns.RelativeColumn(28); //SUBTITULO
                        //                columns.RelativeColumn(25);    // CATEGORIAS
                        //                columns.RelativeColumn(25);    // AUTOR
                        //                columns.RelativeColumn(14); //DT_PUBLICACAO                                
                        //                columns.RelativeColumn(10);   // PREÇO
                        //                columns.RelativeColumn(8);   // QUANTIDADE
                        //            });

                    //            CelulaCabecalho(table.Cell(), "ISBN");
                    //            CelulaCabecalho(table.Cell(), "TÍTULO");
                    //            CelulaCabecalho(table.Cell(), "SUBTITULO");
                    //            CelulaCabecalho(table.Cell(), "CATEGORIAS");
                    //            CelulaCabecalho(table.Cell(), "AUTOR");
                    //            CelulaCabecalho(table.Cell(), "PUBLICAÇÃO");
                    //            CelulaCabecalho(table.Cell(), "PREÇO");
                    //            CelulaCabecalho(table.Cell(), "QTD");

                    //            table
                    //                .Cell()
                    //                .ColumnSpan(8)
                    //                .BorderBottom(3)
                    //                .BorderColor(Preto)
                    //                .PaddingBottom(4);
                    //        });

                    //    //DADOS
                    //    column.Item()
                    //        .Table(table =>
                    //        {
                    //            table.ColumnsDefinition(columns =>
                    //            {
                    //                columns.RelativeColumn(18);    // ISBN
                    //                columns.RelativeColumn(30); //TÍTULO
                    //                columns.RelativeColumn(28); //SUBTITULO
                    //                columns.RelativeColumn(25);    // CATEGORIAS
                    //                columns.RelativeColumn(25);    // AUTOR
                    //                columns.RelativeColumn(14); //DT_PUBLICACAO                                
                    //                columns.RelativeColumn(10);   // PREÇO
                    //                columns.RelativeColumn(8);   // QUANTIDADE
                    //            });

                    //            for (int i = 0; i < dados.Count; i++)
                    //            {
                    //                var livro = dados[i];

                    //                string background = i % 2 == 0 ? Colors.White : RoxoClaro;
                    //                string fontColor = i % 2 == 0 ? Preto : Colors.White;

                    //                CelulaCorpo(table, livro.Isbn, background, fontColor);
                    //                CelulaCorpo(table, livro.Titulo, background, fontColor);
                    //                CelulaCorpo(table, livro.Subtitulo ?? "-", background, fontColor);
                    //                CelulaCorpo(table, livro.Categorias, background, fontColor);
                    //                CelulaCorpo(table, livro.Autor ?? "-", background, fontColor);
                    //                CelulaCorpo(table, livro.Dt_Publicacao.ToString("dd/MM/yyyy"), background, fontColor);
                    //                CelulaCorpo(table, livro.Preco.ToString("F2", CultureInfo.InvariantCulture), background, fontColor);
                    //                CelulaCorpo(table, livro.Quantidade.ToString(), background, fontColor);
                    //            }
                    //        });
                    //});

                        //FORMATAÇÃO ANTIGA DO LAYOUT
                        .Table(table =>
                         {

                             //DEFINIÇÃO DAS COLUNAS
                             table.ColumnsDefinition(columns =>
                             {
                                 columns.RelativeColumn(18);    // ISBN
                                 columns.RelativeColumn(30); //TÍTULO
                                 columns.RelativeColumn(28); //SUBTITULO
                                 columns.RelativeColumn(25);    // CATEGORIAS
                                 columns.RelativeColumn(25);    // AUTOR
                                 columns.RelativeColumn(14); //DT_PUBLICACAO                                
                                 columns.RelativeColumn(10);   // PREÇO
                                 columns.RelativeColumn(8);   // QUANTIDADE
                             });

                             table.Header(header =>
                             {
                                 CelulaCabecalho(header.Cell(), "ISBN");
                                 CelulaCabecalho(header.Cell(), "TÍTULO");
                                 CelulaCabecalho(header.Cell(), "SUBTITULO");
                                 CelulaCabecalho(header.Cell(), "CATEGORIAS");
                                 CelulaCabecalho(header.Cell(), "AUTOR");
                                 CelulaCabecalho(header.Cell(), "PUBLICAÇÃO");
                                 CelulaCabecalho(header.Cell(), "PREÇO");
                                 CelulaCabecalho(header.Cell(), "QTD");

                                 header
                                     .Cell()
                                     .ColumnSpan(8)
                                     .BorderBottom(3)
                                     .BorderColor(Preto)
                                     .PaddingBottom(4);
                             });

                             //DADOS
                             for (int i = 0; i < dados.Count; i++)
                             {
                                 var livro = dados[i];

                                 string background = i % 2 == 0 ? Colors.White : RoxoClaro;
                                 string fontColor = i % 2 == 0 ? Preto : Colors.White;

                                 CelulaCorpo(table, $"|{livro.Isbn}|", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Titulo}|", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Subtitulo}|" ?? "-", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Categorias}|", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Autor}|" ?? "-", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Dt_Publicacao.ToString("dd/MM/yyyy")}|", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Preco.ToString("F2", CultureInfo.InvariantCulture)}|", background, fontColor);
                                 CelulaCorpo(table, $"|{livro.Quantidade.ToString()}|", background, fontColor);
                             }
                         });

                    page.Footer()
                        .AlignCenter()
                        .PaddingTop(10)
                        .Text(x => {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                            x.Span(" de ");
                            x.TotalPages();
                        });
                });
            })
            .GeneratePdf();
        }

        private static void CelulaCabecalho(IContainer container, string texto)
        {
            container
                .AlignCenter()
                .PaddingHorizontal(5)
                .PaddingVertical(8)
                .Text(texto)
                .Bold()
                .FontColor(Preto)
                .FontSize(9);
        }

        private static void CelulaCorpo(TableDescriptor table, string texto, string background, string fontColor)
        {
            table.Cell()
                .Background(background)
                .Padding(6)
                .AlignCenter()
                .Text(t =>
                {
                    var sb = new StringBuilder();
                    foreach (char c in texto)
                    {
                        if (c == '|')
                        {
                            //ESCREVE O TEXTO ACUMULADO
                            if (sb.Length > 0)
                            {
                                t.Span(sb.ToString())
                                    .FontColor(fontColor)
                                    .FontSize(8);

                                sb.Clear();
                            }

                            t.Span("|").FontColor(background); //ESCREVE O SEPARADOR ESCONDIDO
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }

                    if (sb.Length > 0)
                    {
                        t.Span(sb.ToString()).FontColor(fontColor).FontSize(8);
                    }
                });
        }
    }
}