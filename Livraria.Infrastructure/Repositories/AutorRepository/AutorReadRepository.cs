using Dapper;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.AutorRepository
{
    public class AutorReadRepository : BaseRead<AutorOutputDto>, IAutorReadRepository
    {
        public AutorReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<IEnumerable<AutorOutputDto>> Listar()
        {
            SB.AppendLine("SELECT [id],");
            SB.AppendLine("            [nome]");
            SB.AppendLine("FROM [dbo].[Autor]");

            return await Connection.QueryAsync<AutorOutputDto>(SB.ToString());
        }

        public async Task<AutorOutputDto?> SelecionarPorId(int id)
        {
            SB.AppendLine("SELECT [id],");
            SB.AppendLine("            [nome]");
            SB.AppendLine("FROM [dbo].[Autor]");
            SB.AppendLine("WHERE [id] = @Id");

            return await Connection.QueryFirstOrDefaultAsync<AutorOutputDto>(SB.ToString(), new { Id = id });
        }
    }
}
