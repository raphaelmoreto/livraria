using Dapper;
using Livraria.Domain.Dtos.Autor;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.AutorRepository
{
    public class AutorReadRepository : BaseRead<AutorOutputDto>, IRepositoryRead<AutorOutputDto>
    {
        public AutorReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public Task<IEnumerable<AutorOutputDto>> Listar()
        {
            throw new NotImplementedException();
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
