using Dapper;
using Livraria.Domain.Dtos.Livro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.LivroRepository
{
    public class LivroReadRepository : BaseRead<LivroOutputDto>, ILivroReadRepository
    {
        public LivroReadRepository(IDatabaseConnection databaseConnection) : base(databaseConnection) { }

        public async Task<IEnumerable<LivroOutputDto>> BuscaPorPaginacao(int page, int pageSize = 20)
        {
            //OFFSET É A QUANTIDADE DE LINHAS QUE SERÃO PULADAS
            //PAGESIZE É O LIMITADOR PARA QUANTIDADE DE DADOS A SEREM RETORNARDOS
            var offset = (page - 1) * pageSize;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT l.[id],");
            sb.AppendLine("            l.[titulo],");
            sb.AppendLine("            l.[isbn],");
            sb.AppendLine("            l.[dt_publicacao],");
            sb.AppendLine("            l.[preco],");
            sb.AppendLine("            l.[qt_estoque] AS 'quantidade',");
            sb.AppendLine("            STRING_AGG(c.[nome], ', ' ) AS 'categorias',");
            sb.AppendLine("            l.[subtitulo],");
            sb.AppendLine("            a.[nome] AS 'autor'");
            sb.AppendLine("FROM [dbo].[Livro] l");
            sb.AppendLine("INNER JOIN Categoria_Livro cl ON cl.fk_livro = l.id");
            sb.AppendLine("INNER JOIN [dbo].[Categoria] c ON c.[id] = cl.[fk_categoria]");
            sb.AppendLine("LEFT JOIN [dbo].[Autor] a ON l.[fk_autor] = a.[id]");
            sb.AppendLine("GROUP BY l.[id],");
            sb.AppendLine("                l.[titulo],");
            sb.AppendLine("                l.[subtitulo],");
            sb.AppendLine("                l.[isbn],");
            sb.AppendLine("                l.[dt_publicacao],");
            sb.AppendLine("                l.[preco],");
            sb.AppendLine("                l.[qt_estoque],");
            sb.AppendLine("                a.[nome]");
            sb.AppendLine("ORDER BY l.[titulo]");
            sb.AppendLine("OFFSET @Offset ROWS");
            sb.AppendLine("FETCH NEXT @PageSize ROWS ONLY;");

            return await Connection.QueryAsync<LivroOutputDto>(sb.ToString(), new { Offset = offset, PageSize = pageSize });
        }

        public async Task<IEnumerable<LivroOutputDto>> GetAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT l.[id],");
            sb.AppendLine("            l.[titulo],");
            sb.AppendLine("            l.[isbn],");
            sb.AppendLine("            l.[dt_publicacao],");
            sb.AppendLine("            l.[preco],");
            sb.AppendLine("            l.[qt_estoque] AS 'quantidade',");
            sb.AppendLine("            STRING_AGG(c.[nome], ', ' ) AS 'categorias',");
            sb.AppendLine("            l.[subtitulo],");
            sb.AppendLine("            a.[nome] AS 'autor'");
            sb.AppendLine("FROM [dbo].[Livro] l");
            sb.AppendLine("INNER JOIN Categoria_Livro cl ON cl.fk_livro = l.id");
            sb.AppendLine("INNER JOIN [dbo].[Categoria] c ON c.[id] = cl.[fk_categoria]");
            sb.AppendLine("LEFT JOIN [dbo].[Autor] a ON l.[fk_autor] = a.[id]");
            sb.AppendLine("GROUP BY l.[id],");
            sb.AppendLine("                l.[titulo],");
            sb.AppendLine("                l.[subtitulo],");
            sb.AppendLine("                l.[isbn],");
            sb.AppendLine("                l.[dt_publicacao],");
            sb.AppendLine("                l.[preco],");
            sb.AppendLine("                l.[qt_estoque],");
            sb.AppendLine("                a.[nome]");
            sb.AppendLine("ORDER BY l.[titulo]");

            return await Connection.QueryAsync<LivroOutputDto>(sb.ToString());
        }

        public async Task<LivroOutputDto?> GetById(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT l.[id],");
            sb.AppendLine("            l.[titulo],");
            sb.AppendLine("            l.[isbn],");
            sb.AppendLine("            l.[dt_publicacao],");
            sb.AppendLine("            l.[preco],");
            sb.AppendLine("            l.[qt_estoque] AS 'quantidade',");
            sb.AppendLine("            STRING_AGG(c.[nome], ', ') AS 'categorias',");
            sb.AppendLine("            l.[subtitulo],");
            sb.AppendLine("            a.[nome] AS 'autor'");
            sb.AppendLine("FROM [dbo].[Livro] l");
            sb.AppendLine("INNER JOIN Categoria_Livro cl ON cl.fk_livro = l.id");
            sb.AppendLine("INNER JOIN [dbo].[Categoria] c ON c.[id] = cl.[fk_categoria]");
            sb.AppendLine("LEFT JOIN [dbo].[Autor] a ON l.[fk_autor] = a.[id]");
            sb.AppendLine("WHERE l.[id] = @Id");
            sb.AppendLine("GROUP BY l.id,");
            sb.AppendLine("                l.[titulo],");
            sb.AppendLine("                l.[subtitulo],");
            sb.AppendLine("                l.[isbn],");
            sb.AppendLine("                l.[dt_publicacao],");
            sb.AppendLine("                l.[preco],");
            sb.AppendLine("                l.[qt_estoque],");
            sb.AppendLine("                a.[nome]");

            return await Connection.QueryFirstOrDefaultAsync<LivroOutputDto>(sb.ToString(), new { Id = id });
        }
    }
}
