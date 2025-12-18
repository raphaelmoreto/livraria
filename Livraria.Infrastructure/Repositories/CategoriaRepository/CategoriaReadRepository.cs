using Dapper;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.CategoriaRepository
{
    public class CategoriaReadRepository : BaseRead<CategoriaLivroOutputDto>, ICategoriaReadRepository
    {
        public CategoriaReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> VerificarIdDaCategoria(int numeroCategoria)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("       CASE");
            sb.AppendLine("               WHEN COUNT([Categoria].[id]) > 0 THEN 1");
            sb.AppendLine("               ELSE 0");
            sb.AppendLine("       END");
            sb.AppendLine("FROM [dbo].[Categoria]");
            sb.AppendLine("WHERE [Categoria].[id] = @Id;");

            return await Connection.QuerySingleAsync<int>(sb.ToString(), new { Id = numeroCategoria }) > 0;
        }

        public async Task<IEnumerable<CategoriaLivroOutputDto>> GetAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Categoria];");

            return await Connection.QueryAsync<CategoriaLivroOutputDto>(sb.ToString());
        }

        public async Task<CategoriaLivroOutputDto?> GetById(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Categoria]");
            sb.AppendLine("WHERE [id] = @Id;");

            return await Connection.QueryFirstOrDefaultAsync<CategoriaLivroOutputDto>(sb.ToString(), new { Id = id });
        }
    }
}
