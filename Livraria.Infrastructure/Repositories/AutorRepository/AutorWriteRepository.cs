using Dapper;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.AutorRepositories
{
    public class AutorWriteRepository : BaseWrite<AutorEntity>, IAutorRepository
    {
        public AutorWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> BuscarAutorPorNome(string nomeAutor)
        {
            SB.AppendLine("SELECT COUNT([nome])");
            SB.AppendLine("FROM [dbo].[Autor]");
            SB.AppendLine("WHERE [nome] = @Nome");

            return await Connection.QuerySingleAsync<int>(SB.ToString(), new { Nome = nomeAutor }) > 0;
        }
    }
}
