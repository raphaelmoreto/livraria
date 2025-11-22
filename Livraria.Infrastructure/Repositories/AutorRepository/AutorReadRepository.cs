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

        public async Task<int> BuscarIdDoAutor(string nomeAutor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id]");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [nome] = @Autor");

            return await Connection.ExecuteScalarAsync<int>(sb.ToString(), new { Autor = nomeAutor });
        }

        public async Task<IEnumerable<AutorOutputDto>> Listar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Autor]");

            return await Connection.QueryAsync<AutorOutputDto>(sb.ToString());
        }

        public async Task<AutorOutputDto?> SelecionarPorId(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [id] = @Id");

            return await Connection.QueryFirstOrDefaultAsync<AutorOutputDto>(sb.ToString(), new { Id = id });
        }
    }
}
