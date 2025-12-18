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

            return await Connection.QuerySingleAsync<int>(sb.ToString(), new { IdAutor = numeroAutor }) > 0;
        }

        public async Task<IEnumerable<AutorOutputDto>> GetAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Autor];");

            return await Connection.QueryAsync<AutorOutputDto>(sb.ToString());
        }

        public async Task<AutorOutputDto?> GetById(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [id] = @Id;");

            return await Connection.QueryFirstOrDefaultAsync<AutorOutputDto>(sb.ToString(), new { Id = id });
        }
    }
}
