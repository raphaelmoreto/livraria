using Dapper;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.AutorRepository
{
    public class AutorReadRepository : BaseRead<AutorOutputDto>, IAutorReadRepository
    {
        public AutorReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<int> BuscarIdAutorPorNome(string nomeAutor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT ISNULL(MAX([id]), 0)");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [nome] = @Nome");

            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sb.ToString(), new { Nome = nomeAutor });
        }

        public async Task<bool> VerificarIdDoAutor(int numeroAutor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("       CASE");
            sb.AppendLine("               WHEN COUNT([id]) > 0 THEN 1");
            sb.AppendLine("               ELSE 0");
            sb.AppendLine("       END");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [Id] = @IdAutor;");

            using var connection = CreateConnection();
            return await connection.QuerySingleAsync<int>(sb.ToString(), new { IdAutor = numeroAutor }) > 0;
        }

        public async Task<IEnumerable<AutorOutputDto>> GetAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Autor];");

            using var connection = CreateConnection();
            return await connection.QueryAsync<AutorOutputDto>(sb.ToString());
        }

        public async Task<AutorOutputDto?> GetById(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [id] = @Id;");

            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AutorOutputDto>(sb.ToString(), new { Id = id });
        }
    }
}
